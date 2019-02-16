using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	public float speed = 150.0f;

	// Update is called once per frame
	void Update () {
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

		transform.Rotate(0, x, 0, Space.World);
		transform.Translate(0, 0, z);
	}

	

}
