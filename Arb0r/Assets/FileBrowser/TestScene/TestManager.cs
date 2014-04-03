using UnityEngine;
using System.Collections;

//=========================================================================
// Sample script showing FileBrowser class usage
//=========================================================================
public class TestManager : MonoBehaviour {
	
	public FileBrowser fileBrowser;
	
	public Material materialWalls;
	public Material materialFloor;
	public Material materialObjects;
	
	public GUISkin[] extraSkins;
	
	public string[]  extraSkinsInfos;
	public string    texBrowserInfo = "";
	
	public GameObject ceilingLight;
		
	//=========================================================================
	void ChangeWallsTexture() {
		this.fileBrowser.inputMustExist = true;
		this.fileBrowser.ShowBrowser("Pick a texture for the Walls", this.WallsPickedCallback);
	}
	void ChangeFloorTexture() {
		this.fileBrowser.inputMustExist = true;
		this.fileBrowser.ShowBrowser("Pick a texture for the Floor", this.FloorPickedCallback);
	}
	void ChangeObjectsTexture() {
		this.fileBrowser.inputMustExist = true;
		this.fileBrowser.ShowBrowser("Pick a texture for the Objects", this.ObjectsPickedCallback);
	}
	
	void ChangeSceneAudio() {
		this.fileBrowser.inputMustExist = true;
		this.imageMasks = this.fileBrowser.fileMasks;
		this.fileBrowser.fileMasks = "*.wav;*.ogg";
		this.fileBrowser.ShowBrowser("Pick an audio file for the scene's music", this.AudioPickedCallback);
	}

	void Save() {
		this.fileBrowser.ShowSaveBrowser("Save", this.SaveCallback);
	}
	void Load() {
		this.fileBrowser.showTextInput = false;
		this.fileBrowser.ShowLoadBrowser("Load", this.LoadCallback);
	}
	
	//=========================================================================
	void WallsPickedCallback(string file) {
		this.StartCoroutine(this.swapMaterialTexture(this.materialWalls, file));
	}
	void FloorPickedCallback(string file) {
		this.StartCoroutine(this.swapMaterialTexture(this.materialFloor, file));
	}
	void ObjectsPickedCallback(string file) {
		this.StartCoroutine(this.swapMaterialTexture(this.materialObjects, file));
	}
	void AudioPickedCallback(string file) {
		this.fileBrowser.fileMasks = this.imageMasks;
		this.StartCoroutine(this.changeSceneSound(file));
	}

	void SaveCallback(string file) {
		print(file);
	}
	void LoadCallback(string file) {
		print(file);
	}


	//=========================================================================
	IEnumerator swapMaterialTexture(Material material, string texFilename)
	{
		this.infoString = "";
		if (texFilename == null || texFilename == "") yield break;
		
		if (!texFilename.EndsWith(".jpg") && !texFilename.EndsWith(".jpeg") && !texFilename.EndsWith(".png")) {
			this.infoString = "Unsupported format";
			yield break;
		}
		
		WWW www = new WWW("file://" + texFilename);
		yield return www;
		www.LoadImageIntoTexture(material.mainTexture as Texture2D);
		yield break;
	}
	
	//=============================================================================
	IEnumerator changeSceneSound(string path)
	{
		this.infoString = "";
		if (path == null || path == "") yield break;
		
		if (!path.EndsWith(".wav") && !path.EndsWith(".ogg")) {
			this.infoString = "Unsupported format";
			yield break;
		}
		
		WWW www = new WWW("file://" + path);
		yield return www;

		AudioSource source = GameObject.FindObjectOfType(typeof(AudioSource)) as AudioSource;
		source.clip = www.GetAudioClip(true, false);
		source.loop = true;
		source.Play();
	}

	
	//=========================================================================
	// Safelly ignore most of what follows - GUI code to make the sample scene go.
	//=========================================================================
	
	private int currentSkinIndex = -1;
	
	private string infoString = "";
	
	private enum TextureType { Walls, Floor, Objects };
	
	private string imageMasks;

