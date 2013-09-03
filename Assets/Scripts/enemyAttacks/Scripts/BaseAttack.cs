using UnityEngine;
using System.Collections;

public abstract class BaseAttack : MonoBehaviour {
	protected Patient p;
	protected int damageOnInit;
	protected int damagePerTick;
	protected bool isBleeding = false;
	protected float timeTillBleed;
	protected int toolNeededToUse;
	protected bool shouldNeedToBandageAfterSlice = false;
	protected int nextToolNeeded = -1;
	protected float tickExtraDelaySeconds;
	public float toolTimeDurationHeld; //how long hold the tool on it
	public float toolTimeDurationNeededHeld;
	protected SlicePoints sp;
	public MultiStepAttack multiStepAttack;
	
	public GameObject scoreEffectGood;
	
	protected void BaseInit(int damageOnInit, int damagePerTick, int tickExtraDelaySeconds, int tickRepeatRate){
		this.damagePerTick = damagePerTick;
		this.damageOnInit = damageOnInit;
		p = Camera.main.gameObject.GetComponent<Patient>();
		InvokeRepeating("doDamage",tickExtraDelaySeconds,tickRepeatRate);
		damageOnSpawn();
	}
	
	private void doDamage() {
		p.doDamage(damagePerTick);
	}
	private void damageOnSpawn() {
		p.doDamage(damageOnInit);
	}
	
	public bool OnClick(int toolClicked){
		if (toolClicked == toolNeededToUse){
			if (toolTimeDurationNeededHeld <= toolTimeDurationHeld) {
    			onToolSuccess();
				return true;
			} else {
				toolTimeDurationHeld+=Time.deltaTime;
				Debug.Log(toolTimeDurationHeld);
			}
		}
		return false;
    }
	
	public void OnStopClick(int toolClicked){
		toolTimeDurationHeld = 0.0f;
	}
	
	public abstract void onToolSuccess();
	
	public void setMutiStepAttack(MultiStepAttack multiStepAttack){
		this.multiStepAttack = multiStepAttack;
	}
	
	//call this whenever the tool was succesfull because it needs to alert it
	public void alertMultiStepSuccess(){ //since this is called EVERYTIME may as well use it
		Instantiate(scoreEffectGood,transform.position,scoreEffectGood.transform.rotation);
		if (multiStepAttack!=null)multiStepAttack.onStepSuccess();
	}
}
