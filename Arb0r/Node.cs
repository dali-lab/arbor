using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {
	Vector3 location = null;
	string name = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public Node(string name, Vector3 vec) {
		name = name;
		location = vec;
	}
}
