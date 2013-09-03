using UnityEngine;
using System.Collections;

public class DeathTimed : MonoBehaviour {
	
	public float timeTillDie;
	private float dieAtTime;
	void Start(){
		dieAtTime = Time.time + timeTillDie*Time.deltaTime;
	}
	
	void Update(){
		if (dieAtTime < Time.time)
			Destroy(gameObject);
	}
}
