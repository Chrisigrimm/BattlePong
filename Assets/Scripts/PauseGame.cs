using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

	bool toggleESC;
	float savedTimeScale;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("escape")){
			toggleESC=!toggleESC;
			if( toggleESC == true ){
				GameObject.FindWithTag("UICamera").GetComponent<Camera>().enabled = true;
				PausedGame();
			}else {
				GameObject.FindWithTag("UICamera").GetComponent<Camera>().enabled = false;
				UnPauseGame();
			}
		}
	}

	void PausedGame() {
		savedTimeScale = Time.timeScale;
		Time.timeScale = 0;
		AudioListener.pause = true;
	}
	
	void UnPauseGame() {
		Time.timeScale = savedTimeScale;
		AudioListener.pause = false;
	}
}
