using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCloud1 : MonoBehaviour
{
    public Material main_material;

    private GameObject[][] spheres;
    private Vector3[][] dest_pos;
    private int state;
    private int rows;
    private int columns;
    private GameObject mean;
    private GameObject direction;
    private bool initialized;
    private GameObject[][] normals;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        rows = 11;
        columns = 15;
        initialized = false;
    }

    public void Initialize()
    {
        // initialize the spheres matrix of objects
        dest_pos = new Vector3[rows][];
        spheres = new GameObject[rows][];
        for (int i = 0; i < rows; i++)
        {
            spheres[i] = new GameObject[columns];
            dest_pos[i] = new Vector3[columns];
        }

        // create a sphere in every cell of the spheres matrix
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // create the sphere
                spheres[i][j] = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                dest_pos[i][j] = new Vector3(((int)(columns / 2) - j)/2.0f + transform.position.x
                    , ((int)(rows / 2) - i)/2.0f + transform.position.y, 0 + transform.position.z);

                spheres[i][j].transform.position = dest_pos[i][j];
                spheres[i][j].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f); // scale = 0.3 meter of radius
                spheres[i][j].GetComponent<Renderer>().material = main_material;
            }
        }
        initialized = true;
    }

    public void Next()
    {
        state++;
        
        if(state == 1)
        {
            Initialize();
        }

        if (state == 2)
        {
            Vector3 mean_pos = new Vector3(0, 0, 0);
            for (int i = 0; i < dest_pos.Length; i++)
            {
                for (int j = 0; j < dest_pos[i].Length; j++)
                {
                    mean_pos.x += dest_pos[i][j].x;
                    mean_pos.y += dest_pos[i][j].y;
                    mean_pos.z += dest_pos[i][j].z;
                }
            }

            mean_pos.x = mean_pos.x / (rows * columns);
            mean_pos.y = mean_pos.y / (rows * columns);
            mean_pos.z = mean_pos.z / (rows * columns);

            mean = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            mean.transform.position = mean_pos;
            mean.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            mean.GetComponent<Renderer>().material = main_material;
        }

        if (state == 3)
        {
            Destroy(mean);
        }

        if (state == 4)
        {
            for(int i = 0; i < dest_pos.Length;i++)
            {
                for(int j = 0; j < dest_pos[i].Length;j++)
                {
                    dest_pos[i][j] = new Vector3(((int)(columns / 2) - j)/2.0f, ((int)(rows / 2) - i)/2.0f , 0);
                }
            }
        }

        if(state == 5)
        {
            direction = GameObject.CreatePrimitive(PrimitiveType.Cube);
            direction.transform.position = new Vector3(0,0,0);
            direction.transform.localScale = new Vector3(0.2f, 9, 0.2f);
            Vector3 angle = new Vector3(90, 0, 70);
            direction.transform.rotation = Quaternion.Euler(angle);
            direction.GetComponent<Renderer>().material = main_material;
        }

        if(state == 6)
        {
            Destroy(direction);
        }

        if(state == 7)
        {
            CreateNormals();
        }
        if(state == 8)
        {
            DestroyNormals();
        }
        
    }

    public void Back()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(initialized)
        {
            float step;
            if (state == 4)
            {
                step = Time.deltaTime * 3.5f;
            } else
            {
                step = Time.deltaTime/2;
            }
            
            for (int i = 0; i < spheres.Length; i++)
            {
                for (int j = 0; j < spheres[i].Length; j++)
                {
                    spheres[i][j].transform.position = Vector3.MoveTowards(spheres[i][j].transform.position, dest_pos[i][j], step);
                }
            }
        }
        
    }

    public Vector3[][] GetSpheresPosition()
    {
        return dest_pos;
    }

    public void CreateNormals()
    {
        normals = new GameObject[rows][];

        for(int i = 0; i < rows;i++)
        {
            normals[i] = new GameObject[columns];
            for(int j = 0; j < columns; j++)
            {
                normals[i][j] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                normals[i][j].transform.position = spheres[i][j].transform.position;
                normals[i][j].transform.localScale = new Vector3(0.03f, 1.5f, 0.03f);
                normals[i][j].GetComponent<Renderer>().material = main_material;
                Vector3 angle = new Vector3(90, 0, 0);
                normals[i][j].transform.rotation = Quaternion.Euler(angle);
            }
        }
    }

    public void DestroyNormals()
    {
        for (int i = 0; i < rows; i++)
        {

            for (int j = 0; j < columns; j++)
            {
                Destroy(normals[i][j]);
            }
        }
    }
}
