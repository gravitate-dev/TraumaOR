using UnityEngine;
using System.Collections;

//this script deals with the scratch behavior
public class BloodPool : BaseAttack {
	
	// Use this for initialization	
	
	void Start () {
		BaseInit(5,1,5,1);
		toolNeededToUse = 2; //drainun
		toolTimeDurationNeededHeld = 1.0f;
	}

	public override void onToolSuccess(){
		alertMultiStepSuccess();
		Destroy(gameObject);
	}
	
}
