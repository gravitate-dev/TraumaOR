using UnityEngine;
using System.Collections;

public class AnnouncerSoundComment : MonoBehaviour {
	public AudioSource audiosrc;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//when health is low NPC will say stuff
	public void alertWarning(){
		audiosrc.Play();
	}
}
