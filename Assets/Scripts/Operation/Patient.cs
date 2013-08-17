using UnityEngine;
using System.Collections;

public class Patient : MonoBehaviour
{
	public float health;
	public SpriteText healthDisplay;
	public UIProgressBar healthBar;
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
		healthDisplay.Text = health+"";
		healthBar.Value = health/100.0f;
	}
}

