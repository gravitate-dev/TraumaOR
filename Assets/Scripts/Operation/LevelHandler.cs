using UnityEngine;
using System.Collections;

//this guy will spawn enemy stuff every so often
public class LevelHandler : MonoBehaviour {
	
	public GameObject loadingLevelScreen;
	public GameObject[] levelBodyParts;
	public GameObject theBody;
	
	public AudioSource completedSurgerySound;
	public GameObject operationSuccessBanner;
	public Transform area;
	public GameObject[] enemies = new GameObject[7];
	public float RateOfSpawn = 99;
	private float nextSpawn = 0;
	public int total_enemy_types = 0;
	public bool bStopSpawning = false;
	public ActionStageManager actionStageManager;
	public GameObject thebody;
	public GameObject insidethebody;
	public GameObject surgicalIncision;
	public TimerCountDown timerCountDown;
	public AudioSource gameMusic;
	public AudioSource heartbeeps;
	public Patient patient;
	public GameObject endGameScreen;
	public ThePlayer thePlayer;
	public ResultScoreScreen resultScoreScreen;
	public OperatingUIEvents operatingUIEvents;
	
	private float musicNormalVolume;
	
	/*for pausing */
	private bool bPaused;
	
	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt("startOnPanel",1);//this is how i make it so that u dont have to click start again
		if (RateOfSpawn<=0){
			Debug.LogError("RateOfSpawn: CANT SET TO ZERO!");
			RateOfSpawn = 5.0f;
		}
		total_enemy_types = enemies.Length;
		musicNormalVolume = gameMusic.volume;
		actionStageManager.OnStageClear();//this is how we start the game off with a stage
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Escape)) {
			Debug.Log("HI THERE22");
			if (bPaused==false)
				onPauseGame();
			else 
				onResumeGame();
		}
		/*if(bGameStarted && bStopSpawning==false && Time.time > nextSpawn)
        {
			nextSpawn = Time.time + RateOfSpawn;
			//spawnEnemey((int)Random.Range(0,total_enemy_types));
			spawnEnemey(0);
		}*/
	}
	
	private void onPauseGame(){
		Debug.Log("HI THERE");
		Time.timeScale = 0.0f;
		bPaused = true;
		float oldvolume = gameMusic.volume;
		gameMusic.volume = oldvolume/2.0f;
		timerCountDown.stopTimer();
		heartbeeps.mute=true;
		patient.setImmune(true);
	}
	
	private void onResumeGame(){
		Time.timeScale = 1.0f;
		gameMusic.volume = musicNormalVolume;
		bPaused=false;
		//gameMusic.Play();
		patient.setImmune(false);
		heartbeeps.mute=false;
		timerCountDown.resumeTimer();
	}
	
	public void spawnEnemey( int type ) {
        // Random position within this transform
        Vector3 rndPosWithin;
        rndPosWithin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rndPosWithin = area.TransformPoint(rndPosWithin * .5f);
        Instantiate(enemies[type], rndPosWithin, enemies[type].transform.rotation);      
	}
	
	public void OnTimeLimitExpire(){
		Debug.Log("Game time expired!");
	}
	
	
	public void beginCloseBody(){
		timerCountDown.addTime(3);
		thebody.active = true;
		insidethebody.active = false;
		GameObject LastThing = (GameObject)Instantiate(surgicalIncision,area.position,surgicalIncision.transform.rotation);
		LastThing.GetComponent<SurgeryIncisionAttack>().setLevelHandler(this);
		operatingUIEvents.hintUseTool(0);
			
	}
	
	public void enterTheBody() {
		CameraFade.StartAlphaFade( Color.black, false, 2f, 0.0f, () => {goInsideBody();} );
	}
	private void goInsideBody( ) //this is used to enter the body
	{
	    //then we hide ourselves
		theBody.active = false;
		foreach (GameObject obj in levelBodyParts){
			obj.active = true;
		}
		actionStageManager.OnStageClear();
		
	}
	public void stopGame(int reason){
		switch (reason){
		case 0:
			bPaused = false;
			Debug.Log("Surgery Won!");
			completedSurgerySound.Play();
			timerCountDown.stopTimer();
			gameMusic.Stop();
			heartbeeps.mute=true;
			operationSuccessBanner.active = true;
			patient.setImmune(true);
			
			
			//endgame events after fade occur in this block
			CameraFade.StartAlphaFade( Color.black, false, 1f, 0.0f, () => {
				showEndGameScreen();
				thePlayer.changeToolToTalk();
				resultScoreScreen.showResults();
			} 
			);
			break;
			
		default:
			break;
		}
	}
	
	
	public void showEndGameScreen(){
		//here we do the levels
		int farthestLevel = PlayerPrefs.GetInt("farthestLevelBeaten",1);
		int thisLevel = PlayerPrefs.GetInt("levelPlaying");
		if (farthestLevel == thisLevel){
			PlayerPrefs.SetInt("farthestLevelBeaten",++farthestLevel);
			//there we advanced the level
		}
		
		//also lets do the levelscore here
		//for now i set the score to 0
		setLevelRankScore();
		endGameScreen.SetActiveRecursively(true);
	}
	
	private void setLevelRankScore()
	{
		int thisLevel = PlayerPrefs.GetInt("levelPlaying");
		int oldscore = PlayerPrefs.GetInt("levelScoreForLevel"+thisLevel,0);
		if (oldscore==0) {
			PlayerPrefs.SetInt("levelScoreForLevel"+thisLevel,1);
			oldscore=1;
		}
		int newscore = calculateScore();
		if (oldscore < newscore){
			PlayerPrefs.SetInt("levelScoreForLevel"+thisLevel,newscore);
		}
	}
	
	private int calculateScore(){
		//TODO make a score calculator find out the total possible
		return 0;
	}
	
	public void goToEpisodeSelect(){
		loadingLevelScreen.SetActiveRecursively(true);
		Application.LoadLevel("levelSelect");
		
	}
	
	

}


