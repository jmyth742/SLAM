using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PCBuilder : MonoBehaviour {

    //[Tooltip("Field of View (degree)")]
    //   public FoV FOV;

    public Material materialRead;

    //   [Tooltip("Image resolution (pixel)")]
    //   public SizeInt Size;

    //   [Tooltip("Sensor max distance (m)")]
    //   public float MaxRange;

    //   [Tooltip("Only for drawing (rgba)")]
    //   public Color ReadPointsColor;

    //   [Tooltip("Only for drawing (m)")]
    //   public float PointDrawingScale;
    List<Color> color_list;

    public GameObject tofCamera;

    private Vector3[] PointsToReach;
	private Vector3[] readPoints;
    private Vector3[] colorPoints;
    private Color[] colors;
	//private bool initialized = false;
    private GameObject readPointsParent;
    private int numberPoints;
    private Miro3DCamera tofScript;
    private int counter;

    Mesh meshRead;
  

	void Start()
    {
        tofScript = tofCamera.GetComponent<Miro3DCamera>();
        meshRead = new Mesh();
        readPoints = new Vector3[0];
        //Init();
    }

    void Update()
    {
        //      var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        //var z = Input.GetAxis("Vertical") * Time.deltaTime * 150.0f;

        //transform.Rotate(0, x, 0);
        //transform.Translate(0, 0, z);

        //if (!initialized)
        //{
        //    Init();
        //}

    }

    public void createGameObject()
    {
        //if (readPointsParent == null)
        //{
        readPointsParent = new GameObject();
        readPointsParent.name = "LastRead " + counter;
        //readPointsParent.transform.parent = transform;
        readPointsParent.transform.position = this.transform.TransformPoint(this.transform.position);
        readPointsParent.transform.rotation = Quaternion.identity;
        readPointsParent.AddComponent<MeshFilter>();
        MeshRenderer mr = readPointsParent.AddComponent<MeshRenderer>();
        mr.receiveShadows = false;
        mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;


        Debug.Log("Created parent GameObject!");

        //}
    }

    public void DrawPCD(bool visibility)
    {

        if (visibility)
        {
            //this.transform.position = new Vector3(1000, 0, 0);
            //this.transform.rotation = Quaternion.Euler(15, 0, 0);

            int max = numberPoints;
            Vector3[] vertices = new Vector3[max];
            int[] indices = new int[max];
            for (int i = 0; i < max; i++)
            {
                int e = Random.Range(0, readPoints.Length);
                vertices[i] = transform.InverseTransformPoint(readPoints[e]);
                indices[i] = i;
            }

            meshRead.vertices = vertices;
            meshRead.SetColors(color_list);
            meshRead.SetIndices(indices, MeshTopology.Points, 0);

			Debug.Log("Read mesh!");

        }
        else
        {
            meshRead.Clear();
        }

        meshRead.RecalculateBounds();

        readPointsParent.GetComponent<MeshFilter>().mesh = meshRead;
        readPointsParent.GetComponent<MeshRenderer>().material = materialRead;

        counter = counter + 1;

		Debug.Log("Drew mesh!");
    }
               
    public void ReadPCD(string filename)
    {		
		StreamReader reader = new StreamReader(filename, false);
        Debug.Log("Start reading file");
		string[] lengthPCD, tokens;

        string fileFormatPCD = reader.ReadLine();
        string versionPCD = reader.ReadLine();
        string fieldsPCD = reader.ReadLine();
        string sizePCD = reader.ReadLine();
        string typePCD = reader.ReadLine();
        string countPCD = reader.ReadLine();
        string widthPCD = reader.ReadLine();
        string heightPCD = reader.ReadLine();
        string viewpointPCD = reader.ReadLine();
        string pointsPCD = reader.ReadLine();
		lengthPCD = pointsPCD.Split(' ');
		int.TryParse(lengthPCD[1], out numberPoints);
        string dataPCD = reader.ReadLine();
		string pointCloudPCD;

        readPoints = new Vector3[numberPoints];
        colorPoints = new Vector3[numberPoints];
        colors = new Color[numberPoints];
        color_list = new List<Color>();

		Debug.Log("Size: " + numberPoints); 

        for (int i=0; i < numberPoints; i++)
        {
            pointCloudPCD = reader.ReadLine();
			tokens = pointCloudPCD.Split(' ');
            // Debug.Log("Size" + tokens.Length);
			// Debug.Log("Size: " + readPoints.Length); 
			float.TryParse(tokens[0], out readPoints[i].x);
			float.TryParse(tokens[1], out readPoints[i].y);
			float.TryParse(tokens[2], out readPoints[i].z);

            int temp = 0;

            int.TryParse(tokens[3], out temp);

            float red = (temp & 0x00FF0000)/0xFF00;
            float green = (temp & 0x0000FF00)/0xFF;
            float blue = (temp & 0x000000FF);

            colorPoints[i].x = red;
            colorPoints[i].y = green;
            colorPoints[i].z = blue;

            colors[i] = new Color(red / 255.0f, green / 255.0f, blue / 255.0f,0);
            color_list.Add(colors[i]);

            //if(i >= 132 && i <= 150)
            //{
            //    Debug.Log( i + "tokens: " + tokens[3] + " r: " + red + " g: " + green + " b: " + blue);
            //    Debug.Log("c:" + colors[i]);
            //}

        }

        reader.Close();

		Debug.Log("Done reading!"); 

    }

	//void Init()
 //   {
 //       //materialRead = new Material(Resources.Load<Shader>("GeometryShader"));
 //       //materialRead.SetFloat("_Size", PointDrawingScale);
 //       //materialRead.SetColor("_Color", new Color(0,0,0,0));

 //       readPoints = new Vector3[0];

 //       if (Size.rows <= 0) Debug.LogError("INVALID INPUT for camera vertical resolution. Actual value is " + Size.rows);
 //       if (Size.cols <= 0) Debug.LogError("INVALID INPUT for camera horizontal resolution. Actual value is " + Size.cols);

 //       if (MaxRange <= 0) Debug.LogError("INVALID INPUT for camera max visual distance. Actual value is " + MaxRange);

 //       if (FOV.v < 0) Debug.LogError("INVALID INPUT for camera vertical field of view. Actual value is " + FOV.v);
 //       if (FOV.h < 0) Debug.LogError("INVALID INPUT for camera horizontal field of view. Actual value is " + FOV.h);

 //       // change camera
 //       Camera.main.fieldOfView = FOV.v;

 //       // z si allontana dal sensore
 //       // x verso il basso
 //       // y verso sinistra
 //       float minX = 0.0f - FOV.v / 2.0f;
 //       float maxX = 0.0f + FOV.v / 2.0f;

 //       float minY = 0.0f - FOV.h / 2.0f;
 //       float maxY = 0.0f + FOV.h / 2.0f;

 //       float deltaX = (maxX - minX) / (float)Size.rows;
 //       float deltaY = (maxY - minY) / (float)Size.cols;

 //       PointsToReach = new Vector3[Size.cols * Size.rows];
                
 //       for (int r = 0; r < Size.cols; r++)
 //       {
 //           for (int c = 0; c < Size.rows; c++)
 //           {
 //               Vector3 p = new Vector3(0, 0, MaxRange);
 //               p = Quaternion.Euler(minX + deltaX * 0.5f + deltaX * c, minY + deltaY * 0.5f + deltaY * r, 0) * p;
 //               PointsToReach[r * Size.rows + c] = p;
 //           }
 //       }

 //       counter = 0;

 //       initialized = true;
 //       Debug.Log("PCD Builder initialized!");
 //   }

}
