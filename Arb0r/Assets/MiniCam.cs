using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MiniCam : MonoBehaviour {
	
	//public Camera tempCam;
	bool isWindowFull = false;
	List<GameObject> models = new List<GameObject>();
	int windowLimit = 1;
	GUISkin frame;
	//GUIText guiText = new GUIText();
	string modelName = "";
	bool cameraOn = false;
	Vector3 pos = new Vector3(100,100,101);
	public GUIText guiText;
	
	// Use this for initialization
	void Start () {
		//frame = (GUISkin)Resources.Load("frame");
	}
	
	void OnGUI() {
		if(cameraOn == true) {
			guiText.text = modelName;
		}
	}
	//used for loading model into new frame
	public void getModel(string name) {
		
			if(!isWindowFull) {
				cameraOn = true;
				isWindowFull = true;
			}
		
			GameObject model = (GameObject)Instantiate(Resources.Load(name));
			modelName = name;
			model.transform.position = pos;
			if(models.Count >= windowLimit){
				GameObject m = models[0];
				Destroy(m);
				models.RemoveAt(0);
			}
			models.Add(model);

	}	
	
	// Update is called once per frame
	void Update () {
		this.camera.enabled = cameraOn;
		foreach(GameObject go in models) {
			go.transform.Rotate(-Vector3.up, Time.deltaTime * 10);
		}
	
	}
}
