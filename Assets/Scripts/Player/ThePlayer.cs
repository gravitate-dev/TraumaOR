using UnityEngine;
using System.Collections;

public class ThePlayer : MonoBehaviour {
	
	[SerializeField]
	private int toolUsing;
	
	public ToolSuture toolSuture;
	public ToolDrain toolDrain;
	public ToolLaser toolLaser;
	public ToolScalpel toolScalpel;
	public ToolTweezer toolTweezer;
	public ToolGel toolGel;
	public ToolSyringe toolSyringe;
	public ToolUltrasound toolUltrasound;
	private bool hasHandledStopTouch;
	public OperatingUIEvents operatingUIEvents;
	
	private enum TOOL {
		Suture = 0,
		Gel=1,
		Drain=2,
		Tweezer=3,
		Ultra=4,
		Scalpel=5,
		Syringe=6,
		Bandage=7,
		Laser=8,
		Talk=9 //this is where the player is talking
	}

	
	
	private void changeTool(int tool_id) {
		stopToolEffects();
		onDeselectOldTool(toolUsing);
		this.toolUsing = tool_id;
		onSelectNewTool(tool_id);
		operatingUIEvents.setSelectedTool(tool_id);
	}
	
	// Update is called once per frame
	void Update () {
		 if (Input.GetMouseButton(0)){ // if left button pressed...
			doToolEffects();
		} else {
			stopToolEffects();
		}
	}
	
	
	private void stopToolEffects(){
		hasHandledStopTouch = true;
		switch(toolUsing)
		{
			
			case (int)TOOL.Laser:
				toolLaser.onStopTouch();
			break;
			
			case (int)TOOL.Gel:
				toolGel.onStopTouch();
			break;
			
			case (int)TOOL.Tweezer:
				toolTweezer.onStopTouch();
			break;
			
			case (int)TOOL.Suture:
			if (toolSuture!=null)toolSuture.onStopTouch();
			break;
			
			case (int)TOOL.Ultra:
				toolUltrasound.onStopTouch();
			break;
			
			
			case (int)TOOL.Scalpel:
				toolScalpel.onStopTouch();
			break;
			
			case (int)TOOL.Drain:
				toolDrain.onStopTouch();
			break;
			
			case (int)TOOL.Syringe:
				toolSyringe.onStopTouch();
			break;
			
			default:
			break;
		}
	}
	
	private void onSelectNewTool(int toolid){
		switch(toolid)
		{
			
			case (int)TOOL.Laser:
				toolLaser.onSelect();
			break;
			
			case (int)TOOL.Gel:
				toolGel.onSelect();
			break;
			
			case (int)TOOL.Tweezer:
				toolTweezer.onSelect();
			break;
			
			case (int)TOOL.Suture:
				toolSuture.onSelect();
			break;
			
			case (int)TOOL.Ultra:
				toolUltrasound.onSelect();
			break;
			case (int)TOOL.Scalpel:
				toolScalpel.onSelect();
			break;
			
			case (int)TOOL.Drain:
				toolDrain.onSelect();
			break;
			
			case (int)TOOL.Syringe:
				toolSyringe.onSelect();
			break;
			
			default:
			break;
		}
	}
	
	private void onDeselectOldTool(int toolid){
		switch(toolid)
		{
			
			case (int)TOOL.Laser:
				toolLaser.onDeselect();
			break;
			
			case (int)TOOL.Gel:
				toolGel.onDeselect();
			break;
			
			case (int)TOOL.Tweezer:
				toolTweezer.onDeselect();
			break;
			
			case (int)TOOL.Suture:
				toolSuture.onDeselect();
			break;
			
			case (int)TOOL.Ultra:
				toolUltrasound.onDeselect();
			break;
			
			case (int)TOOL.Scalpel:
				toolScalpel.onDeselect();
			break;
			
			case (int)TOOL.Drain:
				toolDrain.onDeselect();
			break;
			
			case (int)TOOL.Syringe:
				toolSyringe.onDeselect();
			break;
			
			default:
			break;
		}
	}
	private void doToolEffects(){
		switch(toolUsing)
		{
			
			case (int)TOOL.Laser:
				toolLaser.onTouch();
			break;
			
			case (int)TOOL.Gel:
				toolGel.onTouch();
			break;
			
			case (int)TOOL.Tweezer:
				toolTweezer.onTouch();
			break;
			
			case (int)TOOL.Suture:
			if (toolSuture!=null)
				toolSuture.onTouch();
			break;
			
			case (int)TOOL.Ultra:
				toolUltrasound.onTouch();
			break;
			
			case (int)TOOL.Scalpel:
				toolScalpel.onTouch();
			break;
			
			case (int)TOOL.Drain:
				toolDrain.onTouch();
			break;
			
			case (int)TOOL.Syringe:
				toolSyringe.onTouch();
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
	public void changeToolToTalk(){
		changeTool((int)TOOL.Talk);
	}
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
