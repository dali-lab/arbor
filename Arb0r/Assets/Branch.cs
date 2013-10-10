using UnityEngine;
using System.Collections;

public class Branch : MonoBehaviour {
	public GameObject node1;
	public GameObject node2;
	Transform tf;
	public GameObject cyl;

	// Use this for initialization
	void Start () {
		node1.transform.LookAt(node2.transform.localPosition);
		cyl = (GameObject)Instantiate(cyl);;
		Vector3 rot = new Vector3(node1.transform.rotation.x + 90, node1.transform.rotation.y, node1.transform.rotation.z);
		cyl.transform.rotation = node1.transform.rotation;
		//cyl.transform.Rotate(cyl.transform.forward, 90 );
		cyl.transform.forward = cyl.transform.up;

		
	}
	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance(node1.transform.position, node2.transform.position);
		Vector3 vec = new Vector3(1,1, distance/3);
		cyl.transform.localScale = vec;
		Vector3 difPos = new Vector3((node2.transform.position.x - node1.transform.position.x)/2, (node2.transform.position.y  - node1.transform.position.y)/2, (node2.transform.position.z - node1.transform.position.z)/2);
		cyl.transform.position = difPos;
		node2.transform.LookAt(node1.transform);
		cyl.transform.LookAt(node2.transform, Vector3.up);
	
	}
}
