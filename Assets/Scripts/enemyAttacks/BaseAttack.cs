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
	protected SlicePoints sp;
	
	protected void BaseInit(int damageOnInit, int damagePerTick, int tickExtraDelaySeconds, int tickRepeatRate){
		this.damagePerTick = damagePerTick;
		this.damageOnInit = damageOnInit;
				if (this.gameObject.tag!="enemyDamage"){
			Debug.LogError("The gameobject must have tag enemyDamage");
		}		
		p = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Patient>();
		InvokeRepeating("doDamage",tickExtraDelaySeconds,tickRepeatRate);
		damageOnSpawn();
	}
	
	private void doDamage() {
		p.doDamage(damagePerTick);
	}
	private void damageOnSpawn() {
		p.doDamage(damageOnInit);
	}
	public abstract void onToolSuccess();
}
