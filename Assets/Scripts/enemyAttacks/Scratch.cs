using UnityEngine;
using System.Collections;

//this script deals with the scratch behavior
public class Scratch : BaseAttack {
	
	// Use this for initialization	
	
	void Start () {
		spawnDamage = 5;	
		p = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Patient>();
		InvokeRepeating("doDamage",5,1);
		damageOnSpawn();
	}
	
    public void OnClick(){
		//todo check what tool is used when clicked
    // this object was clicked - do something
    Destroy (this.gameObject);
    } 
	
	
	public override void doDamage() {
		p.doDamage(damagePerTick);
	}
	
	public override void damageOnSpawn() {
		p.doDamage(spawnDamage);
	}
	
}
