using UnityEngine;
using System.Collections;

public class LightRotate : MonoBehaviour {
	
	public float rotationVelocity = 100;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(transform.rotation.x);
		if (transform.rotation.x <= -.5) {
			Debug.Log("above");
			rotationVelocity *= -1;
			transform.Rotate(Vector3.right * Time.deltaTime*rotationVelocity*3);
		}
		if (transform.rotation.x >= -.3) {
			Debug.Log("below");
			rotationVelocity *= -1;
			transform.Rotate(Vector3.right * Time.deltaTime*rotationVelocity*3);
		}
		transform.Rotate(Vector3.right * Time.deltaTime*rotationVelocity);
	}
}
