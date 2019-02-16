using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using TMPro;
//using MiroZmq;

[System.Serializable]
public struct SizeInt
{
    public int rows, cols;

    public SizeInt(int r, int c)
    {
        rows = r;
        cols = c;
    }
}

[System.Serializable]
public struct FoV
{
    public float h, v;

    public FoV(float hFov, float vFov)
    {
        h = hFov;
        v = vFov;
    }
}

public class Miro3DCamera : MonoBehaviour {

    public FoV FOV;
    public SizeInt Size;
    public float MaxRange;
    public Color ActualPointsColor;
    public float PointDrawingScale;
    public float read_every;
    public bool capturing_active;
    public float distance_noise_sd;
    public float color_noise_sd;

    private int[] colors;

    private float timer;
    private Vector3[] PointsToReach;
    private Vector3[] LastCapturedPoints;
    private Vector3[] LastCapturedPoinsColor;
    private bool initialized = false;
    private int counter;
    Mesh meshActual;
    Material materialActual;
    RandomFromDistribution.ConfidenceLevel_e conf_level = RandomFromDistribution.ConfidenceLevel_e._95;

    ZmqCommunicator zmq = new ZmqCommunicator();

    // option menu stuff
    public GameObject captureMenu;
    public GameObject ICPMenu;

    private TMP_InputField[] inputFields;
    float verticalFOV;
    float horizontalFOV;
    int verticalRes;
    int horizontalRes;
    float maxRange;

    private TMP_InputField[] icpFields;
    int maxIter;
    float transformationEpsilon;
    float euclideanFitnessEpsilon;
    float filterSize;


    public bool IsInitialized() { return initialized; }

    public void Reload()
    {
        Init();
    }

    void Start()
    {
        meshActual = new Mesh();
        zmq.Init(ZmqCommunicatorType.PUB, "tcp://127.0.0.1:35781");
        Init();
        timer = 0;

        inputFields = captureMenu.GetComponentsInChildren<TMP_InputField>();
        icpFields = ICPMenu.GetComponentsInChildren<TMP_InputField>();
        maxIter = 10;
        transformationEpsilon = 1e-9f;
        euclideanFitnessEpsilon = 1e-9f;
        filterSize = 1.0f;



    }


    public void ValueChanged()
    {

        if (float.TryParse(inputFields[0].text, out verticalFOV))
        {
            this.FOV.v = verticalFOV;
        }
        if (float.TryParse(inputFields[1].text, out horizontalFOV))
        {
            this.FOV.h = horizontalFOV;
        }
        if (int.TryParse(inputFields[2].text, out verticalRes))
        {
            this.Size.cols = verticalRes;
        }
        if (int.TryParse(inputFields[3].text, out horizontalRes))
        {
            this.Size.rows = horizontalRes;
        }
        if (float.TryParse(inputFields[4].text, out maxRange))
        {
            this.MaxRange = maxRange;
        }

        if (int.TryParse(icpFields[0].text, out maxIter))
        {
            
        }
        if (float.TryParse(icpFields[1].text, out transformationEpsilon))
        {
           
        }
        if (float.TryParse(icpFields[2].text, out euclideanFitnessEpsilon))
        {
            
        }
        if (float.TryParse(icpFields[3].text, out filterSize))
        {
            
        }

        this.Reload();
    }
    void OnValidate()
    {
        initialized = false;
    }

    void Update()
    {
        if (!initialized)
        {
            Init();
        }

        if(capturing_active)
        {
            timer += Time.deltaTime;

            if (timer > read_every)
            {
                timer = 0;
                Vector3[] pointCloud;
                //Debug.Log("Point capturing started");
                CalcPointCloud(out pointCloud);
            }
        }
       


    }

