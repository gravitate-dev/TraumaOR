using UnityEngine;
using System.Collections;

public class ToolSyringe : BaseTool {
	
	public AudioSource effectSound;
	public CameraShake cameraShake;
	public SyringeAnimation syringeAnimation;
	public int liquidType;
	public float liquidFillAmount;
	public float liquidFillRate = 100.0f;
	public Patient p;
	private bool isEmpty;
	private bool placedSyringeDown;
	
	public GameObject syringeObject;
	public GameObject syringeJuiceJar;
	public enum LIQUID {
		empty = 0,
		healing,
		tumorALL
	}
	// Use this for initialization
	void Start () {
		if (syringeObject==null || syringeJuiceJar==null){ //these are the jar and the syringe
			Debug.LogError("FATAL ERROR: You didnt set up the syringe jar nor the syringe!!!");
		}
		isEmpty = true;
		liquidType = 0;
		tool_id = 6;
		durability = 10.0f;
		mistakeDamage = 0.0f;
		InvokeRepeating("ontick",1,1);
		healRate = .5f; //THIS IS THE RATE OF HEAL FOR INJECTION
	}
	
	public void fillWithLiquid( int liquid, Transform juiceVial ) {
		syringeAnimation.putInJar(juiceVial);
		placedSyringeDown = false;
		//TODO
		//GET THE JUICE TYPE FROM juiceVial in the component syringeAnimation
		if (liquidType!=liquid) {
			liquidFillAmount = 0.0f;
		}
		liquidType = liquid;
		if (liquidFillAmount< 100.0){
			liquidFillAmount += Time.deltaTime*liquidFillRate;
			syringeAnimation.percent = liquidFillAmount;
			Debug.Log("Filling up : "+liquidFillAmount);
		} else {
			Debug.Log("Vial full!");
		}
		isEmpty = false;
		
	}
	
	public void emptyLiquid(float amount) {
		liquidFillAmount -=amount;
		if (placedSyringeDown==false) {
			syringeAnimation.putAtLocation(lastHitPoint);
			placedSyringeDown = true;
		} //this will place the syringe at a good location
		Debug.Log("Emptying out : "+liquidFillAmount);
		if (liquidFillAmount <= 0.0f){
			isEmpty = true;
			liquidFillAmount = 0.0f;
		}
		syringeAnimation.percent = liquidFillAmount;
		
	}
	
	public bool getIsEmpty(){
		return isEmpty;
	}
	public void injectWithLiquid(){
		if (getIsEmpty()==true)
			return;
		emptyLiquid(Time.deltaTime*liquidFillRate);
		switch(liquidType)
		{
		case (int)LIQUID.healing:
			p.doHeal(healRate);
			break;
			
		default:
			break;
		}
	}
	public override void onStopTouch(){
		syringeAnimation.putAtLocation(new Vector3(1000.0f,1000.0f,0.0f));
		firstTimeDown = true;
		placedSyringeDown = false;
		if (effectSound.isPlaying == true)effectSound.Stop();
		
	}
	
	public override void onTouch(){
		if (effectSound.isPlaying==false)effectSound.Play();
		if (isTouchingUI())return; //important to allow GUI
		durability_wear += Time.deltaTime;
		Transform juiceVial = isTouchOk("JuiceVial");
		if (juiceVial !=null) {
			fillWithLiquid((int)LIQUID.healing,juiceVial);
		} else {
			injectWithLiquid();
		}
		
	}
	
	public override void onMistake(){
		if (liquidType == (int)LIQUID.healing)return; //no penalty
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
	
	public override void onSelect(){
		syringeObject.SetActiveRecursively(true);
		syringeJuiceJar.SetActiveRecursively(true);
	}
	public override void onDeselect(){
		syringeObject.SetActiveRecursively(false);
		syringeJuiceJar.SetActiveRecursively(false);
	}
}
