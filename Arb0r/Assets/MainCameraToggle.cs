using UnityEngine;
using System.Collections;

public class MainCameraToggle : MonoBehaviour {
	
	bool mouseMoveEnabled = true;
	bool pointAndClick = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			mouseMoveEnabled = !mouseMoveEnabled;
			this.GetComponent<MouseLook>().enabled = mouseMoveEnabled;
			this.GetComponent<CharacterMotor>().enabled = mouseMoveEnabled;

			if(mouseMoveEnabled == true)
				print ("Camera Enabled");
		
			if(mouseMoveEnabled == false)
				print ("Camera Disabled");
	
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
