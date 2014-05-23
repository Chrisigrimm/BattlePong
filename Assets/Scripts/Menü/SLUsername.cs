using UnityEngine;
using System.Collections;

public class SLUsername : MonoBehaviour {

	//Save Load Username
	public GameObject UsernameOutput;
	// Use this for initialization
	void Awake(){
		//Create when not exist
		SaveLoad.Save ();

		SaveData datas = SaveLoad.Load ();
		UsernameOutput.GetComponent<UILabel> ().text = datas.username;
		GetComponent<UIInput> ().text = datas.username;
	}

	// Update is called once per frame
	void OnSubmit(){
		SaveData datas = SaveLoad.Load ();
		datas.username = GetComponent<UIInput> ().text;
		SaveLoad.Save(datas);
		UsernameOutput.GetComponent<UILabel> ().text = datas.username;
	}
}
