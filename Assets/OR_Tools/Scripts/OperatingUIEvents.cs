using UnityEngine;
using System.Collections;

public class OperatingUIEvents : MonoBehaviour {
	
	
	
	public Material gelmat,drainmat,syringemat,tweezermat;
	public Material lasermat,scalpelmat,suturemat,ultramat;
	
	public int selectedTool = -1;
	
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
	
	IEnumerator flashIt(int index){
		Material matToAlter = getMaterialByIndex(index);
		if (matToAlter==null)yield break;
		
		while(getSelectedTool()!=index) {
		matToAlter.SetColor("_Color",Color.red);
		yield return new WaitForSeconds(0.5f);
		matToAlter.SetColor("_Color",Color.white);
		yield return new WaitForSeconds(0.5f);
		}
		matToAlter.SetColor("_Color",Color.yellow);
	}
	
	void Start(){
		//all material colors should be WHITE HERE
		for (int i =0; i <10; i++)unsetSelectedTool(i); //this was the old tool
	}
	

	public int getSelectedTool(){
		return selectedTool;
	}
	
	public void unsetSelectedTool(int selectedTool){
		Material matToAlter = getMaterialByIndex(selectedTool);
		if (matToAlter==null)return;
		matToAlter.SetColor("_Color",Color.white);
	}
	
	public void setSelectedTool(int toolIndex){
		unsetSelectedTool(selectedTool); //this was the old tool
		selectedTool = toolIndex;
		Material matToAlter = getMaterialByIndex(toolIndex);
		if (matToAlter==null)return;
		matToAlter.SetColor("_Color",Color.yellow);
	}
	public void hintUseTool(int index)
	{
		if (index==-1)return; //-1 means no hint
		StartCoroutine(flashIt(index));
	}
	
	private Material getMaterialByIndex(int index){
		Material matToAlter = null;
		switch(index){
		case (int)TOOL.Suture:
			matToAlter = suturemat;
			break;
		case (int)TOOL.Gel:
			matToAlter = gelmat;
			break;
		case (int)TOOL.Drain:
			matToAlter = drainmat;
			break;
		case (int)TOOL.Tweezer:
			matToAlter = tweezermat;
			break;
		case (int)TOOL.Ultra:
			matToAlter = ultramat;
			break;
		case (int)TOOL.Scalpel:
			matToAlter = scalpelmat;
			break;
		case (int)TOOL.Syringe:
			matToAlter = syringemat;
			break;
		case (int)TOOL.Bandage:
			
			break;
		case (int)TOOL.Laser:
			matToAlter = lasermat;
			break;
		case (int)TOOL.Talk:
			break;
		default:
			break;
		}
		return matToAlter;
	}
}
