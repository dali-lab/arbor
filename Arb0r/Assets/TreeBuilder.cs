using UnityEngine;
using System.Collections;
using System.Linq;

public class TreeBuilder : MonoBehaviour {
	
	//SortedList<string, Node> nameXnode = new SortedList<string, Node>();
	Hashtable nameXpos;
	Hashtable nameXGameObj = new Hashtable();
	public ArrayList relations = new ArrayList();
	ArrayList nameList = new ArrayList();
	ArrayList posList = new ArrayList(); //TEMPORARY
	//public string[] relations;
	private string string1;
	private string string2;
	private Vector3 posScale = new Vector3(4,2,4);


	// Use this for initialization

	public Hashtable getNameXPos(){
		return nameXpos;
	}
	
	void Start () {


		//here's how to create a parser
		Parser p = (Parser) GameObject.FindObjectOfType(typeof(Parser)); // returns the first object of this type
		if (p == null) {
			p = gameObject.AddComponent<Parser>();
			Debug.Log("MADE A NEW PARSER");
		}

	//	Object.DontDestroyOnLoad (p);


	//	p.readFromFile("/Users/nook/Documents/git/arbor/Arb0r/Assets/data2.dta");
		p.testPublicFunctions();
		
		relations = p.getRelations();
		nameXpos = p.getNameXPos();
		
		foreach(string str in relations){ // loop that parses all the relations from relations[]
			int l = str.IndexOf("/");
			string1 = str.Substring(0,l);
			string2 = str.Substring(l+1);
			
			if(!nameList.Contains(string1)){
				nameList.Add(string1);
				this.makeNode(string1);
				
			}
			if(!nameList.Contains(string2)){
				nameList.Add(string2);
				this.makeNode(string2);
				
			}
			branch((GameObject)nameXGameObj[string1],(GameObject)nameXGameObj[string2]);
			
		}

		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void branch(GameObject node1, GameObject node2) { // connects all relationships with a cylinder
		
		node1.transform.LookAt(node2.transform.localPosition);
		GameObject c = (GameObject)Instantiate(Resources.Load("cyl2"));
	//	GameObject c2 = (GameObject)Instantiate(Resources.Load("cyl2"));
		Vector3 rot = new Vector3(node1.transform.rotation.x + 90, node1.transform.rotation.y, node1.transform.rotation.z);
		c.transform.rotation = node1.transform.rotation;
		c.transform.position = (node2.transform.position + node1.transform.position)/2;
		c.transform.forward = c.transform.up;
		int yScale = (int)(Vector3.Distance(node1.transform.position,node2.transform.position) * 4);
		Vector3 scale = new Vector3(5,yScale,5);
		c.transform.localScale = scale;
		
		/*
		float halfY = (node2.transform.position.y - node1.transform.position.y)/2;
		Vector3 c2Pos = new Vector3(node2.transform.position.x, halfY, node2.transform.position.z);
		c2.transform.position = c2Pos;
		*/
		
		/*
		GameObject co = (GameObject)Instantiate(Resources.Load("co"));
		co.transform.rotation = c.transform.rotation;
		co.transform.position = c.transform.position;
		*/
		
		
		/*
		GameObject ps = (GameObject)Instantiate(Resources.Load("ps"));
		ps.transform.rotation = c.transform.rotation;
		ps.transform.position = (node2.transform.position + co.transform.position)/2;
		*/
		
		//co.transform.RotateAround(Vector3.up,180);
		
		
		//co.transform.position = co.transform.position * (15/16);
		//co.transform.forward = co.transform.up;
	
		//Transform t = c.GetComponentInChildren<Transform>();
		//t.localScale.y = (int)Vector3.Distance(node1.transform.position,node2.transform.position);
	}
	
	void makeNode(string str) {
		GameObject shape = (GameObject)Instantiate(Resources.Load(str));
		Node node = shape.gameObject.AddComponent<Node>();
		node.name = str;
		Vector3 pos = (Vector3)nameXpos[str];
		pos.Scale(posScale);
		node.location = pos;
		shape.transform.position = node.location;
		//shape.transform.localScale = new Vector3(20,20,20);
		nameXGameObj[str] = shape;
		shape.AddComponent<SphereCollider>();
	}
	
	
}
