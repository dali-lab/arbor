using UnityEngine;
using System.Collections;
//using UnityEditor;
//parser stuff

public class OpenFile : MonoBehaviour {

	private Parser p;

	string path;

	int smallBuff;
	int buttonHeight;

	private Vector2 scrollPosition;
	public ArrayList dummyArray;// = new string[] {"weight", "height", "color", "temp", "geography", "LOL", "HAHAH", "Phineaus Gage", "Sinbad", "I'm a wizard"};

	bool drawXMenu = false;
	bool drawYMenu = false;

	bool isInstructions = false;

	public GUIStyle btnLoadStyle = new GUIStyle();
	public GUIStyle btnSelXStyle = new GUIStyle();
	public GUIStyle btnSelYStyle = new GUIStyle();
	public GUIStyle btnGOStyle = new GUIStyle();
	public GUIStyle bgMenu = new GUIStyle();
	public GUIStyle bgInst = new GUIStyle();
	public Texture bgInstructions;

	int WIDTH;
	int HEIGHT;

	int btnLoad_x;
	int btnLoad_y;
	int btnLoad_W;
	int btnLoad_H;

	int btnSelX_x;
	int btnSelX_y;
	int btnSelX_W;
	int btnSelX_H;

	int btnSelY_x;
	int btnSelY_y;
	int btnSelY_W;
	int btnSelY_H;

	int btnGO_x;
	int btnGO_y;
	int btnGO_W;
	int btnGO_H;

	string xVar;
	string yVar;
	
	public FileBrowser myFileBrowser;



	// Use this for initialization
	void Start () {

		p = (Parser) GameObject.FindObjectOfType(typeof(Parser)); // returns the first object of this type
		if (p == null) {
			p = gameObject.AddComponent<Parser>();
			Debug.Log("MADE A NEW PARSER");
		}
	//	Object.DontDestroyOnLoad (p);
		
		WIDTH = Screen.width;
		HEIGHT = Screen.height;

		scrollPosition = Vector2.zero;

		xVar = "";
		yVar = "";
		path = "";


		//myFileBrowser = this.GetComponent<FileBrowser>();
		myFileBrowser.fileMasks = "*.dta";
	
	}
	
	// Update is called once per frame
	void Update () {
		WIDTH = Screen.width;
		HEIGHT = Screen.height;

		buttonHeight = (int) (0.056 * HEIGHT);
		smallBuff = (int) (HEIGHT / 256);

		//Load Data button measurements
		btnLoad_x = (int) (WIDTH * 0.439);
		btnLoad_y = (int) (HEIGHT * 0.494);
		btnLoad_W = (int) (WIDTH * 0.123);
		btnLoad_H = (int) (HEIGHT * 0.057);

		//Select X
		btnSelX_x = (int) (WIDTH * 0.205);
		btnSelX_y = (int) (HEIGHT * 0.61);
		btnSelX_W = (int) (WIDTH * 0.235);
		btnSelX_H = (int) (HEIGHT * 0.056);


		//Select Y
		btnSelY_x = (int) (WIDTH * 0.563);
		btnSelY_y = (int) (HEIGHT * 0.61);
		btnSelY_W = (int) (WIDTH * 0.235);
		btnSelY_H = (int) (HEIGHT * 0.056);

		//Go
		btnGO_x = (int) (WIDTH * 0.471);
		btnGO_y = (int) (HEIGHT * 0.696);
		btnGO_W = (int) (WIDTH * 0.059);
		btnGO_H = (int) (HEIGHT * 0.107);
	}

