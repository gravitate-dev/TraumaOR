using UnityEngine;
using System.Collections;

public class Tumor : BaseAttack {

	void Start () {
		BaseInit(5,1,5,1);
		toolNeededToUse = 8; //Laser
		toolTimeDurationNeededHeld = .5f;
	}

	public override void onToolSuccess(){
		alertMultiStepSuccess();
		Destroy(gameObject);
	}
}
