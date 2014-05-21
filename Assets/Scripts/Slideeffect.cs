using UnityEngine;
using System.Collections;

public class Slideeffect : MonoBehaviour {

	public float Slidelength;
	public Animation[] Play;
	private float StartposX, EndPosX;
	public static bool getSlided;
	// Use this for initialization

	void OnPress(){
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.touchCount > 0) {
					Touch touch = Input.GetTouch (0);
					if (touch.phase == TouchPhase.Began) {
							StartposX = touch.position.x;
					} else if (touch.phase == TouchPhase.Ended) {
							EndPosX = touch.position.x;
							if (StartposX < EndPosX) {
									if ((EndPosX - StartposX) > Slidelength)
											getSlided = true;
							} else {
									if ((StartposX - EndPosX) > Slidelength)
											getSlided = true;
							}
					}
			}
		}else{
			getSlided = true;
		}
	}
}
