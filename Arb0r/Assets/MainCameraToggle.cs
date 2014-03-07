using UnityEngine;
using System.Collections;

public class MainCameraToggle : MonoBehaviour {
	
	bool mouseMoveEnabled = true;
	bool pointAndClick = false;
	int width = Screen.width/32;
	int height = Screen.height/5;
	string label = "Camera Enabled";
	
	// Use this for initialization
	
	void OnGUI(){
		GUI.Label(new Rect(width,height, 100, 20), label);
	}
	
	
	void Start () {

		
		
		
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
