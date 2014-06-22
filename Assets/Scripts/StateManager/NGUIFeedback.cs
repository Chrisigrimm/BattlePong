using UnityEngine;
using System.Collections;

public class NGUIFeedback : MonoBehaviour {

	bool mStarted = false;
	
	void Start () { mStarted = true; }
	
	void OnEnable () { if (mStarted) OnHover(UICamera.IsHighlighted(gameObject)); }
	
	public void OnHover (bool isOver)
	{
		if (enabled)
		{
			if (((isOver) ||
			     (!isOver))) Send("OnHover");
		}
	}
	
	public void OnPress (bool isPressed)
	{
		if (enabled)
		{
			if (((isPressed) ||
			     (!isPressed))) Send("OnPress");
		}
	}
	
	public void OnSelect (bool isSelected)
	{
		if (enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
			OnHover(isSelected);
	}

	public void OnClick () { if (enabled) Send("OnClick"); }
	
	public void OnDoubleClick () { if (enabled) Send("OnDoubleClick"); }
	
	public void OnSubmit () {if (enabled) Send ("OnSubmit");}
	
	public void OnChange () { if (enabled) Send ("OnChange"); }
	
	void Send (string functionName)
	{
		GameObject.Find("_StateManager").SendMessage(functionName, gameObject, SendMessageOptions.DontRequireReceiver);
	}
}

