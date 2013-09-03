using UnityEngine;
using System.Collections;

public class Splinter : BaseAttack {

	void Start () {
		BaseInit(5,1,5,1);
		toolNeededToUse = 3; //Tweezer
		toolTimeDurationNeededHeld = 0f;
	}

	public override void onToolSuccess(){
		alertMultiStepSuccess();
		Destroy(gameObject);
	}
}
