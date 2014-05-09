using UnityEngine;
using System.Collections;

   //////////////
  ////G-Tec/////
 ////Keyjin////
//////////////

public class SendToStage : MonoBehaviour {
	void OnClick(){
		GameObject.Find("_StateManager").SendMessage("getClick",gameObject.name);
	}
}
