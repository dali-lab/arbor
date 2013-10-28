using UnityEngine;
using System.Collections;
using System.Linq;

public class TreeBuilder : MonoBehaviour {
	public GameObject cyl;
	SortedList<string, Node> nameXnode = new SortedList<string,Node>();
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Branch(Node node1, Node node2) {
		node1.transform.LookAt(node2.transform.localPosition);
		cyl = (GameObject)Instantiate(cyl);
		Vector3 rot = new Vector3(node1.transform.rotation.x + 90, node1.transform.rotation.y, node1.transform.rotation.z);
		cyl.transform.rotation = node1.transform.rotation;
		cyl.transform.forward = cyl.transform.up;
	}
	
	
}