	//=========================================================================
	void Start ()
	{
		this.ceilingLight.animation["LightAnim"].speed = 2;
		// iTween would be nice here, but we want a clean dependency-free package. Let's user a dumb Lerp updater instead
//		iTween.RotateTo(this.ceilingLight, iTween.Hash("x", 120, "time", 2, "looptype", iTween.LoopType.pingPong, "easetype", iTween.EaseType.easeInOutSine));
		
		this.texBrowserInfo = this.texBrowserInfo.Replace("\\n", "\n");
		for (int i = 0; i < this.extraSkinsInfos.Length; i++) {
			this.extraSkinsInfos[i] = this.extraSkinsInfos[i].Replace("\\n", "\n");
		}
	}
	
	//=========================================================================
	Vector2 __OnGUI_DoSlider(Vector2 vector2)
	{
		float valueX = vector2.x;
		float valueY = vector2.y;
		
		GUILayout.BeginHorizontal();
			GUILayout.Label ("X " + valueX);
			GUILayout.Label ("Y " + valueY);
		GUILayout.EndHorizontal();
		
		valueX = GUILayout.HorizontalSlider(valueX, -5f, 5f);
		valueY = GUILayout.HorizontalSlider(valueY, -5f, 5f);
		valueX = Mathf.Round(valueX * 10) / 10.0f;
		valueY = Mathf.Round(valueY * 10) / 10.0f;
		
		return new Vector2(valueX, valueY);
	}

	//=========================================================================
	Vector2[] __OnGUI_Material(string name, TextureType type, int texSize, Texture texture, Vector2 scale, Vector2 offset)
	{
		GUILayout.BeginHorizontal();
			GUILayout.Label(name);
			GUILayout.Label("Scales:");
		GUILayout.EndHorizontal();
	
		GUILayout.BeginHorizontal();
			if (GUILayout.Button(texture, GUILayout.Width(texSize), GUILayout.Height(texSize))) {
				switch(type) {
				case TextureType.Floor:   this.ChangeFloorTexture();   break;
				case TextureType.Walls:   this.ChangeWallsTexture();   break;
				case TextureType.Objects: this.ChangeObjectsTexture(); break;
				}
				this.infoString = this.texBrowserInfo;
			}
			
			GUILayout.BeginVertical();
				scale = this.__OnGUI_DoSlider(scale);
				GUILayout.Label ("Offset:");
				offset = this.__OnGUI_DoSlider(offset);
			GUILayout.EndVertical();
		GUILayout.EndHorizontal();
		
		return new Vector2[2] { scale, offset };
	}

	//=========================================================================
    void __OnGUI_BrowserCustomize()
	{
        Rect customizeRect = new Rect(10, 10, 150, Screen.height - 20);

        GUILayout.BeginArea(customizeRect, "File Browser", GUI.skin.window);
            GUILayout.Space(10);
		
			// Skin Choices:
            GUILayout.Label("Skin:");
			int oldIndex = this.currentSkinIndex;
            if (GUILayout.Toggle(this.currentSkinIndex == -1, "Unity Default", GUI.skin.button)) {
                this.currentSkinIndex = -1;
            }
			for (int i = 0; i < this.extraSkins.Length; i++) {
				if (this.extraSkins[i] == null) continue;
	            if (GUILayout.Toggle(this.currentSkinIndex == i, this.extraSkins[i].name.Replace("GUISkin", ""), GUI.skin.button)) {
	                this.currentSkinIndex = i;
	            }
			}
            if (this.currentSkinIndex != oldIndex) {
				this.infoString = "";
				this.fileBrowser.skin = this.currentSkinIndex >= 0 ? this.extraSkins[this.currentSkinIndex] : GUI.skin;
				this.fileBrowser.RefreshStyles();																// TODO: Make skin into a property with setter
			
				if (this.currentSkinIndex >= 0 && this.currentSkinIndex < this.extraSkinsInfos.Length) {
					this.infoString = this.extraSkinsInfos[this.currentSkinIndex];
				}
            }
		
			// File browser boolean options:
            GUILayout.Space(10);
            this.fileBrowser.showPlaces         = GUILayout.Toggle(this.fileBrowser.showPlaces,         "Show Places");
            this.fileBrowser.dirExplorer        = GUILayout.Toggle(this.fileBrowser.dirExplorer,        "Explore Dirs");
            this.fileBrowser.acceptsDirectories = GUILayout.Toggle(this.fileBrowser.acceptsDirectories, "Accept Dirs");
			this.fileBrowser.enableFileDelete   = GUILayout.Toggle(this.fileBrowser.enableFileDelete,   "Allow Deletes");
			this.fileBrowser.showTextInput      = GUILayout.Toggle(this.fileBrowser.showTextInput,      "Show Text Input");

			bool hideExt = GUILayout.Toggle(this.fileBrowser.hideExtensions, "Hide Extensions");
			if (this.fileBrowser.hideExtensions != hideExt) {
				this.fileBrowser.hideExtensions = hideExt;
				this.fileBrowser.RefreshStyles();
			}
		
			// Dir and file sizes:
            GUILayout.Space(10);
            GUILayout.Label("Dirs Icon Size: " + this.fileBrowser.dirsHeight);
			int dirsHeight  = this.fileBrowser.dirsHeight;
			int filesHeight = this.fileBrowser.filesHeight;
            this.fileBrowser.dirsHeight  = (int) GUILayout.HorizontalSlider(dirsHeight,  16, 64);
            GUILayout.Label("File Thumb Size: " + this.fileBrowser.filesHeight);
            this.fileBrowser.filesHeight = (int) GUILayout.HorizontalSlider(filesHeight, 16, 128);
			if (dirsHeight != this.fileBrowser.dirsHeight || filesHeight != this.fileBrowser.filesHeight) {
				this.fileBrowser.RefreshStyles();
			}

            GUILayout.Space(10);
            GUILayout.Label("File Masks: ");
            string newVal = GUILayout.TextField(this.fileBrowser.fileMasks);
            if (this.fileBrowser.fileMasks != newVal) {
                this.fileBrowser.fileMasks = newVal;
				this.fileBrowser.RefreshContents();
            }

        GUILayout.EndArea();
    }
	
