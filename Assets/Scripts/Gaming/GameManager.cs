using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	static int Score01;
	static int Score02;
	public GUISkin theSkin;
	public static GameObject pausedPanel;

	void Start(){
		ResetScore ();
		if (Application.loadedLevelName == "Game" ){
			pausedPanel = GameObject.Find("Window - Paused");
			NGUITools.SetActive(pausedPanel,false);
		}
	}

	// Set Score
	public static void Score(string wallName){
		if (wallName == "leftWall") {Score02 += 1;}
		if (wallName == "rightWall") {Score01 += 1;}
	}

	public static void ResetScore(){
		Score02 = 0;
		Score01 = 0;
	}

	public static Vector2 getScore(){
		return new Vector2(Score01,Score02);
	}

	void OnGUI(){
		GUI.skin = theSkin;
		if (Application.platform == RuntimePlatform.Android) {
			GUIUtility.RotateAroundPivot(90,new Vector2(Screen.width / 2 - 60,30));
			GUI.Label (new Rect (Screen.width / 2 - 60, -20, 100, 100), "" + Score01);
			GUIUtility.RotateAroundPivot(-90,new Vector2(Screen.width / 2 - 60,30));
			GUIUtility.RotateAroundPivot(-90,new Vector2(Screen.width / 2 + 60,Screen.height-30));
			GUI.Label (new Rect (Screen.width / 2 + 60, Screen.height-80, 100, 100), "" + Score02);
		}	 else {
				GUI.Label (new Rect (Screen.width / 2 - 150, 20, 100, 100), "" + Score01);
				GUI.Label (new Rect (Screen.width / 2 + 150, 20, 100, 100), "" + Score02);
			}
	}
}