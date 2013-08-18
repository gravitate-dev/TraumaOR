using UnityEngine;
using System.Collections;

public class CylinderKiller : MonoBehaviour {
	public GameObject cyln;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Space)){
			Debug.Log("spawning cylinder");
		    Instantiate(cyln, gameObject.transform.position, Quaternion.identity);
		}
	}
	
	public void KillCylinder(){
		Debug.Log("you clicked me");
		transform.Rotate(10*Vector3.right);
	}
}
