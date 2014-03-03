using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Parser
{
	//relationships between nodes, in the format: "child/parent"
	private ArrayList relations = new ArrayList ();
	//names describing the names associated with the list of values
	private string[] variableNames;
	
	private Hashtable nameXpos = new Hashtable();
	private ArrayList nodeNames = new ArrayList ();
	//probably unnecessary
	private string fileName;
	private string fileContents;
	private string newickString;
	//private string nodePos;
	//a dictionary of varibale positions for each node. 
	private Dictionary<string, float[] > dictionary = new Dictionary<string, float[]> ();
	void Start ()
	{

	}
	
	void Update ()
	{
	
	}
	
	public void readFromFile (string s)
	{
		
		var sr = new StreamReader (Application.dataPath + "/" + s);
		sr.ReadLine ();
		newickString = sr.ReadLine ();

		string str = sr.ReadLine ();
		while (str!=null) {
			categorize (str);
			str = sr.ReadLine ();
		}
		sr.Close ();
		parseRelations (newickString);
	}

	public void testPublicFunctions ()
	{
		foreach (string relation in getRelations()) {
			Debug.Log ("RELATION: " + relation);
		}
		foreach (string element in getNodeNames()) {
			//Debug.Log ("ELEMENT: " + element);
			Vector3 pos = getPositionOfNodeWithVariableIndices (element, 0, 1);
			//Debug.Log ("\tPos: " + pos);
			nameXpos.Add(element,pos);
		}
		foreach (string variable in getVariableNames()) {
			Debug.Log ("VARIABLE: " + variable);
		}

	}
	
	public ArrayList getVariableNames ()
	{
		ArrayList list = new ArrayList (variableNames);
		return list;
	}
	public ArrayList getRelations ()
	{
		return relations;
	}
	public ArrayList getNodeNames ()
	{
		return nodeNames;
		
	}
	public Hashtable getNameXPos ()
	{
		return nameXpos;
	}
	
	//gets the position of a node given the non-time variables we want to plot it with
	public Vector3 getPositionOfNodeWithVariableIndices (string node, int variableIndex, int variableIndex2)
	{
		
		float [] returnee = null;
		dictionary.TryGetValue (node, out returnee);
		//ArrayList r = new ArrayList();
	
		Vector3 r = new Vector3 (returnee [variableIndex], timePositionForNode (node), returnee [variableIndex2]);
		return r; 
	}
	private float timePositionForNode (string s)
	{
		float accum = 0.0f;
		accum += lengthForNode (s); 
		string s2 = ParentForNodeInRelations (s);
		while (s2 !=null) {
			accum += lengthForNode (s2);
			s2 = ParentForNodeInRelations (s2);
		}
		return accum;
	}
	private void parseRelations (string s)
	{


		foreach (string element in nodeNames) {

			string parent = ParentForNode (element);
			if (parent != "") {
				relations.Add (parent + "/" + element);
				//	Debug.Log(parent+"/"+element);
			}
		}
	}
	private string ParentForNodeInRelations (string s)
	{
		foreach (string relation in relations) {
			string [] strs = relation.Split ('/');
			if (strs.Length > 1) {
				if (strs [1] == s) {
					return strs [0];
				}
			}
		}
		return null;
	
	}
	private string ParentForNode (string s)
	{
		
		char [] letters = newickString.ToCharArray ();

		int i = (newickString.IndexOf (s) + s.Length);
		//	Debug.Log ("INDEX OF NODE END "+i);
		string parent = "";

		int leftParensCount = 0;
		int rightParensCount = 0;
		while (i<letters.Length) {
			//	Debug.Log ("ITERATING OVER INDEX "+i);
			if (letters [i] == ')') {
				rightParensCount++;
				//Debug.Log ("INDEX OF )"+i);
			}
			if (letters [i] == '(') {
				leftParensCount++;
			}
			if (rightParensCount > leftParensCount) {
				int j = i + 1;
				while (letters[j] != ':' && letters[j] != ';') {
					//Debug.Log ("PROCCESSING INDEX "+j+"FROM START "+i+"    "+letters[j]);
					parent += letters [j];
					j++;
				}
				break;
			}
			i++;
		}
		return parent;

	}
	//nodeNames[i];

	private float lengthForNode (string s)
	{
		if (s == null)
			return 0.0f;
		int index = newickString.IndexOf (s);
		if (index == 0)
			return 0.0f;
		//Debug.Log ("NEWICKBEFORENODE "+ newickString);
		//	Debug.Log ("INDEXFORNODE " + index);
		string stringAfterNode = newickString.Substring (index + 1 + s.Length);

		char [] letters = stringAfterNode.ToCharArray ();
		int i = 0;		
		string toFloat = "";
		if (letters.Length == 0)
			return 0.0f;

		//Debug.Log ("RAWR letters "+letters[0]);
		//if()

		while (letters[i]!=')'&&letters[i]!='('&&letters[i]!=','&&letters[i]!=';') {
			toFloat += letters [i];
			i++;
		}
		if (toFloat == "")
			return 0.0f;
		else
			return float.Parse (toFloat);

	}
	//read non-newick lines and parse out variable names/

	private void categorize (string s)
	{
		string[] words = s.Split (' ');
		int l;
		string [] variableNamesTemp;
		if (variableNames != null) {
			l = variableNames.Length;
		} else
			l = 0;

		variableNamesTemp = new string [l + 1];
		//if a line is a variable name
		if (!(words.Length > 2)) {
			if (variableNames != null)
				variableNames.CopyTo (variableNamesTemp, 0); 
			variableNamesTemp [l] = s;
			variableNames = variableNamesTemp;
		} else {
			float [] floats = new float[(words.Length - 1)];


			//parse the floats :)
			for (int i =1; i<words.Length; i++) {
				floats [i - 1] = float.Parse (words [i]);
			}
			nodeNames.Add (words [0]);
			dictionary.Add (words [0], floats);
			//Debug.Log("NODE NAME: "+words[0]+" POSITION: "+floats[0]+" "+floats[1]);
		}
	}

	// returns a list of positions associated with a node
	private float [] getPositionsForNode (string s)
	{

		float [] returnee = null;
		dictionary.TryGetValue (s, out returnee);
		return returnee; 
	}



}