	//=========================================================================
	void __OnGUI_TextureCustomize()
	{
		int texSize = (int) ((Screen.height - 150) * 0.33f);
		Rect texturesRect = new Rect(Screen.width - (texSize + 130) - 10, 10, texSize + 130, Screen.height - 20);
		
		GUILayout.BeginArea(texturesRect, "Textures", GUI.skin.window);
		
			Vector2[] values = this.__OnGUI_Material(
				"Walls:", TextureType.Walls, texSize, 
				this.materialWalls.mainTexture,
				this.materialWalls.mainTextureScale,
				this.materialWalls.mainTextureOffset
			);
			this.materialWalls.mainTextureScale  = values[0];
			this.materialWalls.mainTextureOffset = values[1];
		
			GUILayout.Space(10);

			values = this.__OnGUI_Material(
				"Floor:", TextureType.Floor, texSize,
				this.materialFloor.mainTexture,
				this.materialFloor.mainTextureScale,
				this.materialFloor.mainTextureOffset
			);
			this.materialFloor.mainTextureScale  = values[0];
			this.materialFloor.mainTextureOffset = values[1];

			GUILayout.Space(10);

			values = this.__OnGUI_Material(
				"Objects:", TextureType.Objects, texSize,
				this.materialObjects.mainTexture,
				this.materialObjects.mainTextureScale,
				this.materialObjects.mainTextureOffset
			);
			this.materialObjects.mainTextureScale  = values[0];
			this.materialObjects.mainTextureOffset = values[1];

		GUILayout.EndArea();
	}
	
	//=========================================================================
	void OnGUI()
	{
		if (this.fileBrowser.Active) {
			this.__OnGUI_BrowserCustomize();
		} else {
			this.__OnGUI_TextureCustomize();
		}
		
		int w = Screen.width;
		int h = Screen.height;
		
		if (!this.fileBrowser.Active) {
			if (GUI.Button(new Rect(w / 2 - 100, h - 40, 90,  30), "Save")) this.Save();
			if (GUI.Button(new Rect(w / 2,       h - 40, 90,  30), "Load")) this.Load();
			if (GUI.Button(new Rect(w / 2 - 100, h - 80, 190, 30), "Choose Music")) this.ChangeSceneAudio();
		}
		
		if (this.infoString != "")
		{
	        Rect infoRect = new Rect(w - 170, 10, 160, 300);
	        GUILayout.BeginArea(infoRect, "Info", GUI.skin.window);
			GUILayout.TextArea(this.infoString, GUI.skin.label);
			GUILayout.EndArea();
		}
	}
	
}


