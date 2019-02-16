using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCameraScript : MonoBehaviour
{
    public float speed;
    public float max_speed;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * speed*5, 0, Space.World);
        transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * speed );

        if (rb.velocity.x > max_speed)
        {
            float yvel = rb.velocity.y;
            float zvel = rb.velocity.z;
            rb.velocity = new Vector3(max_speed, yvel,zvel);
        } else if (rb.velocity.x < -max_speed)
        {
            float yvel = rb.velocity.y;
            float zvel = rb.velocity.z;
            rb.velocity = new Vector3(-max_speed, yvel, zvel);
        }
        
        if (rb.velocity.y > max_speed)
        {
            float xvel = rb.velocity.x;
            float zvel = rb.velocity.z;
            rb.velocity = new Vector3(xvel, max_speed, zvel);
        } else if(rb.velocity.y < -max_speed)
        {
            float xvel = rb.velocity.x;
            float zvel = rb.velocity.z;
            rb.velocity = new Vector3(xvel, -max_speed, zvel);
        }
        if (rb.velocity.z > max_speed)
        {
            float xvel = rb.velocity.x;
            float yvel = rb.velocity.y;
            rb.velocity = new Vector3(xvel, yvel, max_speed);
        } else if(rb.velocity.z < -max_speed)
        {
            float xvel = rb.velocity.x;
            float yvel = rb.velocity.y;
            rb.velocity = new Vector3(xvel, yvel, -max_speed);
        }

        if(Input.GetKey(KeyCode.Space)) {
            rb.velocity = new Vector3(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Time.deltaTime * speed, 0, 0);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(-Time.deltaTime * speed, 0, 0);
        }
    }
}
