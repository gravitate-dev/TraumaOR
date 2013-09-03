using UnityEngine;
using System.Collections;
 
[RequireComponent(typeof(LineRenderer))]
public class ToolLaser : BaseTool
{
	public AudioSource effectSound;
	public Transform LaserOrigin;
	Vector2 mouse;
	RaycastHit hit;
	float range = 200.0f;
	LineRenderer line;
	     
	void Start()
	{
		line = GetComponent<LineRenderer>();
		line.SetVertexCount(2);
		line.SetWidth(.1f, .1f);
			
		tool_id = 8;
		durability = 10.0f;
		mistakeDamage = 1.0f;
		InvokeRepeating("ontick",1,1);
	}
     
	
	public override void onStopTouch(){
		firstTimeDown = true;
		if (effectSound.isPlaying == true)effectSound.Stop();
		line.enabled = false;
	}
	public override void onTouch(){
		durability_wear += Time.deltaTime;
		
		//here we do the laser effects
		if(Input.GetMouseButton(0))
	    {
			if (isTouchingUI())return; //important to allow GUI
			if (effectSound.isPlaying == false)effectSound.Play();
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit,range)){
				//Debug.Log(hit.transform.tag);
				// the object identified by hit.transform was clicked
				// do whatever you want
				if (hit.transform.tag.Equals("UIBtn")==false){
				line.SetPosition(0, LaserOrigin.position);
	    		line.SetPosition(1, hit.point);
				line.enabled = true;
				} else {
					onStopTouch();
				}
				
				if (hit.transform.tag.Equals("MissedTool")){
					onMistake();
				}
			}
	    	
	    
	    }
	    else
	    line.enabled = false;
		
		
		Transform enemyTransform = isTouchOk("Laserable",layerMask);
		if (enemyTransform ==null) { onMistake();}
		else {
			//here we handle the components
			
			BaseAttack ba = enemyTransform.GetComponent<BaseAttack>();
			if (ba!=null) {
				bool isToolDone = ba.OnClick(tool_id); //once the enemyAttack is removed
				if (isToolDone == true){
					
				}
			}
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
	public override void onDeselect(){}
}

