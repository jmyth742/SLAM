using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class CreatorDynamicScript : MonoBehaviour
{
    public TMP_Text pcl_tx_text;
    public TMP_Text pcl_ty_text;
    public TMP_Text pcl_tz_text;
    public TMP_Text pcl_rx_text;
    public TMP_Text pcl_ry_text;
    public TMP_Text pcl_rz_text;

    private float pcl_tx;
    private float pcl_ty;
    private float pcl_tz;
    private float pcl_rx;
    private float pcl_ry;
    private float pcl_rz;

    public Material materialRead;
    private List<Color> color_list;
    private Vector3[] pointsReaded;

    ZmqCommunicator zmq = new ZmqCommunicator();
    MiroZmq.ZmqPC _pc = new MiroZmq.ZmqPC();

    byte[] msg;
    int numberPoints;

    // Start is called before the first frame update
    void Start()
    {
        zmq.Init(ZmqCommunicatorType.SUB, "tcp://127.0.0.1:5557");
        pcl_tx = 0.0f;
        pcl_ty = 0.0f;
        pcl_tz = 0.0f;
        pcl_rx = 0.0f;
        pcl_ry = 0.0f;
        pcl_rz = 0.0f;

    }

    void Init(){
        
    }

    // Update is called once per frame
    void Update()
    {
        bool res = zmq.TryGetLastMessage(ref msg);
        if(res){
            _pc.Deserialize(ref msg); 
            pointsReaded = new Vector3[_pc._rows*_pc._cols];
            color_list = new List<Color>();

            float red;
            float green;
            float blue;

            //Debug.Log("got message"); 
            for (int i = 0; i<_pc._rows*_pc._cols;i++ ){
                pointsReaded[i].x = - _pc.pointsRead[i].x;
                pointsReaded[i].y = _pc.pointsRead[i].y;
                pointsReaded[i].z = _pc.pointsRead[i].z;

                red = (_pc.colorsRead[i] & 0x00FF0000) / 0xFF00;
                green = (_pc.colorsRead[i] & 0x0000FF00) / 0xFF;
                blue = (_pc.colorsRead[i] & 0x000000FF);
                Color c = new Color(red / 255.0f, green / 255.0f, blue / 255.0f, 0);
                //Color c = new Color(0, 0, 0);
                color_list.Add(c);
            }
            //Debug.Log("Rows times columns: " + _pc._rows * _pc._cols);
            //Debug.Log("Rows: " + _pc._rows);
            
            //Debug.Log("Colors readed:");
            //Debug.Log(_pc.colorsRead[200]);

            //Debug.Log("Translations:");
            //Debug.Log(_pc._tx);
            //Debug.Log(_pc._ty);
            //Debug.Log(_pc._tz);

            //Debug.Log("Rotations:");
            //Debug.Log(_pc._rx);
            //Debug.Log(_pc._ry);
            //Debug.Log(_pc._rz);

            pcl_tx = _pc._tx;
            pcl_ty = _pc._ty;
            pcl_tz = _pc._tz;
            pcl_rx = _pc._rx;
            pcl_ry = _pc._ry;
            pcl_rz = _pc._rz;

            drawPCD(_pc._rows*_pc._cols);
            
        }

        pcl_tx_text.text = pcl_tx.ToString("00.00");
        pcl_ty_text.text = pcl_ty.ToString("00.00");
        pcl_tz_text.text = pcl_tz.ToString("00.00");
        pcl_rx_text.text = pcl_rx.ToString("00.00");
        pcl_ry_text.text = pcl_ry.ToString("00.00");
        pcl_rz_text.text = pcl_rz.ToString("00.00");
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

            int temp = 0;

            int.TryParse(tokens[3], out temp);

            float red = (temp & 0x00FF0000) / 0xFF00;
            float green = (temp & 0x0000FF00) / 0xFF;
            float blue = (temp & 0x000000FF);

            Color c = new Color(red / 255.0f, green / 255.0f, blue / 255.0f, 0);
            color_list.Add(c);

            if (i >= 132 && i <= 150)
            {
                Debug.Log(i + "tokens: " + tokens[3] + " r: " + red + " g: " + green + " b: " + blue);
                
            }

        }

        reader.Close();

        //Debug.Log("Done reading!");
    }

    public void drawPCD(int size)
    {
        GameObject obj = new GameObject();
        obj.AddComponent<MeshFilter>();
        obj.AddComponent<MeshRenderer>();
        Mesh meshPoints = new Mesh();
        obj.transform.position = this.transform.position;
        obj.transform.rotation = Quaternion.identity;
        Vector3[] vertices = new Vector3[size];
        int[] indices = new int[size];

        for (int i = 0; i < size; i++)
        {
            if (Mathf.Abs(pointsReaded[i].x) > 1000 || Mathf.Abs(pointsReaded[i].y) > 1000 || Mathf.Abs(pointsReaded[i].z) > 1000)
            {
                vertices[i] = new Vector3(0, 0, 0);
            } else
            {
                vertices[i] = pointsReaded[i];
            }
            indices[i] = i;
        }

        meshPoints.vertices = vertices;
        meshPoints.SetColors(color_list);
        meshPoints.SetIndices(indices, MeshTopology.Points, 0);
        meshPoints.RecalculateBounds();

        obj.GetComponent<MeshFilter>().mesh = meshPoints;
        obj.GetComponent<MeshRenderer>().material = materialRead;

    }

      public void drawPCDOld()
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
