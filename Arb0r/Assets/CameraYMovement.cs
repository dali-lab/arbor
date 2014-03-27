using UnityEngine;
using System.Collections;

public class CameraYMovement : MonoBehaviour {
	
	
	float speed = 5.0f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float yDir = 0;
		if(Input.GetKey(KeyCode.Q)) yDir = 1;
		if(Input.GetKey(KeyCode.E)) yDir = -1;
		transform.position += transform.up * yDir * speed * Time.deltaTime;
	
	}
}
