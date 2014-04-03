using UnityEngine;
using System.Collections;
using UnityEditor;

public class Instructions : MonoBehaviour {

	private bool render = false;
	private Rect windowRect = new Rect (0, 0, Screen.width, Screen.height);
	public Texture image;
	
	public void ShowWindow() {
		render = true;
	}
	
	public void HideWindow() {
		render = false;
	}
	
	public void OnGUI() {
		if (GUI.Button (new Rect (Screen.width/2 - 50,10,100,20), "Instructions"))
			ShowWindow();
		
		if (render) {
			windowRect = GUI.Window (0, windowRect, DoMyWindow, image);
		}
	}
	
	public void DoMyWindow(int windowID) {
		if (GUI.Button (new Rect (Screen.width - 30,5,25,20), "X"))
			HideWindow();
	}
}
