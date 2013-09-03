using UnityEngine;
using System.Collections;

public class MultiStepAttack : MonoBehaviour {

	//this guy is used to manage the multiple step ones
	//lets test this out with a laser
	
	public GameObject[] states;
	private int finalstate;
	private int currentState;
	public float timeTillRevertBack = 5.0f; //number of seconds for the tumor to revert back a stage
	
	void Start() {
		finalstate = states.Length;
		GameObject newthing = (GameObject)Instantiate(states[currentState],transform.position,states[currentState].transform.rotation);
		newthing.GetComponent<BaseAttack>().setMutiStepAttack(this);
		
	}
	
	public void onStepSuccess(){
		currentState++;
		if (currentState == finalstate){
			Destroy(gameObject);
			return;
		}
		GameObject newthing = (GameObject)Instantiate(states[currentState],transform.position,states[currentState].transform.rotation);
		
		BaseAttack baseAttack = newthing.GetComponent<BaseAttack>();
		if (baseAttack==null){
			Debug.LogError("Very bad! multi step attack without baseattack component!");
		} else baseAttack.setMutiStepAttack(this);
	}
	
}
