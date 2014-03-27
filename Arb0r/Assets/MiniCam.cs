using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MiniCam : MonoBehaviour {
	
	//public Camera tempCam;
	bool isWindowFull = false;
	List<GameObject> models = new List<GameObject>();
	int windowLimit = 4;
	GUISkin frame;
	//GUIText guiText = new GUIText();
	string modelName = "";
	bool cameraOn = true;
	ArrayList posList = new ArrayList();
	ArrayList labelList = new ArrayList();
	public GUIText guiText;
	int heightUnit = Screen.height/4;
	int widthUnit = Screen.width/16;
	//Vector3 scale = new Vector3(.25f,.5f,.25f);
	Hashtable nameXpos;
	float speed = Screen.height/4;
	Vector3 mouseVec;
	float xVal = (float)(.7f * Screen.width);
	float yVal = (float)(Screen.height/4);
	float wVal = (float)(.3f * Screen.width);
	float hVal = (float)(Screen.height/4);
	
	Rect cam1;
	Rect cam2;
	Rect cam3;
	Rect cam4;
	
	int modelIndex = -1;
	bool isRotating = false;
	Parser p;

	
	// Use this for initialization
	void Start () {
		//frame = (GUISkin)Resources.Load("frame");
		
		cam1 = new Rect(xVal, yVal * 3, wVal, hVal);
		cam2 = new Rect(xVal, yVal * 2, wVal, hVal);
		cam3 = new Rect(xVal, yVal, wVal, hVal);
		cam4 = new Rect(xVal, .005f, wVal, hVal);
		
		Vector3 pos1 = new Vector3(100,100,101);
		Vector3 pos2 = new Vector3(-100,100,101);
		Vector3 pos3 = new Vector3(100,-100,101);
		Vector3 pos4 = new Vector3(-100,-100,101);
		
		posList.Add(pos1);
		posList.Add(pos2);
		posList.Add(pos3);
		posList.Add(pos4);
		
		//here's how to create a parser
		p = (Parser) GameObject.FindObjectOfType(typeof(Parser)); // returns the first object of this type
		if (p == null) {
			p = gameObject.AddComponent<Parser>();
			Debug.Log("MADE A NEW PARSER");
		}

		//p.readFromFile("data2.dta");
		//p.testPublicFunctions();
		nameXpos = p.getNameXPos();
		foreach(string name in nameXpos.Keys){
			print(nameXpos[name] + " -- " + name);	
		}

		
		
	}
	
	void OnGUI() {
		if(cameraOn == true) {
			guiText.text = modelName;
		}
		GameObject m;
		if(models.Count >= 1){
			m = models[0];
			string m1 = parseName(m.name);
			GUI.Label(new Rect(widthUnit * 15, 10, 100, 20), m1);
			Vector3 pos = (Vector3)nameXpos[m1];
			GUI.Label(new Rect(widthUnit * 11.5f, 10, 100, 200), p.getVariable1Name()+": " + pos.x + "\nTime: " + pos.y + "\n"+p.getVariable2Name()+": " + pos.z);
			if(models.Count >= 2) {
				m = models[1];
				string m2 = parseName(m.name);
				GUI.Label(new Rect(widthUnit * 15, heightUnit, 100, 20), m2);
				pos = (Vector3)nameXpos[m2];
				GUI.Label(new Rect(widthUnit * 11.5f, heightUnit, 100, 200), p.getVariable1Name()+": " + pos.x + "\nTime: " + pos.y + "\n"+p.getVariable2Name()+": " + pos.z);
				if(models.Count >= 3) {
					m = models[2];
					string m3 = parseName(m.name);
					GUI.Label(new Rect(widthUnit * 15, heightUnit * 2, 100, 20), m3);
					pos = (Vector3)nameXpos[m3];
					GUI.Label(new Rect(widthUnit * 11.5f, (heightUnit * 2), 100, 200), p.getVariable1Name()+": " + pos.x + "\nTime: " + pos.y + "\n"+p.getVariable2Name()+": " + pos.z);
					if(models.Count == 4) {
						m = models[3];
						string m4 = parseName(m.name);
						GUI.Label(new Rect(widthUnit * 15, heightUnit * 3, 100, 20), m4);
						pos = (Vector3)nameXpos[m4];
						GUI.Label(new Rect(widthUnit * 11.5f, (heightUnit * 3), 100, 200), p.getVariable1Name()+": " + pos.x + "\nTime: " + pos.y + "\n"+p.getVariable2Name()+": " + pos.z);					}
				}
			}
		}
		
		
	}
	
	string parseName(string name){
		string goodName;
		int s = name.IndexOf("(");
		goodName = name.Substring(0,s);
		return goodName;	
	}
	
	//used for loading model into new frame
	public void getModel(string name) {
		
			/*
			if(!isWindowFull) {
				cameraOn = true;
				isWindowFull = true;
			}
			
			*/
		if(models.Count >= windowLimit){
			GameObject m = models[models.Count - 1];
			Destroy(m);
			models.RemoveAt(windowLimit - 1);
		}
		GameObject model = (GameObject)Instantiate(Resources.Load(name));
		modelName = name;
		models.Insert(0,model);
		int posIndex = 0;
		foreach(GameObject m in models){
			m.transform.position = (Vector3)posList[posIndex];
			posIndex++;
		}
	}	
	
	// Update is called once per frame
	void Update () {
		this.camera.enabled = cameraOn;	
		
		if(!isRotating && Input.GetMouseButtonDown(0)){
			if(cam1.Contains(Input.mousePosition)){
				modelIndex = 0;	
			}
			if(cam2.Contains(Input.mousePosition)){
				modelIndex = 1;		
			}
			if(cam3.Contains(Input.mousePosition)){
				modelIndex = 2;		
			}
			if(cam4.Contains(Input.mousePosition)){
				modelIndex = 3;		
			}
		}
		
		
		if(Input.GetMouseButton(0)){
			mouseVec = new Vector3(Input.GetAxis("Mouse X") , Input.GetAxis("Mouse Y"), 0);
			print ("MOUSE VEC: " + mouseVec);
			if(modelIndex != -1 && modelIndex < models.Count){
			GameObject go = (GameObject)models[modelIndex];
			go.transform.Rotate(-mouseVec, Time.deltaTime * speed);
			isRotating = true;
			}
		}
		
		if(Input.GetMouseButtonUp(0)){
			isRotating = false;
			modelIndex = -1;
		}
			

	
	}
}