    void Init()
    {
        //Debug.LogError("INITI CAMERA");
        //zmq.Init(ZmqCommunicatorType.PUB, "tcp://127.0.0.1:35781");

        materialActual = new Material(Resources.Load<Shader>("GeometryShader"));
        materialActual.SetFloat("_Size", PointDrawingScale);
        materialActual.SetColor("_Color", ActualPointsColor);

        LastCapturedPoints = new Vector3[0];
        LastCapturedPoinsColor = new Vector3[0];

        if (Size.rows <= 0) Debug.LogError("INVALID INPUT for camera vertical resolution. Actual value is " + Size.rows);
        if (Size.cols <= 0) Debug.LogError("INVALID INPUT for camera horizontal resolution. Actual value is " + Size.cols);

        if (MaxRange <= 0) Debug.LogError("INVALID INPUT for camera max visual distance. Actual value is " + MaxRange);

        if (FOV.v < 0) Debug.LogError("INVALID INPUT for camera vertical field of view. Actual value is " + FOV.v);
        if (FOV.h < 0) Debug.LogError("INVALID INPUT for camera horizontal field of view. Actual value is " + FOV.h);

        // change camera
        Camera.main.fieldOfView = FOV.v;

        float minX = 0.0f - FOV.v / 2.0f;
        float maxX = 0.0f + FOV.v / 2.0f;

        float minY = 0.0f - FOV.h / 2.0f;
        float maxY = 0.0f + FOV.h / 2.0f;

        float deltaX = (maxX - minX) / (float)Size.rows;
        float deltaY = (maxY - minY) / (float)Size.cols;

        PointsToReach = new Vector3[Size.cols * Size.rows];
                
        for (int r = 0; r < Size.cols; r++)
        {
            for (int c = 0; c < Size.rows; c++)
            {
                Vector3 p = new Vector3(0, 0, MaxRange);
                p = Quaternion.Euler(minX + deltaX * 0.5f + deltaX * c, minY + deltaY * 0.5f + deltaY * r, 0) * p;
                PointsToReach[r * Size.rows + c] = p;
            }
        }

        counter = 0;

        initialized = true;
        //Debug.Log("Camera parameters updated!");
    }

    public SizeInt GetPointcloudSize()
    {
        return Size;      
    }

    public void CalcPointCloud(out Vector3[] pts)
    {
        //Debug.LogError("Calculating point cloud");
        pts = new Vector3[PointsToReach.Length];
        Vector3[] pts_color = new Vector3[PointsToReach.Length];
        int[] colors = new int[PointsToReach.Length];
        Vector3 worldTarget;
        Ray ray;
        RaycastHit hit;

        for (int i = 0; i < PointsToReach.Length; i = i+1)
        {
            worldTarget = transform.TransformPoint(PointsToReach[i]);
            ray = new Ray(transform.position, worldTarget - transform.position);
            if (Physics.Raycast(ray, out hit))
            {
                pts[i] = transform.InverseTransformPoint(hit.point);
                pts[i].z = pts[i].z + color_noise_sd * pts[i].z * RandomFromDistribution.RandomRangeNormalDistribution(-3f, 3f, conf_level) / 100;

                Renderer rend = hit.transform.GetComponent<Renderer>();
                MeshCollider meshCollider = hit.collider as MeshCollider;

                if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null) {
                    pts_color[i] = new Vector3(0, 0, 0);
                } else {
                    Texture2D tex = rend.material.mainTexture as Texture2D;
                    Vector2 pixelUV = hit.textureCoord;
                    pixelUV.x *= tex.width;
                    pixelUV.y *= tex.height;
                    Color pixel_colour = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
                    pts_color[i].x = pixel_colour.r;
                    pts_color[i].y = pixel_colour.g;
                    pts_color[i].z = pixel_colour.b;

                    pts[i].z = pts[i].z + color_noise_sd * pts[i].z * RandomFromDistribution.RandomRangeNormalDistribution(-3f, 3f, conf_level) / 100;

                }
                if (Mathf.Abs(pts[i].x) > MaxRange || Mathf.Abs(pts[i].y) > MaxRange || Mathf.Abs(pts[i].z) > MaxRange)
                {
                    pts[i] = new Vector3(0, 0, 0);
                }

                if (pts[i].magnitude > MaxRange)
                {
                    pts[i] = new Vector3(0, 0, 0);
                    pts_color[i] = new Vector3(0, 0, 0);
                }
            }
            else
            {
                pts[i] = Vector3.zero;
                pts_color[i] = new Vector3(0, 0, 0);
            }
        }

        LastCapturedPoints = pts;
        LastCapturedPoinsColor = pts_color;

