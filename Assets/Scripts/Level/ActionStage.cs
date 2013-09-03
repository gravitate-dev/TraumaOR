using UnityEngine;
using System.Collections;

public class ActionStage : MonoBehaviour {
	public GameObject[] obstacles;
	
	[HideInInspector]public float delayTimer;
	[HideInInspector]public Transform cameraCenterLocations;
	[HideInInspector]public string stageNPCDialogSpeakerName;
	[HideInInspector]public string stageNPCDialog;
	private ActionStageManager actionStageManager;
	public int toolToHintAt = -1;
	
	void Update (){
		if (areAllEnemiesDead()) {
			if (delayTimer==0.0f)delayTimer=0.5f;
			actionStageManager.Invoke("OnStageClear",delayTimer);
			Destroy(gameObject);
		}
	}
	
	private void goInsideBody(){
		GameObject go = GameObject.FindGameObjectWithTag("LevelHandler");
		if (go==null)Debug.LogError("You must have a LevelHandler with tag LevelHandler and Script LevelHandler in this scene!");
		LevelHandler levelHandler = go.GetComponent<LevelHandler>();
		if (levelHandler==null)Debug.LogError("LevelHandler missing levelhandlerScript");
			if (levelHandler!=null){
					levelHandler.enterTheBody();
				}
		}
	public void setActionStageManager(ActionStageManager actionStageManager){
		this.actionStageManager = actionStageManager;
	}
	public bool areAllEnemiesDead(){
		foreach( GameObject go in obstacles){
			if (go!=null)return false;
		}
		return true;
	}
	
	public void skipStage(){
		foreach( GameObject go in obstacles){
			Destroy(go);
		}
		
	}
}
