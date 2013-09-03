using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CamerMover))] 
public class EditorCameraMover : Editor {
	
	public override void OnInspectorGUI() {
		CamerMover myTarget = target as CamerMover;
		DrawDefaultInspector();
		
		if(GUILayout.Button("View Camera 0"))
			myTarget.moveCameraTo(0);
		
		if(GUILayout.Button("View Camera 1"))
			myTarget.moveCameraTo(1);
		
		if(GUILayout.Button("View Camera 2"))
			myTarget.moveCameraTo(2);
		
		if(GUILayout.Button("View Camera 3"))
			myTarget.moveCameraTo(3);
		
		if(GUILayout.Button("View Camera 4"))
			myTarget.moveCameraTo(4);
		
		if(GUILayout.Button("View Camera 5"))
			myTarget.moveCameraTo(5);
	}
}