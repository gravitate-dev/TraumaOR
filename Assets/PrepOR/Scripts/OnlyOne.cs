using UnityEngine;
using System.Collections;

public class OnlyOne : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject[] objs= GameObject.FindGameObjectsWithTag("cyln");
		if (objs.Length!=0){
			foreach(GameObject o in objs){
				if (o==gameObject)
					continue;
				Destroy(o);
			}
		}
		gameObject.tag="cyln";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
