using UnityEngine;
using System.Collections;

public class MenüBrain : MonoBehaviour {

	public GameObject UsernameInput, UsernameOutput;
	// Use this for initialization
	void Awake(){
		SaveData datas = SaveLoad.Load ();
		UsernameOutput.GetComponent<UILabel> ().text = datas.username;
		UsernameInput.GetComponent<UIInput> ().text = datas.username;
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
}
