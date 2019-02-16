using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereControllerScriptOld : MonoBehaviour {

    public Material main_material;

    private GameObject[][] spheres;
    private int state;
    private bool animation_started;
    private float currentAnimationTime;
    private Vector3 initialTranslation;
    private GameObject direction;
    private GameObject centerOfMass;
    private Vector3 centerOfMassOriginalPos;
    private GameObject[][] directions;

    // Use this for initialization
    void Start () {
        animation_started = false;
        initialTranslation = new Vector3(-10, 0, 0); // initial translation from the origin of the central sphere of the cloud
        state = 0;
        int rows = 11;
        int columns = 15;
        // initialize the spheres matrix of objects
        spheres = new GameObject[rows][]; 
        for(int i = 0; i < rows;i++)
        {
            spheres[i] = new GameObject[columns];
        }

        // create a sphere in every cell of the spheres matrix
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // create the sphere
                spheres[i][j] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //set parent
                //spheres[i][j].transform.parent = this.transform;
                // set initial position
                spheres[i][j].transform.position = new Vector3((int)(columns / 2) - j + initialTranslation.x
                    , (int)(rows / 2) - i+ initialTranslation.y, 0 + initialTranslation.z);
                spheres[i][j].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f); // scale = 0.3 meter of radius
                spheres[i][j].GetComponent<Renderer>().material = main_material; // the main material is the one of the spheres

            }
        }
    }

    public void Next()
    {
        state++;
        Debug.Log("new dstate : " + state.ToString());

        if (state == 1)
        {
            // calculate the mean of the pointcloud
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
            // int the center of mass create a sphere of radius 1 representing the center of mass
            centerOfMass = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            centerOfMass.transform.parent = this.transform;
            centerOfMass.transform.position = mean;
            centerOfMass.GetComponent<Renderer>().material = main_material;
            centerOfMassOriginalPos = mean;
            centerOfMass.transform.localScale = new Vector3(1, 1, 1);

        }
        else if (state == 2)
        {
            animation_started = true;
            currentAnimationTime = 0;
        }
        else if (state == 3)
        {
            // destroy the center of mass
            Destroy(centerOfMass);
        }
        else if (state == 4)
        {
            // create a very long rectangle in a direction to simulate the direction of this pointcloud
            direction = GameObject.CreatePrimitive(PrimitiveType.Cube);
            direction.transform.parent = this.transform;
            direction.transform.localScale = new Vector3(0.2f, 15, 0.2f);
            Vector3 angle = new Vector3(90, 0, 70);
            direction.transform.rotation = Quaternion.Euler(angle);
            direction.GetComponent<Renderer>().material = main_material;

        }
        else if (state == 5)
        {
            // destroy the direction
            Destroy(direction);
        }
        else if (state == 6)
        {
            directions = new GameObject[spheres.Length][]; // create another matrix of gameobjects
            for (int i = 0; i < spheres.Length; i++)
            {
                directions[i] = new GameObject[spheres[0].Length];
            }

            // for every shpere create a long regtangle in the z direction to simulate the normal direction of the point
            for (int i = 0; i < directions.Length; i++)
            {
                for (int j = 0; j < directions[i].Length; j++)
                {
                    directions[i][j] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    directions[i][j].transform.parent = this.transform;
                    directions[i][j].transform.position = spheres[i][j].transform.position;
                    directions[i][j].GetComponent<Renderer>().material = main_material;
                    Vector3 angle = new Vector3(90, 0, 0);
                    directions[i][j].transform.rotation = Quaternion.Euler(angle);
                    directions[i][j].transform.localScale = new Vector3(0.03f, 3, 0.03f);
                }
            }

        }
        else if (state == 7)
        {
            // destroy the directions of every sphere
            for (int i = 0; i < directions.Length; i++)
            {
                for (int j = 0; j < directions[i].Length; j++)
                {
                    Destroy(directions[i][j]);
                }
            }
        }

    }
	
	// Update is called once per frame
	void Update () {

        if (animation_started)
        {

            if (state == 3)
            { // in 5 seconds do a translation, that is the distance from the center of mass of the pointcloud to the origin
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

    /**
     * Function that adds a vector to every point of the pointcloud and at the center of mass 
     * 
     */
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

