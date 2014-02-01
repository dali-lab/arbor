using UnityEngine;
using System.Collections;
using System.Linq;

public class TreeBuilder : MonoBehaviour {
	
	//SortedList<string, Node> nameXnode = new SortedList<string, Node>();
	Hashtable nameXnode = new Hashtable();
	Hashtable nameXGameObj = new Hashtable();
	public ArrayList relations = new ArrayList();
	//public string[] relations;
	private string string1;
	private string string2;

	// Use this for initialization
	void Start () {
		
		//TESTING ... follow the pattern to mock up a sweet tree!
		Vector3 pos1 = new Vector3(4,8,8);
		Vector3 pos2 = new Vector3(4,4,8);
		Vector3 pos3 = new Vector3(8,8,10);
		Vector3 pos4 = new Vector3(8,4,10);
		Vector3 pos5 = new Vector3(2,2,10);
		Vector3 pos6 = new Vector3(1,4,2);
		Vector3 pos7 = new Vector3(4,5,0);
		Vector3 pos8 = new Vector3(4,1,1);
		Vector3 pos9 = new Vector3(1,1,0);
	
		
		string relation1 = "sp1/sp2";       
		string relation2 = "sp2/nodeA";
		string relation3 = "sp3/sp4";
		string relation4 = "sp4/sp5";
		string relation5 = "sp5/nodeC";
		string relation6 = "nodeA/sp3";
		
		
		relations.Add(relation1);
		relations.Add(relation2);
		relations.Add(relation3);
		relations.Add(relation4);
		relations.Add(relation5);
		relations.Add(relation6);
		
		Node n1 = new Node("sp1" , pos1);
		Node n2 = new Node("sp2" , pos2);
		Node n3 = new Node("sp3" , pos3);
		Node n4 = new Node("sp4" , pos4);
		Node n5 = new Node("sp5" , pos5);
		Node n6 = new Node("nodeA" , pos6);
		Node n7 = new Node("nodeB" , pos7);
		Node n8 = new Node("nodeC" , pos8);
		Node n9 = new Node("nodeD" , pos9);
		
		nameXnode[n1.name] = n1;
		nameXnode[n2.name] = n2;
		nameXnode[n3.name] = n3;
		nameXnode[n4.name] = n4;
		nameXnode[n5.name] = n5;
		nameXnode[n6.name] = n6;
		//nameXnode[n7.name] = n7;
		nameXnode[n8.name] = n8;
		//nameXnode[n9.name] = n9;

		//End of Test
		
		int index = 0;
		ICollection Nodelist = nameXnode.Values;
		foreach( Node n in Nodelist ) { // loop that loads all shapes using the filename
			GameObject shape = (GameObject)Instantiate(Resources.Load(n.name));
			shape.transform.position = n.location;
			shape.transform.localScale = new Vector3(20,20,20);
			nameXGameObj[n.name] = shape;
			//shape.transform.rotation = n.dummy.transform.rotation;
			
		}
		
		
		foreach(string str in relations){ // loop that parses all the relations from relations[]
			int l = str.IndexOf("/");
			print (l);
			string1 = str.Substring(0,l);
			string2 = str.Substring(l+1);
			branch((GameObject)nameXGameObj[string1],(GameObject)nameXGameObj[string2]);
		}

				
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void branch(GameObject node1, GameObject node2) { // connects all relationships with a cylinder
		node1.transform.LookAt(node2.transform.localPosition);
		GameObject c = (GameObject)Instantiate(Resources.Load("cylz"));
		Vector3 rot = new Vector3(node1.transform.rotation.x + 90, node1.transform.rotation.y, node1.transform.rotation.z);
		c.transform.rotation = node1.transform.rotation;
		c.transform.position = (node2.transform.position + node1.transform.position)/2;
		c.transform.forward = c.transform.up;
		int yScale = (int)(Vector3.Distance(node1.transform.position,node2.transform.position)/2.1);
		Vector3 scale = new Vector3(1,yScale,1);
		c.transform.localScale = scale;
		//Transform t = c.GetComponentInChildren<Transform>();
		//t.localScale.y = (int)Vector3.Distance(node1.transform.position,node2.transform.position);
	}
	
	
}
