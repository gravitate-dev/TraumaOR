using UnityEngine;
using System.Collections;

public abstract class BaseTool : MonoBehaviour {
	
	public Patient patient;
	protected int tool_id;
	protected float durability;
	protected float durability_wear = 0.0f;
	protected float durabilityRegenRate=1.0f;
	protected bool isUnlocked = true;
	protected bool isAvailable = true;
	protected float mistakeDamage = 0.0f;
	protected float healRate = 0.0f;
	protected bool firstTimeDown; //this is usedful for UI detection when the tool is first invoked ONTOUCHBEGIN
	public LayerMask layerMask;
	protected float nextMistakeTime;
	protected float mistakeDelayTime = 2.0f;
	protected Vector3 lastHitPoint;

	public abstract void onStopTouch();
	public abstract void onTouch();
	
	public Transform isTouchOk(string allowableTag, LayerMask lm){
		if (allowableTag == "anything") //this is used for syringe and gel
			return null;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
		
		
		
		
			if (Physics.Raycast(ray, out hit,100,lm)){
				// the object identified by hit.transform was clicked
				// do whatever you want
			lastHitPoint = hit.point;
			//Debug.Log(hit.transform.tag);
					if (hit.transform.tag.Equals(allowableTag)){
									return hit.transform;
					} else if (hit.transform.tag.Equals("UIStuff") || hit.transform.tag.Equals("UIBtn")){
						return null;
					} else if (hit.transform.tag.Equals("MissedTool")){
						onMistake();
						return null;
					}
			}
		
		return null;
	}
	public Transform isTouchOk(string allowableTag){
		int sutureOnlyLayer = LayerMask.NameToLayer("SutureOnly");
		int sutureMASK = 1 << sutureOnlyLayer; // this is 00000000000001000000
		int notSutureMask = ~(sutureMASK);
		return isTouchOk(allowableTag,notSutureMask);
	}
	
	//this should have ignore UI or not
	public bool isTouchingUI() {
		int sutureOnlyLayer = LayerMask.NameToLayer("SutureOnly");
		int sutureMASK = 1 << sutureOnlyLayer; // this is 00000000000001000000
		int notSutureMask = ~(sutureMASK);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit,100,notSutureMask)){
				if (hit.transform.tag.Equals("UIBtn")){
					if (firstTimeDown==true){ //helps call UI when it used to not be visible due to obstructions ENEMYATTACKS
					UIButton uibutton = hit.transform.GetComponent<UIButton>();
					if (uibutton!=null)uibutton.scriptWithMethodToInvoke.Invoke(uibutton.methodToInvoke,0);
				}
						return true;
					}
			}
		firstTimeDown = false;
		return false;
	}
	public abstract void onMistake();
	public abstract void ontick();
	
	public abstract void onSelect();
	public abstract void onDeselect();
	
	public void onUnavailable(){
		isAvailable = false;
		Color color = renderer.material.color;
    	color.a = 0.0f;
    	renderer.material.color = color;
	}
	
	public void onAvailable(){
		isAvailable = true;
		Color color = renderer.material.color;
    	color.a = 1.0f;
    	renderer.material.color = color;
	}
}
