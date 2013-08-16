using UnityEngine;
using System.Collections;

public class ThePlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		 if (Input.GetMouseButtonDown(0)){ // if left button pressed...
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)){
				// the object identified by hit.transform was clicked
				// do whatever you want
					if (hit.transform.tag.Equals("enemyDamage")){
									hit.transform.GetComponent<Scratch>().OnClick();
				}
			}
		}
	}
}
