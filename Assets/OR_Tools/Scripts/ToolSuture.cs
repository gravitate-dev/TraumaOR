using UnityEngine;
using System.Collections;

public class ToolSuture : BaseTool {
	
	
	public SutureEffects sutureEffects;
	// Use this for initialization
	void Start () {
		tool_id = 0;
		durability = 10.0f;
		mistakeDamage = 1.0f;
		InvokeRepeating("ontick",1,1);
	}
	
	public override void onStopTouch(){
		firstTimeDown = true;
		sutureEffects.clearEffects();
	}
	
	public override void onTouch(){
		if (isTouchingUI()==false){
		sutureEffects.drawEffects();
		durability_wear += Time.deltaTime;
		} else {
			//Debug.Log("Touching UI!");
		}
		//all the sutures logic is handled by the effects
		
	}
	
	public override void onMistake(){
	}
	
	public override void ontick(){
		if (durability_wear > 0) {
		durability_wear -= durabilityRegenRate;
		} else durability_wear = 0.0f;
		
	}
	
	public override void onSelect(){}
	public override void onDeselect(){}
}
