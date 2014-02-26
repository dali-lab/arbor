using UnityEngine;
using System.Collections;

public class WindowDrag : MonoBehaviour {

	int GUI_Unit; // the screenwidth / 64 to give us a building block to work with
	int smallBuff; //GUI_Unit/4, a small buffer for spacing
	int bigBuff; //GUI_Unit/2, a larger buffer for spacing
	int buttonWidth; // standard width of buttons
	int buttonHeight; // standard height of buttons (textsize + buffer)
	int menuWidth; // == buttonWidth + bigBuffer
	int menuTextSize; //same as GUI_Unit.
	int sideX; // the distance of a button from the side of the screen.

	string XbuttonText; //the text that will be displayed on the X Button
	string YbuttonText;

	string selectedX; //a temporary handler for our new variables (set to NewX/YValue on "Reset")
	string selectedY;

	string newXValue; // the variables that the program will read from to draw the tree
	string newYValue;

	string prettyArrows;

	string[] dummyArray = new string[] {"weight", "height", "color", "temp", "geography", "LOL", "HAHAH", "Phineaus Gage", "Sinbad", "I'm a wizard"};

	bool displayXValues;
	bool displayYValues;

	private Vector2 scrollPosition;
	

	void OnGUI (){

		drawMenu ();

		if (displayYValues == true)
			{
			drawYVarMenu ();
			}

		if (displayXValues == true)
		{
			drawXVarMenu ();
		}
	}

	void drawMenu (){


		int y = bigBuff + smallBuff;

		//draw the box to contain the menu
		GUI.Box (new Rect(bigBuff, bigBuff, menuWidth, ((buttonHeight*4) + (smallBuff*5)) ), "");


		if (GUI.Button (new Rect(sideX, y, buttonWidth, buttonHeight), "<size=" + menuTextSize +">" + XbuttonText + "</size>"))
		{
			displayXValues = true;
			XbuttonText = prettyArrows;


		}

		if (GUI.Button (new Rect(sideX, (y+= (buttonHeight + smallBuff)), buttonWidth, buttonHeight), "<size=" + menuTextSize +">" + YbuttonText + "</size>"))
		{
			displayYValues = true;
			YbuttonText = prettyArrows;
		}


		if (GUI.Button (new Rect(sideX, (y+= (buttonHeight + smallBuff)), buttonWidth, buttonHeight), "<size=" + menuTextSize +">Reset</size>"))
		{
			resetView ();
		}

		if (GUI.Button (new Rect(sideX, (y += (buttonHeight + smallBuff)), buttonWidth, buttonHeight), "<size=" + menuTextSize + ">Ortho</size>"))
		{
			switchToOrtho();
		}


	}

	//function for switching to orthographic view;
	void switchToOrtho () {
		//switch to ortho
	}

	//function for reseting the view based off of X and Y variables
	void resetView (){
		selectedX = newXValue;
		selectedY = newYValue;

		/* PUT CODE HERE
		 * to reset the position of the camera at the base node
		 * k cool.
		 */
	}

	void drawYVarMenu ()
		{
		displayXValues = false; //make sure we can't open two menus at once

		scrollPosition = GUI.BeginScrollView (
			new Rect( 
		         (bigBuff + (smallBuff*2) + smallBuff + menuWidth),		 //X
		         bigBuff, 												//Y
		         ((menuWidth + smallBuff*2)+20), 						//W
		         ((buttonHeight*4)+(smallBuff*4))						//H
		         ),

			scrollPosition, 
				
			new Rect(
				0,
				0, 
				((menuWidth + smallBuff*2)),
				(dummyArray.Length*(buttonHeight + smallBuff) + smallBuff)
				)
			);

		//GUI.Box (new Rect((bigBuff + smallBuff + menuWidth), bigBuff, menuWidth, (dummyArray.Length*(smallBuff + buttonHeight)+smallBuff)) , "");
		
		GUI.Box (new Rect(0,0,menuWidth, dummyArray.Length*(smallBuff+buttonHeight)+smallBuff), "");       

		for (int i = 0; i < dummyArray.Length; i++)
			{
			if(GUI.Button (new Rect(smallBuff, (smallBuff + i*(buttonHeight + smallBuff)), buttonWidth, buttonHeight), dummyArray[i]))
				{
				newYValue = dummyArray[i];
				YbuttonText = dummyArray[i];
				displayYValues = false;
				}
			}

		GUI.EndScrollView();

		}

	void drawXVarMenu (){
		displayYValues = false; //make sure we can't open two menus at once

		scrollPosition = GUI.BeginScrollView (
			new Rect( 
		         (bigBuff + (smallBuff*2) + smallBuff + menuWidth),		 //X
		         bigBuff, 												//Y
		         ((menuWidth + smallBuff*2) + 20), 						//W
		         ((buttonHeight*4)+(smallBuff*4))						//H
		         ),
			
			scrollPosition, 
			
			new Rect(
			0,
			0, 
			((menuWidth + smallBuff*2)),
			(dummyArray.Length*(buttonHeight + smallBuff) + smallBuff)
			)
			);

		GUI.Box (new Rect(0,0,menuWidth, dummyArray.Length*(smallBuff+buttonHeight)+smallBuff), "");

		//scrollable box.
		for (int i = 0; i < dummyArray.Length; i++){
			if(GUI.Button (new Rect(smallBuff, (smallBuff + i*(buttonHeight + smallBuff)), buttonWidth, buttonHeight), dummyArray[i]))
			{
				newXValue = dummyArray[i];
				XbuttonText = dummyArray[i];
				displayXValues = false;
				
			}

		}

		GUI.EndScrollView ();

		
	}
		
	
	// Use this for initialization
	void Start () {
		GUI_Unit = (int)(Screen.width / 64);
		smallBuff = GUI_Unit / 4;
		bigBuff = GUI_Unit / 2;
		sideX = bigBuff + smallBuff;
		buttonHeight = GUI_Unit + bigBuff;
		buttonWidth = GUI_Unit * 8;
		menuTextSize = GUI_Unit;
		menuWidth = buttonWidth + bigBuff;

		XbuttonText = "X Value";
		YbuttonText = "Y Value";

		prettyArrows = "> > >";

		displayXValues = false;
		displayYValues = false;

		scrollPosition = Vector2.zero;
//		Debug.Log(dummyArray.Length);

	}
	
	// Update is called once per frame
	void Update () {
		GUI_Unit = (int)(Screen.width / 64);
		smallBuff = GUI_Unit / 4;
		bigBuff = GUI_Unit / 2;
		sideX = bigBuff + smallBuff;
		buttonHeight = GUI_Unit + bigBuff;
		buttonWidth = GUI_Unit * 8;
		menuTextSize = GUI_Unit;
		menuWidth = buttonWidth + bigBuff;

	}
	
}