	void OnGUI (){


		GUI.Box(new Rect(-15,0, Screen.width, Screen.height), "", bgMenu);




		if (drawXMenu == true)
		{
			drawXVarMenu();
		}

		if (drawYMenu == true)
		{
			drawYVarMenu ();
		}


		if (GUI.Button(new Rect(btnLoad_x, btnLoad_y, btnLoad_W, btnLoad_H), "", btnLoadStyle))
		{
//			Debug.Log(myFileBrowser.ShowBrowser("Choose a Data File",  callback));
			myFileBrowser.ShowBrowser("Choose a Data File",  callback);
			Debug.Log ("REACHED SHOWBROWSER");
//		 	path = EditorUtility.OpenFilePanel(
//				"Select txt file for parsing",
//				"",
//				"dta");
//			Debug.Log (path);
//			p.clearData ();
//			p.readFromFile (path);
//			dummyArray = p.getVariableNames();


		}

		if (path != "")
		{
			if (GUI.Button (new Rect(btnSelX_x, btnSelX_y, btnSelX_W, btnSelX_H), "", btnSelXStyle))
				{
				drawXMenu = true;
				}

			if (GUI.Button (new Rect(btnSelY_x, btnSelY_y, btnSelY_W, btnSelY_H), "", btnSelYStyle))
				{
				drawYMenu = true;
				}
		}

		if (xVar != "" && yVar != ""){
			if (GUI.Button (new Rect(btnGO_x, btnGO_y, btnGO_W, btnGO_H), "", btnGOStyle))
			{
				loadMainLevel ();
			}
		}
		showInstructions ();
	}


	void callback(string filename) {
		Debug.Log ("REACHED CLLBACK");
		path = filename;
		Debug.Log (path);
		p.clearData ();
		p.readFromFile (path);
		dummyArray = p.getVariableNames();

	}

	void drawXVarMenu (){
		
		scrollPosition = GUI.BeginScrollView (
			new Rect( 
		         (int)(0.204 * WIDTH),		 //X
				 (int)(0.663 * HEIGHT), 												//Y
		 		((int)(0.236 * WIDTH) + 20), 						//W
				 (int)(0.283 * HEIGHT)						//H
		 ),
		         
		         scrollPosition, 
		         
		         new Rect(
			0,
			0, 
			(int)(0.236 * WIDTH),
			(dummyArray.Count*(buttonHeight + smallBuff) + smallBuff)
			)
		         );
			
			GUI.Box (new Rect(0,0,btnSelX_W, dummyArray.Count*(smallBuff+buttonHeight)+smallBuff), "");
			
			//scrollable box.
			for (int i = 0; i < dummyArray.Count; i++){
			if(GUI.Button (new Rect(smallBuff, (smallBuff + i*(buttonHeight + smallBuff)), btnSelX_W, buttonHeight), (string)(dummyArray[i])))
			{
				xVar = (string)dummyArray[i];
				p.setVariable1Name(xVar);
				drawXMenu = false;

				
			}
			
		}
		
		GUI.EndScrollView ();
		
		
		}

	void drawYVarMenu ()
	{
		
		scrollPosition = GUI.BeginScrollView (
			new Rect( 
		         (int)(0.562 * WIDTH),		 //X
		         (int)(0.663 * HEIGHT), 												//Y
		         ((int)(0.236 * WIDTH) + 20), 						//W
		         (int)(0.283 * HEIGHT)						//H
		         ),
			
			scrollPosition, 
			
			new Rect(
			0,
			0, 
			(int)(0.236 * WIDTH),
			(dummyArray.Count*(buttonHeight + smallBuff) + smallBuff)
			)
			);
		
		GUI.Box (new Rect(0,0,btnSelX_W, dummyArray.Count*(smallBuff+buttonHeight)+smallBuff), "");
		
		//scrollable box.
		for (int i = 0; i < dummyArray.Count; i++){
			if(GUI.Button (new Rect(smallBuff, (smallBuff + i*(buttonHeight + smallBuff)), btnSelX_W, buttonHeight), (string)(dummyArray[i])))
			{
				yVar = (string)dummyArray[i];
				drawYMenu = false;
				p.setVariable2Name(yVar);
				Debug.Log(p.getVariable1Name()+ " " + p.getVariable2Name());

				
			}
			
		}
		
		GUI.EndScrollView ();

	}

	void loadMainLevel()
	{
		Application.LoadLevel("Tree"); //Just replace "mainLevel" with the name of the scene.
	}

	void showInstructions()
	{
		if(!isInstructions){
			if (GUI.Button (new Rect (Screen.width - 150, 10, 100, 20), "Instructions")){
				isInstructions = !isInstructions;
			}
		}
		
		
		if(isInstructions){
			GUI.Box (new Rect(0,0,Screen.width, Screen.height), "", bgInst);
			//GUI.Label(new Rect(0,0,Screen.width, Screen.height), bgInstructions);
			if(GUI.Button (new Rect (Screen.width - 150, 10 , 100, 20), "Close")){
				isInstructions = !isInstructions;
			}
		}

	}


}
