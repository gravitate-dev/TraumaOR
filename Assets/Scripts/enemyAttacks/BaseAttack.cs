using UnityEngine;
using System.Collections;

public abstract class BaseAttack : MonoBehaviour {
	protected Patient p;
	protected float health = 10; //how much health
	protected float damagePerTick = 1; //if this isn't taken care of it will slowly damage
	protected float spawnDamage;
	// Use this for initialization
	void Start () {
		if (this.gameObject.tag!="enemyDamage"){
			Debug.LogError("The gameobject must have tag enemyDamage");
		}		
	}
	
	public abstract void doDamage();
	public abstract void damageOnSpawn();
}
