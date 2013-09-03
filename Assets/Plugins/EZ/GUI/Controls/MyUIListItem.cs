using UnityEngine;
using System.Collections;

public class MyUIListItem : UIListItem
{

	public string LevelToLoad;
	public GameObject levelLoader;
	
	public override void OnInput(ref POINTER_INFO ptr)
	{
		if (!m_controlIsEnabled /*|| IsHidden()*/)
		{
			//if (!IsHidden())
			{
				switch (ptr.evt)
				{
					case POINTER_INFO.INPUT_EVENT.NO_CHANGE:
					case POINTER_INFO.INPUT_EVENT.DRAG:
						list.ListDragged(ptr);
						break;
					case POINTER_INFO.INPUT_EVENT.TAP:
					case POINTER_INFO.INPUT_EVENT.RELEASE:
					case POINTER_INFO.INPUT_EVENT.RELEASE_OFF:
						list.PointerReleased();
						break;
				}
			}

			if(Container != null)
			{
				ptr.callerIsControl = true;
				Container.OnInput(ptr);
			}

			return;
		}

		// Do our own tap checking with the list's
		// own threshold:
		if (Vector3.SqrMagnitude(ptr.origPos - ptr.devicePos) > (list.dragThreshold * list.dragThreshold))
		{
			ptr.isTap = false;
			if (ptr.evt == POINTER_INFO.INPUT_EVENT.TAP)
				ptr.evt = POINTER_INFO.INPUT_EVENT.RELEASE;
		}


		if (inputDelegate != null)
			inputDelegate(ref ptr);


		// Change the state if necessary:
		switch (ptr.evt)
		{
			case POINTER_INFO.INPUT_EVENT.NO_CHANGE:
				if (ptr.active)	// If this is a hold
					list.ListDragged(ptr);
				break;
			case POINTER_INFO.INPUT_EVENT.MOVE:
				if (!selected)
				{
					if (soundOnOver != null && m_ctrlState != CONTROL_STATE.OVER)
						soundOnOver.PlayOneShot(soundOnOver.clip);

					SetControlState(CONTROL_STATE.OVER);
				}
				break;
			case POINTER_INFO.INPUT_EVENT.DRAG:
				if (!ptr.isTap)
				{
					if(!selected)
						SetControlState(CONTROL_STATE.NORMAL);
					list.ListDragged(ptr);
				}
				else
					SetControlState(CONTROL_STATE.ACTIVE);
				break;
			case POINTER_INFO.INPUT_EVENT.PRESS:
				SetControlState(CONTROL_STATE.ACTIVE);
				break;
			case POINTER_INFO.INPUT_EVENT.TAP:
				// Tell our list we were selected:
				list.DidSelect(this);
				list.PointerReleased();
				break;
			case POINTER_INFO.INPUT_EVENT.RELEASE:
			case POINTER_INFO.INPUT_EVENT.RELEASE_OFF:
				if (!selected)
					SetControlState(CONTROL_STATE.NORMAL);
				list.PointerReleased();
				break;
			case POINTER_INFO.INPUT_EVENT.MOVE_OFF:
				if(!selected)
					SetControlState(CONTROL_STATE.NORMAL);
				break;
		}

		if (Container != null)
		{
			ptr.callerIsControl = true;
			Container.OnInput(ptr);
		}

		if (repeat)
		{
			if (m_ctrlState == CONTROL_STATE.ACTIVE)
				goto Invoke;
		}
		else if (ptr.evt == whenToInvoke)
			goto Invoke;

		return;

		Invoke:
		if (ptr.evt == whenToInvoke)
		{
			if (soundOnClick != null)
				soundOnClick.PlayOneShot(soundOnClick.clip);
		}
		//if (levelLoader != null) //i modified it here
					
		if (changeDelegate != null)
			changeDelegate(this);
	}
}

