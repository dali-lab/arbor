using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	
	Vector3 startPos = Vector3.zero;
	Vector3 curPos = Vector3.zero;
	Vector3 destPos = Vector3.zero;
	bool isMoving = false;
	bool isClick = false;
	
	
	// Use this for initialization
	void Start () {
		startPos = this.transform.position;
		curPos = startPos;
		destPos = startPos;
	}
	
	// Update is called once per frame
	void Update () {
		

		
	}
	
	void zoomOut () {
		this.transform.position = startPos;
	}
	
	public void getTarget(Vector3 location) {
		
		//destPos = location - transform.forward * 20;
		if(curPos == startPos) {
			this.transform.position = destPos;
			curPos = destPos;

		}
		curPos = this.transform.position;
		destPos = location;
		
		StartCoroutine(MoveToTarget());
			//curPos = destPos;
		
		//this.transform.position.z += 3.0f;
		
	}
	
	IEnumerator MoveToTarget() {
  		
		float i = 0.0f;
  		while (i < 1.0f) {
    		transform.position = Vector3.Lerp(curPos, destPos, Mathf.SmoothStep(0,1,i));
			this.transform.LookAt(destPos);
    		i += Time.deltaTime;
    		yield return 0;
  		}
	}
	
}
