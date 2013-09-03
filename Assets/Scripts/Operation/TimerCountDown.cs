using UnityEngine;
using System.Collections;

public class TimerCountDown : MonoBehaviour {
	
	public SpriteText stext;
	public int timeLeft;
	public int timeLeft_Minuets;
	public int timeLeft_Seconds;
	public LevelHandler levelHandler;
	private bool bTimeStopped;
	// Use this for initialization
	void Start () {
		StartCoroutine(beginCountdown());
		
	}
	
	IEnumerator beginCountdown(){
		bTimeStopped = false;
		while (bTimeStopped==false){
		
		if (timeLeft <= 0) {
			break;
			stext.Text = "0";
			levelHandler.OnTimeLimitExpire();
		} else {
			timeLeft_Minuets  = timeLeft / 60;
			timeLeft_Seconds = timeLeft % 60;
			stext.Text = timeLeft_Minuets.ToString()+":"+timeLeft_Seconds.ToString();
			timeLeft -= 1;
		}
			yield return new WaitForSeconds(1);
		}
	}
	
	public void stopTimer(){
		bTimeStopped = true;
	}
	
	public void addTime(int amount){
		timeLeft += amount;
	}
	
	public void resumeTimer() {
		bTimeStopped = false;
	}
}
