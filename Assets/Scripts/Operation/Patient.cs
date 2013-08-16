using UnityEngine;
using System.Collections;

public class Patient : MonoBehaviour
{
	public float health;
	public GUIText healthDisplay;
	public AnnouncerSoundComment asc;
	public bool hasAlerted = false;
	// Use this for initialization
	void Start ()
	{
		healthDisplay.text = "100";	
	}
	
	public void doDamage( float amount ){
		health -= amount;
		if (health < 80 && hasAlerted ==false){
			asc.alertWarning();
			hasAlerted=true;
		}
		healthDisplay.text = health+"";
	}
}

