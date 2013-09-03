using UnityEngine;
using System.Collections;

public class CamerMover : MonoBehaviour {
	
	public GameObject camera;
	private Transform oldPosition, newPosition;
	public bool bMoveCamera;
	public Transform[] camPositions;
	public Patient patient;
	public TrailRenderer HeartBeepsLine;
	
	private Vector3 originalCameraPosition;
	
	public ActionStageManager actionStageManager;

	
	// Update is called once per frame
	void Update () {
		//good for debugging
		if (bMoveCamera && newPosition!=null){
			if (newPosition==null)Debug.LogError("new position null");
			if (camera.transform.position==null)Debug.LogError("Cam pos null");
			float distance = Vector3.Distance(camera.transform.position,newPosition.position);
			//Debug.Log(distance);
			if (distance<=.16) {
				camera.transform.position = newPosition.position;
				bMoveCamera = false;
				patient.setImmune(false);
				HeartBeepsLine.enabled = true;
				actionStageManager.OnCameraStoppedMoving();
				
			} else{
				patient.setImmune(true);
				camera.transform.position = Vector3.Lerp(oldPosition.position,newPosition.position,Time.deltaTime);	
				HeartBeepsLine.enabled = false;
			}
		}
	
	}
	
	void Start(){
		if (camera==null)Debug.LogError("camera not set!");
		originalCameraPosition = camera.transform.position;
	}
	public void returnCameraToMain(){
		camera.transform.position = originalCameraPosition;
	}
	
	public void moveCameraTo(Transform newPosition){
		this.oldPosition = camera.transform;
		this.newPosition = newPosition;
		bMoveCamera=true;
	}
	public void moveCameraTo(int positionIndex){
		if (positionIndex>=camPositions.Length){
			Debug.LogError("Moving camera to a null position, because index is too high or you forgot to add camera positions!");
			return;
		}
		this.oldPosition = camera.transform;
		this.newPosition = camPositions[positionIndex];
		bMoveCamera=true;
	}
	
	public void moveCameraToInstantly(Transform newPosition){
		camera.transform.position = newPosition.position;
		bMoveCamera=false;
		actionStageManager.OnCameraStoppedMoving();
	}
}
