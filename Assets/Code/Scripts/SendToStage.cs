using UnityEngine;
using System.Collections;

public class SendToStage : MonoBehaviour {
	void OnClick(){
		GameObject.Find("_StateManager").SendMessage("getClick",gameObject.name);
	}
}
