using UnityEngine;
using System.Collections;

public class ActionStageManager : MonoBehaviour {
	
	public GUISkin customText;
	public OperatingUIEvents operatingUIEvents;
	public CamerMover camMover;
	public LevelHandler levelHandler;
	public GameObject[] stages;
	public Transform[] cameraCenterLocations;
	public bool[] shouldShakeOnSpawn = new bool[10]; //this is for the editor
	public bool[] shouldBlackOutIntoScene = new bool[10]; //this is for the editor
	public bool[] _expandedActionStages = new bool[10]; //this is for the editor
	public float[] delayTimer = new float[10];
	public GameObject[] needTobeVisible = new GameObject[10];
	public bool[] instanlyMoveTo = new bool[10]; //this is for the editor
	public string[] stageNPCDialog = new string[10];
	public CameraShake cameraShake;
	private GameObject lastThingVisible;
	private ActionStage lastActionStage;
	public NPCTextHandler npcTextDialog;
	
	
	public int currentStage;
	private int totalStages;
	public Transform enemySpawnArea; //used to spawn the action stages!
	
	public bool bSpawnNextStage,bMovingCamera,bHasReachedDestination,bStageBuilding;
	
	private GameObject spawningStage;
	// Use this for initialization
	void Start () {
		if (stages.Length==0){
			Debug.LogError("You need more than 0 stages");
		}
		totalStages = stages.Length;
	}
	
	void Update(){
		
		//here we check to see if all the conditions to spawn the next stage are there!
		if (bSpawnNextStage==true && bMovingCamera ==false && bStageBuilding==false){
			nextStage();
		}
	}
	
	public void OnStageClear(){
		bHasReachedDestination = false;
		bSpawnNextStage = true;
	}
	
	public void OnCameraStoppedMoving(){
		bHasReachedDestination = true;
		bMovingCamera = false;
	}
	private void nextStage() {
		if (currentStage >= stages.Length || stages[currentStage]==null)currentStage=totalStages; //quickfix for actionstages
		
		if (totalStages==currentStage){
			Invoke("letsMoveCameraBack",1.0f);
			CameraFade.StartAlphaFade( Color.black, false, 3f, 0.0f, () => levelHandler.beginCloseBody() );
			bSpawnNextStage = false;
			return;
		}
		
		if (needTobeVisible[currentStage]!=null && needTobeVisible[currentStage]!=lastThingVisible) {
			bStageBuilding = true;
			StartCoroutine(hideBodyPartsNotUsedForActionStage(needTobeVisible[currentStage],shouldBlackOutIntoScene[currentStage]));
			return; //this will wait until its ready to spawn
		}
		
		if (cameraCenterLocations[currentStage]!=null && bHasReachedDestination==false){
			if (bMovingCamera==false){
				bMovingCamera = true;
				if (instanlyMoveTo[currentStage]==true)
					camMover.moveCameraToInstantly(cameraCenterLocations[currentStage]);
				else
					camMover.moveCameraTo(cameraCenterLocations[currentStage]);
			}
			return;
		}
		
		GameObject astage = (GameObject)Instantiate(stages[currentStage],enemySpawnArea.position,stages[currentStage].transform.rotation);
		ActionStage actionStage = astage.GetComponent<ActionStage>();
		actionStage.setActionStageManager(this);
		if (shouldShakeOnSpawn[currentStage]==true){
			cameraShake.shakeStrong(.5f);
		}
		//when its spawned its also important to put a little text info for the user
		//HERES THE NPC TEXT
		showNPCText(stageNPCDialog[currentStage]);
		
		actionStage.delayTimer = delayTimer[currentStage];
		
		
		//here i check the tooltoHint at
		if (actionStage.toolToHintAt !=-1)
		{
			operatingUIEvents.hintUseTool(actionStage.toolToHintAt);
		}
		currentStage++;
		bSpawnNextStage = false;
		lastActionStage = actionStage;
	}
	
	public void skipStage(){
		if (lastActionStage!=null)
			lastActionStage.skipStage();
	}
	public void letsMoveCameraBack(){
		camMover.returnCameraToMain();
	}
	
	public IEnumerator hideBodyPartsNotUsedForActionStage(GameObject needTobeVis,bool shouldBlackOutInto){
		Debug.Log("HIDING STUFF"+needTobeVis.name);
		if (lastThingVisible==null){
			lastThingVisible = needTobeVis;
			yield return new WaitForSeconds(0.0f);
		}
			if (shouldBlackOutInto) {
				CameraFade.StartAlphaFade( Color.black, false, 4f, 0.0f, () =>{ bStageBuilding=false; } );
				yield return new WaitForSeconds(2.0f);
				lastThingVisible.SetActiveRecursively(false);
				needTobeVis.SetActiveRecursively(true);
				lastThingVisible = needTobeVis;
			} else {
				lastThingVisible.SetActiveRecursively(false);
				needTobeVis.SetActiveRecursively(true);
				lastThingVisible = needTobeVis;
				bStageBuilding = false;
			}
		yield return new WaitForSeconds(0.0f);
	}
	
	private void showNPCText(string npcText){
		if (npcText==null || npcText=="")return;
		npcTextDialog.showText(npcText,5.0f); //TODO ill make it based off of text length later
		
	}
}
