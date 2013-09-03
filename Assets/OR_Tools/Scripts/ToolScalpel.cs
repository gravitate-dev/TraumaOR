using UnityEngine;
using System.Collections;

public class ToolScalpel : BaseTool {
	
	public AudioSource effectSound;
	public CameraShake cameraShake;
	// Use this for initialization
	void Start () {
		tool_id = 5;
		durability = 10.0f;
		mistakeDamage = 7.0f;
		InvokeRepeating("ontick",1,1);
	}
	
	public override void onStopTouch(){
		firstTimeDown = true;
		if (effectSound.isPlaying == true)effectSound.Stop();
	}
	
	public override void onTouch(){
		
		if (isTouchingUI())return; //important to allow GUI
		if (effectSound.isPlaying==false)effectSound.Play();
		durability_wear += Time.deltaTime;
		Transform enemyTransform = isTouchOk("Cuttable",layerMask);
		if (enemyTransform !=null) {
			//here we handle the components
			
			//first check here to see if the thing is cuttable
			Cutable cutable = enemyTransform.GetComponent<Cutable>();
			if (cutable!=null)cutable.onCut();
			//then do normal attack if its not cuttable
			BaseAttack baseAttack = enemyTransform.GetComponent<BaseAttack>();
			if (baseAttack!=null)
				baseAttack.OnClick(tool_id); //once the enemyAttack is removed
			
		}
		
	}
	
	public override void onMistake(){
		
		if (nextMistakeTime <= Time.time) {
			if (effectSound.isPlaying == true)effectSound.Stop();
			cameraShake.shake();
			Debug.Log("mistake damage");
			nextMistakeTime = Time.time + mistakeDelayTime;
			patient.doDamage(mistakeDamage);
		}
		
	}
	
	public override void ontick(){
		if (durability_wear > 0) {
		durability_wear -= durabilityRegenRate;
		} else durability_wear = 0.0f;
		
	}
	
	public override void onSelect(){}
	public override void onDeselect(){}
}
