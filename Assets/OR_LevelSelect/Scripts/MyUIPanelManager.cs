using UnityEngine;
using System.Collections;

public class MyUIPanelManager : UIPanelManager {

	IEnumerator Start()
	{
		ScanChildren();

		if (initialPanel != null)
		{
			curPanel = initialPanel;
			breadcrumbs.Add(curPanel);
		}

		if (circular)
			linearNavigation = true;

		if(deactivateAllButInitialAtStart)
		{
			// Wait a frame so the contents of the panels
			// are done Start()'ing, or else we'll get
			// unhidden stuff:
			yield return null;

			for (int i = 0; i<panels.Count; ++i)
				if (panels[i] != initialPanel && panels[i] != curPanel)
					panels[i].gameObject.SetActiveRecursively(false);
		}
		
		//here i do this neat trick to change default panels
		//if they already seen it
		if (PlayerPrefs.GetInt("startOnPanel",0)!=0){
			MoveForward();//that way it will auto move forward
		}
		
	}
	
}