        for (int i = 0; i < LastCapturedPoints.Length; i++)
        {
            int red = (int)(LastCapturedPoinsColor[i].x * 255);
            int green = (int)(LastCapturedPoinsColor[i].y * 255);
            int blue = (int)(LastCapturedPoinsColor[i].z * 255);
            int alpha = 0;
            //int rgb = ((int)alpha << 24 | (int)red) << 16 | ((int)green) << 8 | ((int)blue);
            int rgb = ((int)red) << 16 | ((int)green) << 8 | ((int)blue);
            colors[i] = rgb;
        }

        //Debug.Log("Colors");
        //Debug.Log(colors[200]);

        //Debug.Log("ICP parameters:");
        //Debug.Log(_pc.t);
        

        //Debug.Log("Counter " + counter);
        //string filename = "Assets/Resources/test/pointcloud " + counter + ".pcd";
        //counter = counter + 1;
        //CreatePCD(filename);
        //CreatePCD("D:/UniTN/Robotic Perception and Action/new_SLAM/SLAM/build/inputUnity.pcd");

        //Serialize newSerialize = new Serialize(Size.rows, 4,PointsToReach);
        //for(int t=0; t <= Size.rows; t++){
        MiroZmq.ZmqPC _pc = new MiroZmq.ZmqPC();

        
        //Serialize(int rows, int cols, int maxIter, float transformationEpsilon, float euclideanFitnessEpsilon, float filterSize, float tx, float ty, float tz, float rx, float ry, float rz, ref Vector3[] points)
        zmq.Write(_pc.Serialize(GetPointcloudSize().rows, GetPointcloudSize().cols, maxIter, transformationEpsilon, euclideanFitnessEpsilon, filterSize, 0, 0, 0, 0, 0, 0, ref LastCapturedPoints, ref colors));
        //}
        //Debug.Log("Points Sent!");
        //Debug.Log(GetPointcloudSize().rows + " " + GetPointcloudSize().cols + " " + maxIter + " " + transformationEpsilon + " " + euclideanFitnessEpsilon + " " + filterSize);
    }

    public void CreatePCD(string filename)
    {
        StreamWriter writer = new StreamWriter(filename, false);

        writer.WriteLine("# .PCD v.7 - Point Cloud Data file format");
        writer.WriteLine("VERSION .7");
        writer.WriteLine("FIELDS x y z rgb");
        writer.WriteLine("SIZE 4 4 4 4");
        writer.WriteLine("TYPE F F F U");
        writer.WriteLine("COUNT 1 1 1 1");
        writer.WriteLine("WIDTH " + LastCapturedPoints.Length.ToString());
        writer.WriteLine("HEIGHT 1");
        writer.WriteLine("VIEWPOINT 0 0 0 1 0 0 0");
        writer.WriteLine("POINTS " + LastCapturedPoints.Length.ToString());
        writer.WriteLine("DATA ascii");

        for (int i=0; i < LastCapturedPoints.Length; i++)
        {
            writer.Write(LastCapturedPoints[i].x.ToString("0.0000"));
            writer.Write(" ");
            writer.Write(LastCapturedPoints[i].y.ToString("0.0000"));
            writer.Write(" ");
            writer.Write(LastCapturedPoints[i].z.ToString("0.0000"));
            writer.Write(" ");


            int red = (int)(LastCapturedPoinsColor[i].x * 255);
            int green = (int)(LastCapturedPoinsColor[i].y * 255);
            int blue = (int)(LastCapturedPoinsColor[i].z * 255);
            int rgb = ((int)red) << 16 | ((int)green) << 8 | ((int)blue);
            float rgb2 = (float)rgb;
            writer.WriteLine(rgb.ToString());
        }

        writer.Close();

        Debug.Log("Filename " + filename);

    }

    public float NextGaussian()
    {
        float v1, v2, s;
        do
        {
            v1 = 2.0f * Random.Range(0f, 1f) - 1.0f;
            v2 = 2.0f * Random.Range(0f, 1f) - 1.0f;
            s = v1 * v1 + v2 * v2;
        } while (s >= 1.0f || s == 0f);

        s = Mathf.Sqrt((-2.0f * Mathf.Log(s)) / s);

        return v1 * s;
    }

    public float NextGaussian(float mean, float standard_deviation)
    {
        return mean + NextGaussian() * standard_deviation;
    }

    public float NextGaussian(float mean, float standard_deviation, float min, float max)
    {
        float x;
        do
        {
            x = NextGaussian(mean, standard_deviation);
        } while (x < min || x > max);
        return x;
    }
}
