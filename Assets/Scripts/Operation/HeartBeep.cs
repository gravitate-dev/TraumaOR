using UnityEngine;
using System.Collections;

public class HeartBeep : MonoBehaviour {

	public AudioSource heartBeep;
	private float heartbeepInterval = 1.2f;
	private float oldheartbeepInterval = 1.2f;
	// Use this for initialization
	void Start () {
		InvokeRepeating("beep",1,oldheartbeepInterval);
	}
	
	public void beep() {
        heartBeep.Play();
		
	}
	
	public void setheartBeepInterval(float heartbeepInterval){
		if (heartbeepInterval < 1.0f) {
			Debug.LogWarning("You can't set heartbeep lower than 1.0 seconds");
			return;
		}
		this.heartbeepInterval = heartbeepInterval;
		
		if (oldheartbeepInterval!=heartbeepInterval) { //if we change the interval we have to reset the invoke
			CancelInvoke();
			InvokeRepeating("beep",heartbeepInterval,heartbeepInterval);
		}
			
	}
	
	public void stopBeep() {
		CancelInvoke();
	}
}
