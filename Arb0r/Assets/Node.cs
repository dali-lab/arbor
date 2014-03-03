using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {
	//public GameObject dummy; // have to use gameobject to make branch function work 
	public string name;
	public Vector3 location;
	CameraControl cc;
	MiniCam mc;
	
	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag("MainCamera");
		cc = (CameraControl) go.GetComponent(typeof(CameraControl));
		GameObject go2 = GameObject.FindGameObjectWithTag("Camera");
		mc = (MiniCam) go2.GetComponent(typeof(MiniCam));
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
	
	void OnMouseDown () {
		//BroadcastMessage("getTarget",location, SendMessageOptions.DontRequireReceiver);
		//cc.getTarget(location);
		mc.getModel(name);
		print(name);
	}
	

}
