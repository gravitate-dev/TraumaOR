using UnityEngine;
using System.Collections;

public class ToolUltrasound : BaseTool {
	
	
	public AudioSource effectSound;
	// Use this for initialization
	void Start () {
		tool_id = 4;
		durability = 10.0f;
		mistakeDamage = 0.0f;
		InvokeRepeating("ontick",1,1);
	}
	
	public override void onStopTouch(){
		firstTimeDown = true;
	}
	
	public override void onTouch(){
		if (effectSound.isPlaying==true) //lets use the sound to determine the rate at which we can use the tool :S
			return;
		if (isTouchingUI()==false){
		effectSound.Play();
		durability_wear += Time.deltaTime;
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
