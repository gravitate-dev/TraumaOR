using UnityEngine;
using System.Collections;

public class ToolGel : BaseTool {
	
	public Patient p;
	public GameObject gel;
	public AudioSource effectSound;
	public float nextGelSpawnTime;
	private float gelSpawnRate  = 1.0f;
	private int numberSpawned=0;
	private float range = 200.0f;
	
	public LayerMask layerMaskSecondCast;
	public Transform zstopper;
	// Use this for initialization
	void Start () {
		tool_id = 1;
		durability = 100.0f;
		mistakeDamage = 0.0f;
		healRate = 0.1f;
		InvokeRepeating("ontick",1,1);
	}
	
	public override void onStopTouch(){
		firstTimeDown = true;
		if (effectSound.isPlaying == true)effectSound.Stop();
	}
	public override void onTouch(){
		durability_wear += Time.deltaTime;
		
		if(Input.GetMouseButton(0))
	    {
			if (isTouchingUI())return; //important to allow GUI
			if (effectSound.isPlaying == false)effectSound.Play();
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			//Debug.Log(nextGelSpawnTime- Time.time*10);
			if (Physics.Raycast(ray, out hit,range,layerMask)){
				//Debug.Log(hit.transform.tag);
				// the object identified by hit.transform was clicked
				// do whatever you want
				if (hit.transform.tag.Equals("UIBtn")==false){
					
					if (effectSound.isPlaying==false)effectSound.Play();
					//then spawn the gels
					
					if (Time.time*10 > nextGelSpawnTime) {
						nextGelSpawnTime = Time.time*10 + gelSpawnRate;
						if (hit.transform.tag.Equals("MissedTool")) //we will recast this
						{
							Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
							if (Physics.Raycast(ray2, out hit,range,layerMaskSecondCast)){
								//Debug.Log("Second cast to :"+hit.transform.tag);
								if (hit.transform.tag.Equals("BarelyVisibleSurface")|| hit.transform.tag.Equals("Gelable")) //we will recast this
							{
								//Debug.Log("Second cast to :"+hit.transform.tag);
							//Vector3 new_pos = new Vector3(hit.point.x,hit.point.y,zstopper.position.z);
							Instantiate(gel,hit.point,Quaternion.identity);	
							}
							}
						}
						else
						Instantiate(gel,hit.point,Quaternion.identity);
						//Debug.Log("Spawned GelCount: "+numberSpawned++);
						p.doHeal(healRate);
					}
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
