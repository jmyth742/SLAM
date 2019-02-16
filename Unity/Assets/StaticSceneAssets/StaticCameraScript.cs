using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCameraScript : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + Time.deltaTime * speed,0);
    }
}
