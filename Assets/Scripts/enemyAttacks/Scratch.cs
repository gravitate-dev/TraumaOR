using UnityEngine;
using System.Collections;

//this script deals with the scratch behavior
public class Scratch : BaseAttack {
	
	// Use this for initialization	
	
	void Start () {
		BaseInit(5,1,5,1);
		toolNeededToUse = 0;
	}
	
	public void OnClick(int toolClicked){
		if (toolClicked == toolNeededToUse){
    	Destroy (this.gameObject);
		}
    } 

	public override void onToolSuccess(){
		Destroy(gameObject);
	}
	
}
