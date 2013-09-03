using UnityEngine;
using System.Collections;

public class ToolDrain : BaseTool {
	
	public AudioSource effectSound;
	private Transform lastThingTouched;
	// Use this for initialization
	void Start () {
		tool_id = 2;
		durability = 10.0f;
		mistakeDamage = 1.0f;
		InvokeRepeating("ontick",1,1);
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log(durability_wear);
	}
	
	public override void onStopTouch(){
		firstTimeDown = true;
		if (effectSound.isPlaying == true)effectSound.Stop();
		if (lastThingTouched!=null){
			lastThingTouched.GetComponent<BaseAttack>().OnStopClick(tool_id); //once the enemyAttack is removed
		}
	}
	public override void onTouch(){
		durability_wear += Time.deltaTime;
		if (isTouchingUI())return; //important to allow GUI
		lastThingTouched = isTouchOk("Drainable",layerMask);
		Transform enemyTransform = lastThingTouched;
		if (enemyTransform ==null) { onMistake();}
		else {
			//here we handle the components
			if (effectSound.isPlaying==false)effectSound.Play();
			bool isToolDone = enemyTransform.GetComponent<BaseAttack>().OnClick(tool_id); //once the enemyAttack is removed
			if (isToolDone == true){
				if (effectSound.isPlaying==true)effectSound.Stop();
			}
		}
		
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
