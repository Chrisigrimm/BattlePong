using UnityEngine;
using System.Collections;

public class BattlePongScale : MonoBehaviour {
		
	private string LoadedLevel;
	private Object[] GmObjs;
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName != LoadedLevel && Application.loadedLevelName!=null) {
			LoadedLevel = Application.loadedLevelName;
			switch(LoadedLevel){
				case "Menü":
					Camera Cam = GameObject.Find("Camera").GetComponent<Camera>();
					Vector2 aspect = ScaleToFormat.getAspectRatio();
					Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;
					
					if(ScaleToFormat.isLandScape()){
						break;
					}
					ScaleToFormat.setNewOrthographicSize(Cam,100f,16f);
					GmObjs = GameObject.FindGameObjectsWithTag("Anchor");
					foreach(GameObject GmObj in GmObjs){
						ScaleToFormat.reScaleToFormat(new Vector2(10,16),GmObj);
					}
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
					}
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
				break;
				
			}
		}
	}
}
