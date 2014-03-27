using UnityEngine;
using System.Collections;

public class MainCameraToggle : MonoBehaviour {
	
	bool mouseMoveEnabled = true;
	bool pointAndClick = false;
	string label = "Camera Enabled";
	WindowDrag wd;
	int xSide;
	
	// Use this for initialization
	
	void OnGUI(){
		GUI.Box(new Rect(xSide,Screen.height - 30, 120, 30 - xSide), label);
	}
	
	
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag("MainCamera");
		wd = (WindowDrag)go.GetComponent<WindowDrag>();
		xSide = wd.sideX;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			mouseMoveEnabled = !mouseMoveEnabled;
			this.GetComponent<MouseLook>().enabled = mouseMoveEnabled;
			//this.GetComponent<CharacterMotor>().enabled = mouseMoveEnabled;

			if(mouseMoveEnabled == true)
				label = "Camera Enabled";
		
			if(mouseMoveEnabled == false)
				label = "Camera Disabled";
	
		}
		/*
		if(Input.GetKey(KeyCode.LeftShift)) {
			pointAndClick = !pointAndClick;
			this.GetComponent<CameraControl>().enabled = pointAndClick;
			
			if(pointAndClick == true)
				print("Using Point and Click");
			if(pointAndClick == false)
				print("Using First Person Controls");
		}
		*/	
	}
	
}
