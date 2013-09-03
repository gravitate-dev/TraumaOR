using UnityEngine;
using System.Collections;

//used in the main episode select screen
public class LevelLoader : MonoBehaviour {
	
	public GameObject loadingLevelScreen;
	public GameObject confirmBox;
	public SpriteText confirmText;
	public string levelToLoad;
	public Material rankS,rankA,rankB,rankC;
	void Start(){
		confirmBox.SetActiveRecursively(false);
	}
	
	public void setLevelPlaying(int levelIndex){
		PlayerPrefs.SetInt("levelPlaying",levelIndex);
	}
	
	public void askPlayerToConfirmLevel(string levelString) {
		levelToLoad = levelString;
		confirmBox.SetActiveRecursively(true);
		confirmText.Text = "Play Mission : "+levelToLoad+"?";
	}
	
	
	private void loadChosenLevel(){
		if (levelToLoad!=null)
			Application.LoadLevel(levelToLoad);
	}
	
	//they clicked YES
	public void okayConfirm(){
		loadingLevelScreen.SetActiveRecursively(true);
		confirmBox.SetActiveRecursively(false);
		loadChosenLevel();
	}
	
	//they clicked no
	public void cancelConfirm() {
		confirmBox.SetActiveRecursively(false);
		
	}
	
	
}
