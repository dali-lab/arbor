// This example loads an OBJ file from the WWW, including the MTL file and any textures that might be referenced

var objFileName = "http://www.starscenesoftware.com/objtest/Spot.obj";
var standardMaterial : Material;	// Used if the OBJ file has no MTL file.
									// Also, even if there is a MTL file, the shader is taken from this material. 

function Start () {
	var loadingText = GameObject.Find("LoadingText").GetComponent(GUIText);
	loadingText.enabled = true;
	
	var objData = ObjReader.use.ConvertFileAsync (objFileName, true, standardMaterial);
	while (!objData.isDone) {
		loadingText.text = "Loading... " + (objData.progress*100).ToString("f0") + "%";
		yield;
	}

	var numberOfObjects = objData.gameObjects.Length;
	loadingText.text = "Generated " + numberOfObjects + " GameObject" + ((numberOfObjects > 1)? "s" : "");
}