using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {
	//public GameObject dummy; // have to use gameobject to make branch function work 
	public string name;
	public Vector3 location;
	// Use this for initialization
	void Start () {
		//dummy = new GameObject();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public Node(string filename, Vector3 loc) {
		name = filename;
		location = loc;
		//dummy = new GameObject(name);
		//dummy.transform.position = loc;
	}
}
