using UnityEngine;
using System.Collections;

public class UIButtonStates : MonoBehaviour {

	public GameObject[] SendStageTo;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnHover(bool State){

		foreach(GameObject Obj in SendStageTo){
			Obj.SendMessage("getHover",gameObject,SendMessageOptions.DontRequireReceiver);
		}
	}

	void OnClick(){
		foreach(GameObject Obj in SendStageTo){
			Obj.SendMessage("getClick",gameObject);
		}
	}

	void OnPress (){
		foreach(GameObject Obj in SendStageTo){
			Obj.SendMessage("getPress",gameObject);
		}
	}
}
