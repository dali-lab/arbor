using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	
	Vector3 startPos = Vector3.zero;
	Vector3 curPos = Vector3.zero;
	Vector3 destPos = Vector3.zero;
	float speed = 35f;
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
		float yMovement = 0;
		Vector3 moveVec = this.transform.position;
		if(isMoving == false) {
			if(Input.GetKey(KeyCode.D))
				transform.Rotate(Vector3.up * speed * Time.deltaTime);
			if(Input.GetKey(KeyCode.A))
				transform.Rotate(-Vector3.up * speed * Time.deltaTime);
		}
		/*
		if(Input.GetKey(KeyCode.Q)){
			yMovement += 0.5f * Time.deltaTime;
			print(yMovement);
			moveVec.y = yMovement;
		}
		if(Input.GetKey(KeyCode.E)){
			yMovement -= 0.5f * Time.deltaTime;
			moveVec.y = yMovement;
			print(yMovement);
		}
		this.transform.position = moveVec;
		
			
		 */
		
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
		destPos.y += .5f;
		destPos.z -= .3f;
		
		StartCoroutine(MoveToTarget());
			//curPos = destPos;
		
		//this.transform.position.z += 3.0f;
			
	}
	
	IEnumerator MoveToTarget() {
  		isMoving = true;
		float i = 0.0f;
  		while (i < 1.0f) {
    		transform.position = Vector3.Lerp(curPos, destPos, Mathf.SmoothStep(0,1,i));
			this.transform.LookAt(destPos);
    		i += Time.deltaTime;
    		yield return 0;
  		}
		isMoving = false;
	}
	
}
