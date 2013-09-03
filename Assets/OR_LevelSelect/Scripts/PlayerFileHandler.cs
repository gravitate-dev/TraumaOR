using UnityEngine;
using System.Collections;

public class PlayerFileHandler : MonoBehaviour {
	
	public MyUIListItem[] myLevels;
	//i will handle the 
	//levels they beat and i record which levels are locked
	//and not locked
	// Use this for initialization
	void Start () {
		//resetPlayerLevels();
		//i get the highest level beaten
		int currentLevel = PlayerPrefs.GetInt("farthestLevelBeaten",1);
		
		
		for (int i =0; i < currentLevel && i < myLevels.Length;i++){ 
			myLevels[i].unlockLevel();
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	 void OnApplicationQuit()
	{
		//so that way it will always start at 0 when they start the game
		PlayerPrefs.SetInt("startOnPanel", 0);
	}
	
	void resetPlayerLevels()
	{
		PlayerPrefs.DeleteAll();
		
	}
}
