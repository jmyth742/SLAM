using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

    public GameObject tofCamera;
    public GameObject tofReadCamera;
    public Camera firstPersonCamera;
    public Camera pcBuilderCamera;
    public Camera overheadCamera;
    public float speed_main;
    public float speed_tof_cam;
    public float viewport_width;
    public float viewport_height;


    Miro3DCamera tofScript;
    PCBuilder tofReadScript;
    Vector3[] pointCloud;
    private int counter;
    private float timeCount;
    private Vector3 currentPosition;
    private Vector3 lastPosition;
    private Vector3 currentRotation;
    private Vector3 lastRotation;

    private void Start()
    {
        tofScript = tofCamera.GetComponent<Miro3DCamera>();
        tofReadScript = tofReadCamera.GetComponent<PCBuilder>();
        counter = 0;
        timeCount = 0;

        ShowFirstPersonView();
    }


    void Update()
    {
        if(overheadCamera.isActiveAndEnabled)
        {
            overheadCamera.transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * speed_main, 0, Space.World);
            overheadCamera.transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * speed_main);
            if (Input.GetKey(KeyCode.Q))
            {
                overheadCamera.transform.Rotate(Time.deltaTime * speed_main, 0, 0);
            }

            if (Input.GetKey(KeyCode.E))
            {
                overheadCamera.transform.Rotate(-Time.deltaTime * speed_main, 0, 0);
            }
        }
        
        else if (firstPersonCamera.isActiveAndEnabled)
        {
            currentPosition = tofCamera.transform.position;
            currentRotation = tofCamera.transform.rotation.eulerAngles;

            tofCamera.transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * speed_tof_cam, 0, Space.World);
            tofCamera.transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * speed_tof_cam);
            if (Input.GetKey(KeyCode.Q))
            {
                tofCamera.transform.Rotate(Time.deltaTime * speed_tof_cam, 0, 0);
            }

            if (Input.GetKey(KeyCode.E))
            {
                tofCamera.transform.Rotate(-Time.deltaTime * speed_tof_cam, 0, 0);
            }

            tofReadCamera.transform.rotation = tofCamera.transform.rotation;
            tofReadCamera.transform.position = new Vector3(tofCamera.transform.position.x+1000, tofCamera.transform.position.y, tofCamera.transform.position.z);

            lastPosition = currentPosition;
            lastRotation = currentRotation;

            timeCount = timeCount + Time.deltaTime;

        }

        if (Input.GetKeyDown(KeyCode.C) && overheadCamera.isActiveAndEnabled)
        {
            ShowFirstPersonView();
        }
        else if (Input.GetKeyDown(KeyCode.C) && firstPersonCamera.isActiveAndEnabled)
        {
            ShowPCBuilderView();
        }
        else if(Input.GetKeyDown(KeyCode.C) && pcBuilderCamera.isActiveAndEnabled)
        {
            ShowOverheadView();
        }

    }

    public void ShowOverheadView()
    {
        firstPersonCamera.enabled = false;
        pcBuilderCamera.enabled = false;
        overheadCamera.enabled = true;
        tofScript.capturing_active = false;
    }

    public void ShowFirstPersonView()
    {
        pcBuilderCamera.rect = new Rect(0.01f, 0.01f, 0.2f, 0.2f);
        firstPersonCamera.enabled = true;
        pcBuilderCamera.enabled = true;
        overheadCamera.enabled = false;
        tofScript.capturing_active = true;
    }

    public void ShowPCBuilderView()
    {
        pcBuilderCamera.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        firstPersonCamera.enabled = false;
        pcBuilderCamera.enabled = true;
        overheadCamera.enabled = false;
        tofScript.capturing_active = false;
    }

}
