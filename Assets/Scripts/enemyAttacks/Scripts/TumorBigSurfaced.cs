using UnityEngine;
using System.Collections;

public class TumorBigSurfaced : BaseAttack {
	
	private enum TUMOR_STAGE {
		hidden_ultra = 0,
		visibleUncut_scalpel = 1, //player sees it and needs to cut the tumor
		visibleCutted_drain = 2, //player has cut the tumor but needs to drain it
		drained_scalpel = 3, //the tumor is now drained
		removeable_tweezer = 4, //waiting for the cut
		bleedinghole_tissue = 5,
		completed = 6
	}
	
	public int finalstate = 6;
	public int currentState;
	public int tumorstage_reverseStage = 3;
	public float timeTillRevertBack = 5.0f; //number of seconds for the tumor to revert back a stage
	
	public void Update(){
		
	}
	
	public override void onToolSuccess(){
		alertMultiStepSuccess();
		Destroy(gameObject);
	}
}
