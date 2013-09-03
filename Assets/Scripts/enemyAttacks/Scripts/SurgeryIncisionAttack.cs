using UnityEngine;
using System.Collections;

//this script deals with the scratch behavior
public class SurgeryIncisionAttack : BaseAttack {
	
	// Use this for initialization	
	private LevelHandler levelHandler;
	void Start () {
		BaseInit(0,0,100,100);
		toolNeededToUse = 12; //suturekillable only
		lookAtCamera();
		GameObject obj = GameObject.FindGameObjectWithTag("LevelHandler");
		if (obj==null)Debug.LogError("LEVEL HANDLER MISSING!");
		else levelHandler = obj.GetComponent<LevelHandler>();
	}
	
	private void lookAtCamera(){
		transform.LookAt(Camera.mainCamera.transform.position);
		transform.Rotate(Vector3.right * 90);
	}
	
	
	
	public void setLevelHandler(LevelHandler levelHandler){
		this.levelHandler = levelHandler;	
	}
	
	public override void onToolSuccess(){
		//actually the object isnt destroyed the sutures have to be checked
		if (levelHandler!=null){
			levelHandler.stopGame(0);
		}
		Destroy(gameObject);
	}
	
}
