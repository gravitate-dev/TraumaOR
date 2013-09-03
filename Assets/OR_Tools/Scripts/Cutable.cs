using UnityEngine;
using System.Collections;

public class Cutable : MonoBehaviour {
	
	private bool _isCut;
	//used in cutmarks 
	// Use this for initialization
	void Start () {
	}
	
	public void onCut(){
		hideMe();
	}
	
	public void showMe(){
		gameObject.SetActiveRecursively(true);
		_isCut = false;
	}
	
	public void hideMe(){
		gameObject.SetActiveRecursively(false);
		_isCut = true;
	}
	
	public bool isCut(){
		return _isCut;
	}
	
}
