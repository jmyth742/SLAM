using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCameraManager : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;
    public Camera cam3;

    // Start is called before the first frame update
    void Start()
    {
        cam1.enabled = false;
        cam2.enabled = true;
        cam3.enabled = true;
        cam1.transform.position = new Vector3(-580, 400, -700);
        cam2.transform.position = new Vector3(0, 0, 0);
        cam3.transform.position = new Vector3(-1000, -5, 0);
        cam3.rect = new Rect(0.01f, 0.01f, 0.2f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(cam1.enabled == true)
            {
                cam3.rect = new Rect(0.01f, 0.01f, 0.2f, 0.2f);
                cam1.enabled = false;
                cam2.enabled = true;
                cam3.enabled = true;
            } else if( cam2.enabled == true)
            {
                cam3.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
                cam1.enabled = false;
                cam2.enabled = false;
                cam3.enabled = true;
            } else
            {
                cam1.enabled = true;
                cam2.enabled = false;
                cam3.enabled = false;
            }
        }
    }
}
