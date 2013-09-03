using UnityEngine;
using System.Collections;

public class ToolTweezer : BaseTool {
	
	//public AudioSource effectSound;
	public GameObject tweezerTray;
	private GameObject objectHandleing;
	private Vector3 originalObjectPosition = Vector3.one;
	private float xdiff;
	private float ydiff;
	float range = 200.0f;
	// Use this for initialization
	void Start () {
		tool_id = 3;
		durability = 10.0f;
		mistakeDamage = 1.0f;
		InvokeRepeating("ontick",1,1);
	}
	
	public override void onStopTouch(){
		tweezerTray.active = false;
		firstTimeDown = true;
		if (objectHandleing!=null){
			objectHandleing.transform.position = originalObjectPosition;
			objectHandleing = null;
		}
	}
	
	public override void onTouch(){
		durability_wear += Time.deltaTime;
		if (objectHandleing==null) { //only need to check for UI when im not holding an object
		if (isTouchingUI())return; //important to allow GUI
		}
		//here we do the laser effects
		if(Input.GetMouseButton(0))
	    {
			
			//if (effectSound.isPlaying == false)effectSound.Play();
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit,range,layerMask)){
				//Debug.Log(hit.transform.tag);
				// the object identified by hit.transform was clicked
				// do whatever you want
				
				//we are searching for this tweezeable object
				//if we found it we will just send the hit parameters to the private function
				if (objectHandleing==null) {
				if (hit.transform.tag.Equals("Tweezeable")){						
						objectHandleing = hit.transform.gameObject;
						originalObjectPosition = objectHandleing.transform.position;
						//also we will have to set the original position so that way it wont be
						//absolute movement rather relative movement which i like better
						xdiff = objectHandleing.transform.position.x - hit.point.x;
						ydiff = objectHandleing.transform.position.y - hit.point.y;
				}
				} else {
					tweezerTray.active = true;
					handleObject(hit);
				}
				
			}
			
		}
		
	}
	
	//if u already have something in ur hand then we will move it here
	private void handleObject(RaycastHit hit){
		if (objectHandleing==null)
			return;
		if (objectHandleing.transform.tag.Equals("Tweezeable")){ //this if statement is important because when
			//the tweezeable reaches the tray it will not be tweezeable
			objectHandleing.transform.position = new Vector3(hit.point.x+xdiff,hit.point.y+ydiff,objectHandleing.transform.position.z);
		}
	}
	
	public override void onMistake(){
	}
	
	public override void ontick(){
		if (durability_wear > 0) {
		durability_wear -= durabilityRegenRate;
		} else durability_wear = 0.0f;
		
	}
	
	public override void onSelect(){}
	public override void onDeselect(){
		tweezerTray.active = false;
	}
}
