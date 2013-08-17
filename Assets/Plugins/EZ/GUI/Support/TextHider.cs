//-----------------------------------------------------------------
//  Copyright 2010 Brady Wright and Above and Beyond Software
//	All rights reserved
//-----------------------------------------------------------------


using UnityEngine;
using System.Collections;


public class TextHider : MonoBehaviour 
{
	IControl parentControl;
	
	public IControl Parent
	{
		get { return parentControl; }
		set { parentControl = value; }
	}
	
	void OnEnable()
	{
		if(parentControl == null)
			return;
		
		if(parentControl is AutoSpriteControlBase)
		{
			AutoSpriteControlBase c = (AutoSpriteControlBase)parentControl;
			if(c.IsHidden())
				gameObject.active = false;
			else 
				gameObject.active = c.gameObject.active;
		}
		else
			gameObject.active = ((Component)parentControl).gameObject.active;
	}
}
