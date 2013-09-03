using UnityEngine;
using System.Collections;

public class NPCTextHandler : MonoBehaviour {
	
	public SpriteText npcText;
	public GameObject backBoard;
	//this is used during the operation
	// Use this for initialization
	
	public void showText(string text,float duration){
		CancelInvoke();
		npcText.Text = text;
		npcText.Hide(false);
		backBoard.renderer.enabled = true;
		Invoke("hideText",duration);
	}
	
	public void hideText(){
		npcText.Hide(true);
		backBoard.renderer.enabled = false;
	}
}
