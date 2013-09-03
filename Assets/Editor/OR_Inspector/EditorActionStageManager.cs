using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ActionStageManager))] 
public class EditorActionStageManager : Editor {
	
	private SerializedObject actionStageManager;   //Add the runner as a Serialized Object
    private SerializedProperty actionStages; //Add the shapes list as a S. Property 
	private ActionStageManager asm;
	private Vector2 scrollPosition = Vector2.zero;
	SerializedProperty m_Property;

    void OnEnable() //On Enable(click) find the runner and shapes list
    {
        actionStageManager = new SerializedObject(target);
		asm  = actionStageManager.targetObject as ActionStageManager;
		m_Property = actionStageManager.FindProperty("stages");
    }
	
	public override void OnInspectorGUI() {
		m_Property = actionStageManager.FindProperty("stages");
		/*EditorGUILayout.BeginVertical();
		do {
		if (m_Property.propertyPath != "stages" && !m_Property.propertyPath.StartsWith("stages" + ".") ) {
			break;
		}
		EditorGUILayout.PropertyField(m_Property);
			Debug.Log(asm.currentStage);
			//lets get another one
			//Debug.Log(m_Property.GetType());
			//Debug.Log(m_Property.serializedObject.GetType());
			//Debug.Log(m_Property.serializedObject.targetObject.GetType());
			//EditorGUILayout.LabelField(temp.name,"",GUILayout.MaxWidth(200f));
			//SerializedProerpty m_Property2 = m_Property.serializedObject.FindProperty("ActionStage");
			//EditorGUILayout.PropertyField(m_Property2);
		} while (m_Property.NextVisible(true));
		EditorGUILayout.EndVertical();
		
		// Apply the property, handle undo
		actionStageManager.ApplyModifiedProperties();
		//DrawDefaultInspector();
		DrawDefaultInspector();
		
		*/
		//DrawDefaultInspector();
		//lets draw the mandatory things here
		asm.npcTextDialog = EditorGUILayout.ObjectField("NPC TextDialog", asm.npcTextDialog, typeof(NPCTextHandler), true) as NPCTextHandler;
		asm.customText = EditorGUILayout.ObjectField("GUISkin", asm.customText, typeof(GUISkin), true) as GUISkin;
		asm.cameraShake = EditorGUILayout.ObjectField("CameraShaker", asm.cameraShake, typeof(CameraShake), true) as CameraShake;
		asm.enemySpawnArea = EditorGUILayout.ObjectField("EnemySpawnArea", asm.enemySpawnArea, typeof(Transform), true) as Transform;
		asm.levelHandler = EditorGUILayout.ObjectField("LevelHandler", asm.levelHandler, typeof(LevelHandler), true) as LevelHandler;
		asm.operatingUIEvents = EditorGUILayout.ObjectField("OperatingUIEvents",asm.operatingUIEvents, typeof(OperatingUIEvents),true) as OperatingUIEvents;
		asm.camMover = EditorGUILayout.ObjectField("CamMover",asm.camMover, typeof(CamerMover),true) as CamerMover;
		scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, new GUILayoutOption[1]{GUILayout.MinHeight(500)});
 
 

		EditorGUILayout.Separator();
		EditorGUILayout.SelectableLabel("Here we put the stages");
		
		if (GUILayout.Button("Skip Current Stage")) {
			asm.skipStage();
    	}
		int displaySize = asm.stages.Length;
		int changedSize = EditorGUILayout.IntField("How many stages",displaySize);
		if (displaySize!=changedSize && changedSize!=0 && changedSize!=null){
			System.Array.Resize(ref asm.stages, changedSize);	
		}
		int counter = 0;
		int totalsize = asm.stages.Length;
		if (asm.shouldShakeOnSpawn.Length < totalsize)System.Array.Resize(ref asm.shouldShakeOnSpawn, totalsize*2);
		if (asm.cameraCenterLocations.Length < totalsize)System.Array.Resize(ref asm.cameraCenterLocations, totalsize*2);
		if (asm.delayTimer.Length < totalsize)System.Array.Resize(ref asm.delayTimer, totalsize*2);
		if (asm._expandedActionStages.Length < totalsize)System.Array.Resize(ref asm._expandedActionStages, totalsize*2);
		if (asm.needTobeVisible.Length < totalsize)System.Array.Resize(ref asm.needTobeVisible, totalsize*2);
		if (asm.shouldBlackOutIntoScene.Length < totalsize)System.Array.Resize(ref asm.shouldBlackOutIntoScene, totalsize*2);
		if (asm.instanlyMoveTo.Length < totalsize)System.Array.Resize(ref asm.instanlyMoveTo, totalsize*2);
		if (asm.stageNPCDialog.Length < totalsize)System.Array.Resize(ref asm.stageNPCDialog, totalsize*2);
         if (totalsize>0){
		foreach (GameObject go in asm.stages){
				
			bool bShouldToggle;
			
				if (go==null || go.GetComponent<ActionStage>()==null)continue;
			SerializedObject aStage = new SerializedObject(go.GetComponent<ActionStage>());
			SerializedObject objName = new SerializedObject(go);
			string foldoutName = "No Stage";
			if (asm.stages[counter]!=null) {
					foldoutName = asm.stages[counter].name;
				}
			if (counter==(asm.currentStage-1) && asm.customText!=null){ //Just to green style the current stage!
				EditorGUILayout.TextField("Active",asm.customText.GetStyle("label"));
			}else {
			
				}
			asm._expandedActionStages[counter] = EditorGUILayout.Foldout(asm._expandedActionStages[counter], foldoutName);//GUILayout.Toggle(asm._expandedActionStages[counter], "Click to toggle", GUILayout.ExpandWidth(true));
			if (asm._expandedActionStages[counter]) {
			asm.stages[counter] = EditorGUILayout.ObjectField("ActionStage", asm.stages[counter], typeof(GameObject), true) as GameObject;
			asm.cameraCenterLocations[counter] = EditorGUILayout.ObjectField("cameraPosition", asm.cameraCenterLocations[counter], typeof(Transform), true) as Transform;
			asm.shouldShakeOnSpawn[counter] = EditorGUILayout.Toggle("shakeOnSpawn",asm.shouldShakeOnSpawn[counter]);
			asm.shouldBlackOutIntoScene[counter] = EditorGUILayout.Toggle("BlackoutIntoScene",asm.shouldBlackOutIntoScene[counter]);
			asm.delayTimer[counter] = EditorGUILayout.FloatField("postDelayTimer",asm.delayTimer[counter]);
			asm.needTobeVisible[counter] = EditorGUILayout.ObjectField("BackgroundObjects", asm.needTobeVisible[counter], typeof(GameObject), true) as GameObject;
			asm.instanlyMoveTo[counter] = EditorGUILayout.Toggle("Move Cam Instantly", asm.instanlyMoveTo[counter]);
			asm.stageNPCDialog[counter] = EditorGUILayout.TextField("NPC TEXT", asm.stageNPCDialog[counter]);
					
			}      
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			//cereal.ApplyModifiedProperties();
			counter++;
		}
		}
		EditorGUILayout.EndScrollView();
		/*EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("shouldShakeCamera","",GUILayout.MaxWidth(200f));
		EditorGUILayout.PropertyField(actionStages, GUIContent.none, GUILayout.MaxWidth(100f));
			
		GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        */
	}
}
