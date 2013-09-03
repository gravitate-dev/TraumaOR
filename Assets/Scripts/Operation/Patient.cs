using UnityEngine;
using System.Collections;

public class Patient : MonoBehaviour
{
	public float healthCap = 100;
	public float health;
	public SpriteText healthDisplay;
	public UIProgressBar healthBar;
	public AnnouncerSoundComment asc;
	public bool hasAlerted = false;
	private bool isImmune;
	

	
	// Use this for initialization
	void Start ()
	{
		healthDisplay.text = "99";	
		healthBar.Value = 99.0f/100.0f;
	}
	
	public void doDamage( float amount ){
		health -= amount;
		if (health < 80 && hasAlerted ==false){
			asc.alertWarning();
			hasAlerted=true;
		}
		if (health >= healthCap)health = healthCap;
		int iHealth = (int)health;
		healthDisplay.Text = iHealth+"";
		healthBar.Value = health/100.0f;
	}
	
	public void doHeal( float amount ) {
		health += amount;
		if (health >= healthCap)health = healthCap;
		int iHealth = (int)health;
		healthDisplay.Text = iHealth+"";
		healthBar.Value = health/100.0f;
		
	}
	
	public void setHealthCap(float healthCap) {
		this.healthCap = healthCap;
	}
	
	public void setImmune(bool isImmune){
		this.isImmune = isImmune;
	}
}

