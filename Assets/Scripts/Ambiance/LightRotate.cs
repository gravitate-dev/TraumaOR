using UnityEngine;
using System.Collections;

public class LightRotate : MonoBehaviour {
	
	public float rotationVelocity = 100;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.rotation.x <= -.5) {
			rotationVelocity *= -1;
			transform.Rotate(Vector3.right * Time.deltaTime*rotationVelocity*3);
		}
		if (transform.rotation.x >= -.3) {
			rotationVelocity *= -1;
			transform.Rotate(Vector3.right * Time.deltaTime*rotationVelocity*3);
		}
		transform.Rotate(Vector3.right * Time.deltaTime*rotationVelocity);
	}
}
