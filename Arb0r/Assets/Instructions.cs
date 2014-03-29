using UnityEngine;
using System.Collections;
//using UnityEditor;

public class Instructions : MonoBehaviour {

	private bool isInstructions = false;
	private Rect windowRect = new Rect (0, 20, Screen.width, Screen.height + 20);
	public Texture image;

	
	public void OnGUI() {

		if(!isInstructions){
			if (GUI.Button (new Rect (Screen.width - 150, 10, 100, 20), "Instructions")){
				isInstructions = !isInstructions;
			}
		}


		if(isInstructions){
			GUI.Label(windowRect, image);
			if(GUI.Button (new Rect (Screen.width - 150, 10 , 100, 20), "Close")){
				isInstructions = !isInstructions;
			}
		}



	}

}
