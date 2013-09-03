using UnityEngine;
using System.Collections;

public class SyringeAnimation : MonoBehaviour {

	public Transform MaxHeight;
	public Transform MinHeight;
	public Transform syringeBody;
	public float percent;
	public float syringeOffsetScreen;
	void Update (){
		if (percent >100.0f)percent = 100.0f;
		if (percent < 0.0f)percent=0.0f;
		float decimalPercent = percent/100.0f;
		
		syringeBody.position = new Vector3(syringeBody.position.x,Mathf.Lerp(MinHeight.position.y,MaxHeight.position.y,decimalPercent),syringeBody.position.z);
	}
	public void putInJar(Transform Jar){
		Transform dockPosition = Jar.FindChild("DockingPosition");
		if (dockPosition!=null) transform.position = dockPosition.position;
	}
	
	public void putAtLocation(Vector3 pos){
		pos.y += syringeOffsetScreen;
		transform.position = pos;
	}
}
