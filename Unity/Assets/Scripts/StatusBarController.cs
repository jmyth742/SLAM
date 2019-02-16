using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusBarController : MonoBehaviour {

    public GameObject tofCamera;
    
    public TMP_Text tx;
    public TMP_Text ty;
    public TMP_Text tz;
    public TMP_Text rx;
    public TMP_Text ry;
    public TMP_Text rz;

    private Vector3 initialPosition;
    private Vector3 initialRotation;

    void Init()
    {
        
    }

    void Start()
    {
        initialPosition = tofCamera.transform.position;
        initialRotation = tofCamera.transform.rotation.eulerAngles;
    }

    void Update () {
        float xpos = tofCamera.transform.position.x - initialPosition.x;
        float ypos = tofCamera.transform.position.y - initialPosition.y;
        float zpos = tofCamera.transform.position.z - initialPosition.z;
        float xrot = tofCamera.transform.rotation.eulerAngles.x - initialRotation.x;
        float yrot = tofCamera.transform.rotation.eulerAngles.y - initialRotation.y;
        float zrot = tofCamera.transform.rotation.eulerAngles.z - initialRotation.z;

        tx.text = xpos.ToString("00.00");
        ty.text = ypos.ToString("00.00");
        tz.text = zpos.ToString("00.00");
        rx.text = xrot.ToString("00.00");
        ry.text = yrot.ToString("00.00");
        rz.text = zrot.ToString("00.00");

        
    }
}
