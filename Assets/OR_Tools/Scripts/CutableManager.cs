using UnityEngine;
using System.Collections;

//I hold the references to the cuts if the cut fails then i will set all the cutables back to viewable

public class CutableManager : MonoBehaviour {
	
	public Cutable[] cutables;
	public bool destroySelfOnCut;
	public BaseAttack baseAttack;
	void Start() {
		baseAttack = gameObject.GetComponent<BaseAttack>();
	}
	void Update() {
		if (Input.GetMouseButtonUp(0)){
			if (countAllCuts()==0){ //this will check to see if the cuts are all down
				if (baseAttack!=null){
					baseAttack.onToolSuccess();
				}
				if (destroySelfOnCut)Destroy(gameObject);
			} else {
				showAllCuts();
			}
		}
		
	}
	
	public void showAllCuts(){
		for (int i = 0; i < cutables.Length; i++){
			cutables[i].showMe();
		}
	}
	
	public int countAllCuts(){
		int returnThis = 0;
		for (int i = 0; i < cutables.Length; i++){
			if (cutables[i].isCut()==false)returnThis++;
		}
		return returnThis;
	}
	
	public void onOneCut(){
		//this is a call for Cutable to use
	}
	
	private void onStopCutting() {
		showAllCuts();
	}
	
}
