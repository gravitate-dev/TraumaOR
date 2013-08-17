using UnityEngine;
using System.Collections;

public class ThePlayer : MonoBehaviour {
	
	[SerializeField]
	private int toolUsing;
	
	public AudioSource sutureSound,scalpelSound,drainSound;
	public AudioSource laserSound,bandageSound;
	private enum TOOL {
		Suture = 0,
		Gel,
		Drain,
		Tweezer,
		Ultra,
		Scalpel,
		Syringe,
		Bandage,
		Laser
	}
	// Use this for initialization
	void Start () {
	
	}
	
	
	private void changeTool(int tool_id) {
		stopToolEffects();
		this.toolUsing = tool_id;
	}
	
	// Update is called once per frame
	void Update () {
		 if (Input.GetMouseButton(0)){ // if left button pressed...
			
			
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit,100)){
				// the object identified by hit.transform was clicked
				// do whatever you want
					if (hit.transform.tag.Equals("enemyDamage")){
									hit.transform.GetComponent<Scratch>().OnClick(toolUsing);
				}
			}
			doToolEffects();
		} else {
			stopToolEffects();
		}
	}
	
	
	private void stopToolEffects(){
		switch(toolUsing)
		{
			
			case (int)TOOL.Laser:
				if (laserSound.isPlaying==true)laserSound.Stop();
			break;
			
			case (int)TOOL.Bandage:
				if (bandageSound.isPlaying==true)bandageSound.Stop();
			break;
			
			case (int)TOOL.Suture:
				if (sutureSound.isPlaying==true)sutureSound.Stop();
			break;
			
			case (int)TOOL.Scalpel:
				if (scalpelSound.isPlaying==true)scalpelSound.Stop();
			break;
			
			case (int)TOOL.Drain:
			if (drainSound.isPlaying==true)drainSound.Stop();
			break;
			
			default:
			break;
		}
	}
	private void doToolEffects(){
		switch(toolUsing)
		{
			
			case (int)TOOL.Laser:
				if (laserSound.isPlaying==false)laserSound.Play();
			break;
			
			case (int)TOOL.Bandage:
				if (bandageSound.isPlaying==false)bandageSound.Play();
			break;
			
			case (int)TOOL.Suture:
			if (sutureSound.isPlaying==false)sutureSound.Play();
			break;
			
			case (int)TOOL.Scalpel:
				if (scalpelSound.isPlaying==false)scalpelSound.Play();
			break;
			
			case (int)TOOL.Drain:
			if (drainSound.isPlaying==false)drainSound.Play();
			break;
				
			default:
			break;
		}
	}
	
	//since EZGUI doesnt let me Invoke with Parameters I made redundent functions here to call on tool buttons clicked
	public void EZchangeTool_0() {changeTool(0);}
	public void EZchangeTool_1() {changeTool(1);}
	public void EZchangeTool_2() {changeTool(2);}
	public void EZchangeTool_3() {changeTool(3);}
	public void EZchangeTool_4() {changeTool(4);}
	public void EZchangeTool_5() {changeTool(5);}
	public void EZchangeTool_6() {changeTool(6);}
	public void EZchangeTool_7() {changeTool(7);}
	public void EZchangeTool_8() {changeTool(8);}
	public void EZchangeTool_9() {changeTool(9);}
}
/* Tool reference
Suture  0
Salve : 1
Drain : 2
Tweezer : 3
Ultrasound : 4
Scalpel : 5
Suture : 6
Syringe : 7
Bandage : 8
Laser: 9
*/
