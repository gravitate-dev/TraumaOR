using UnityEngine;
using System.Collections;

//this script deals with the scratch behavior
public class Scratch : BaseAttack {
	
	// Use this for initialization	
	
	void Start () {
		BaseInit(5,1,5,1);
		toolNeededToUse = 12; //suturekillable only
		lookAtCamera();
	}
	
	private void lookAtCamera(){
		transform.LookAt(Camera.mainCamera.transform.position);
		transform.Rotate(Vector3.right * 90);
	}
	
	

	public override void onToolSuccess(){
		//actually the object isnt destroyed the sutures have to be checked
		alertMultiStepSuccess();
		Destroy(gameObject);
	}
	
}
