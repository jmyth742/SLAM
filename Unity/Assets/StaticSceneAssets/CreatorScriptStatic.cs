using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;

public class CreatorScriptStatic : MonoBehaviour
{

    public Material materialRead;
    public GameObject cam;
    public float angle_speed;
    public int draw_every;


    private float counter;
    private List<Color> color_list;
    private Vector3[] pointsReaded;
    int numberPoints;
    int pointcloudtoRead;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        pointcloudtoRead = 0;
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime * angle_speed;
        if(counter > draw_every && pointcloudtoRead < 359)
        {
            string filename = "./Assets/StaticSceneAssets/data/pointcloud" + pointcloudtoRead + ".pcd";
            pointcloudtoRead += (int)draw_every;
            readPCDFile(filename);
            drawPCD();
            counter = 0;
        }
        

  
    }
    public void readPCDFile(string filename)
    {
        StreamReader reader = new StreamReader(filename, false);
        //Debug.Log("Start reading file");
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
        //Debug.Log("finished reading headers, pointsline: " + pointsPCD);
        lengthPCD = pointsPCD.Split(' ');
        int.TryParse(lengthPCD[1], out numberPoints);
        string dataPCD = reader.ReadLine();
        string pointCloudPCD;

        pointsReaded = new Vector3[numberPoints];
        color_list = new List<Color>();

        //Debug.Log("Size: " + numberPoints);

        for (int i = 0; i < numberPoints; i++)
        {
            pointCloudPCD = reader.ReadLine();
            tokens = pointCloudPCD.Split(' ');

            float.TryParse(tokens[0], out pointsReaded[i].x);
            float.TryParse(tokens[1], out pointsReaded[i].y);
            float.TryParse(tokens[2], out pointsReaded[i].z);

            pointsReaded[i].x = - float.Parse(tokens[0], CultureInfo.InvariantCulture);
            pointsReaded[i].y = float.Parse(tokens[1], CultureInfo.InvariantCulture);
            pointsReaded[i].z = float.Parse(tokens[2], CultureInfo.InvariantCulture);

            int temp = 0;

            int.TryParse(tokens[3], out temp);

            float red = (temp & 0x00FF0000) / 0xFF00;
            float green = (temp & 0x0000FF00) / 0xFF;
            float blue = (temp & 0x000000FF);

            Color c = new Color(red / 255.0f, green / 255.0f, blue / 255.0f, 0);
            color_list.Add(c);

            //if (i >= 132 && i <= 150)
            //{
            //    Debug.Log(i + "color: " + tokens[3] + " r: " + red + " g: " + green + " b: " + blue);
            //    Debug.Log(i + "tokens: " + tokens[0] + " " + tokens[1] + " " + tokens[2]);
            //    Debug.Log(i + "points: " + pointsReaded[i].x + " " + pointsReaded[i].y + " " + pointsReaded[i].z);
            //}

        }

        reader.Close();

        //Debug.Log("Done reading!");
    }

    public void drawPCD()
    {
        GameObject obj = new GameObject();
        obj.AddComponent<MeshFilter>();
        obj.AddComponent<MeshRenderer>();
        Mesh meshPoints = new Mesh();
        obj.transform.position = this.transform.position;
        obj.transform.rotation = Quaternion.identity;
        Vector3[] vertices = new Vector3[numberPoints];
        int[] indices = new int[numberPoints];

        for (int i = 0; i < numberPoints; i++)
        {
            vertices[i] = pointsReaded[i];
            indices[i] = i;

        }

        meshPoints.vertices = vertices;
        meshPoints.SetColors(color_list);
        meshPoints.SetIndices(indices, MeshTopology.Points, 0);
        meshPoints.RecalculateBounds();

        obj.GetComponent<MeshFilter>().mesh = meshPoints;
        obj.GetComponent<MeshRenderer>().material = materialRead;

    }
}
