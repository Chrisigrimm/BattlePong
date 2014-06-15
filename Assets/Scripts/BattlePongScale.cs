using UnityEngine;
using System.Collections;

public class BattlePongScale : MonoBehaviour {

	public static bool Scaled = false;

	public static bool ScaleGame(){
		Object[] GmObjs;

		switch(Application.loadedLevelName){
		case "Menue":
			Camera Cam = GameObject.Find("Camera").GetComponent<Camera>();
			Vector2 aspect = ScaleToFormat.getAspectRatio();
			Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;
			if(ScaleToFormat.isLandScape()){
				Scaled = true;
				break;
			}
			ScaleToFormat.setNewOrthographicSize(Cam,100f,16f);
			GmObjs = GameObject.FindGameObjectsWithTag("Anchor");
			foreach(GameObject GmObj in GmObjs){
				ScaleToFormat.reScaleToFormat(new Vector2(10,16),GmObj);
			}
			Scaled = true;
			break;
		case "Game":
			ScaleToFormat.getAspectRatio();
			ScaleToFormat.setNewOrthographicSize(Camera.main,1080f,9f);
			GmObjs = FindObjectsOfType (typeof(GameObject));
			foreach(GameObject GmObj in GmObjs){
				SpriteRenderer SpRE = GmObj.GetComponent<SpriteRenderer>();
				if( SpRE ){
					if (!GmObj.transform.parent ){
						ScaleToFormat.reScaleToFormat(new Vector2(16,9),GmObj);
					}else{
						ScaleToFormat.reScaleShaderToFormat(new Vector2(16,9),GmObj);
					}
				}
				if( GmObj.GetComponent<UIPanel>() ){
					ScaleToFormat.reScaleToFormat(new Vector2(16,9),GmObj);
				}
			}
			Scaled = true;
			break;
		case "GameMulti":
			ScaleToFormat.getAspectRatio();
			ScaleToFormat.setNewOrthographicSize(Camera.main,1080f,9f);
			GmObjs = FindObjectsOfType (typeof(GameObject));
			foreach(GameObject GmObj in GmObjs){
				SpriteRenderer SpRE = GmObj.GetComponent<SpriteRenderer>();
				if( SpRE ){
					if (!GmObj.transform.parent ){
						ScaleToFormat.reScaleToFormat(new Vector2(16,9),GmObj);
					}else{
						ScaleToFormat.reScaleShaderToFormat(new Vector2(16,9),GmObj);
					}
				}
			}
			Scaled = true;
			break;
		}

		return Scaled;
	}
}
