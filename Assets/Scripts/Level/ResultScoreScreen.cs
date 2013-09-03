using UnityEngine;
using System.Collections;

public class ResultScoreScreen : MonoBehaviour {
	
	public float skipAbleDelay;
	public float theirScore;
	private float skipAllowedAtTime;
	private bool isShowing;
	public LevelHandler levelHandler;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isShowing==true && skipAllowedAtTime<Time.time){
			if (Input.GetMouseButton(0)){
				levelHandler.goToEpisodeSelect();
			}
		}
	}
	
	public void showResults(){
		skipAllowedAtTime = Time.time + skipAbleDelay*Time.deltaTime;
		isShowing = true;
	}
}
