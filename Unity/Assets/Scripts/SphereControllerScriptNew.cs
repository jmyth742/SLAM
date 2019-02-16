using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereControllerScriptNew : MonoBehaviour
{
    public Material main_material;

    private GameObject[][] spheres;
    private int state;
    private bool animation_started;
    private float currentAnimationTime;
    private Vector3 initialTranslation;
    private GameObject direction;
    private GameObject centerOfMass;
    private Vector3 centerOfMassOriginalPos;

    // Use this for initialization
    void Start()
    {
        animation_started = false;
        initialTranslation = new Vector3(10, 0, 0);
        state = 0;
        int rows = 11;
        int columns = 15;
        int n_spheres = rows * columns;
        spheres = new GameObject[rows][];
        for (int i = 0; i < rows; i++)
        {
            spheres[i] = new GameObject[columns];
        }

        Quaternion rotation = Quaternion.Euler(10, 10, 10);
        Matrix4x4 m = Matrix4x4.Rotate(rotation);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                spheres[i][j] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //set parent
                //spheres[i][j].transform.parent = this.transform;
                if (j != 0)
                {
                    spheres[i][j].transform.position = new Vector3((int)(columns / 2) - j + initialTranslation.x
                    , (int)(rows / 2) - i + initialTranslation.y, 0 + initialTranslation.z);
                } else // in the first column the spheres are 0.5 in the z direction, and not 0
                {
                    spheres[i][j].transform.position = new Vector3((int)(columns / 2) - j + initialTranslation.x
                    , (int)(rows / 2) - i + initialTranslation.y, 0.5f + initialTranslation.z);
                }
                
                spheres[i][j].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                spheres[i][j].GetComponent<Renderer>().material = main_material;

                spheres[i][j].transform.position = m.MultiplyPoint3x4(spheres[i][j].transform.position);

            }
        }
    }

    public void Next()
    {
        state++;
        Debug.Log("old dstate : " + state.ToString());

        if (state == 1)
        {
            Vector3 mean = new Vector3(0, 0, 0);
            for (int i = 0; i < spheres.Length; i++)
            {
                for (int j = 0; j < spheres[i].Length; j++)
                {
                    mean.x += spheres[i][j].transform.position.x;
                    mean.y += spheres[i][j].transform.position.y;
                    mean.z += spheres[i][j].transform.position.z;
                }
            }
            mean.x = mean.x / (spheres.Length * spheres[0].Length);
            mean.y = mean.y / (spheres.Length * spheres[0].Length);
            mean.z = mean.z / (spheres.Length * spheres[0].Length);
            centerOfMass = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            centerOfMass.transform.parent = this.transform;
            centerOfMass.transform.position = mean;
            centerOfMass.GetComponent<Renderer>().material = main_material;
            centerOfMassOriginalPos = mean;
            Debug.Log("mean: " + mean.ToString());
            centerOfMass.transform.localScale = new Vector3(1, 1, 1);

        }
        else if (state == 2)
        {
            animation_started = true;
            currentAnimationTime = 0;
        }
        else if (state == 3)
        {
            Destroy(centerOfMass);
        }
        else if (state == 4)
        {
            direction = GameObject.CreatePrimitive(PrimitiveType.Cube);
            direction.transform.parent = this.transform;
            direction.transform.localScale = new Vector3(0.2f, 15, 0.2f);
            Vector3 angle = new Vector3(120, 10, 75);
            direction.transform.rotation = Quaternion.Euler(angle);
            direction.GetComponent<Renderer>().material = main_material;

        }
        else if (state == 5)
        {
            // rotate every point of -9 degree, -5 degree and -9 degree 
            Quaternion rotation = Quaternion.Euler(-9, -5, -9);
            Matrix4x4 m = Matrix4x4.Rotate(rotation);
            for (int i = 0; i < spheres.Length; i++)
            {
                for (int j = 0; j < spheres[i].Length; j++)
                {
                    spheres[i][j].transform.position = m.MultiplyPoint3x4(spheres[i][j].transform.position);
                }
            }
            Vector3 angle = new Vector3(90, 0, 70); // rotate the big direction in the same way as the other direction
            direction.transform.rotation = Quaternion.Euler(angle);
        }
        else if (state == 6)
        {
            Destroy(direction);
        }
        else if (state == 7)
        {
            // do another little rotation of all the spheres in order to simulate a iteration
            Quaternion rotation = Quaternion.Euler(0, -1, -1);
            Matrix4x4 m = Matrix4x4.Rotate(rotation);
            for (int i = 0; i < spheres.Length; i++)
            {
                for (int j = 0; j < spheres[i].Length; j++)
                {
                    spheres[i][j].transform.position = m.MultiplyPoint3x4(spheres[i][j].transform.position);
                }
            }

        }
        else if (state == 8)
        {
            // do another little rotation of all the spheres in order to simulate a iteration
            Quaternion rotation = Quaternion.Euler(-1, -1, 0);
            Matrix4x4 m = Matrix4x4.Rotate(rotation);
            for (int i = 0; i < spheres.Length; i++)
            {
                for (int j = 0; j < spheres[i].Length; j++)
                {
                    spheres[i][j].transform.position = m.MultiplyPoint3x4(spheres[i][j].transform.position);
                }
            }
        }
        else if (state == 9)
        {
            // do another little rotation of all the spheres in order to simulate a iteration
            Quaternion rotation = Quaternion.Euler(0, -1, 0);
            Matrix4x4 m = Matrix4x4.Rotate(rotation);
            for (int i = 0; i < spheres.Length; i++)
            {
                for (int j = 0; j < spheres[i].Length; j++)
                {
                    spheres[i][j].transform.position = m.MultiplyPoint3x4(spheres[i][j].transform.position);
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if (animation_started)
        {

            if (state == 3)
            {
                Vector3 translation = new Vector3(-centerOfMassOriginalPos.x * Time.deltaTime / 5,
                    -centerOfMassOriginalPos.y * Time.deltaTime / 5, -centerOfMassOriginalPos.z * Time.deltaTime / 5);
                Translation(translation);

                currentAnimationTime += Time.deltaTime;
                if (currentAnimationTime >= 5)
                {
                    animation_started = false;
                }
            }

        }
    }

    private void Translation(Vector3 transl)
    {
        for (int i = 0; i < spheres.Length; i++)
        {
            for (int j = 0; j < spheres[i].Length; j++)
            {
                float new_x = spheres[i][j].transform.position.x + transl.x;
                float new_y = spheres[i][j].transform.position.y + transl.y;
                float new_z = spheres[i][j].transform.position.z + transl.z;
                Vector3 new_pos = new Vector3(new_x, new_y, new_z);
                spheres[i][j].transform.position = new_pos;
            }
        }

        float new_xc = centerOfMass.transform.position.x + transl.x;
        float new_yc = centerOfMass.transform.position.y + transl.y;
        float new_zc = centerOfMass.transform.position.z + transl.z;
        Vector3 new_posc = new Vector3(new_xc, new_yc, new_zc);
        centerOfMass.transform.position = new_posc;
    }
}

