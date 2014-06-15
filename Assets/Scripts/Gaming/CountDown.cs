using UnityEngine;
using System.Collections;

public class CountDown : MonoBehaviour {
	public float Timer = 3;
	private float saveTimer = 3;
	private bool Visible = true;
	public GUISkin theSkin;
	// Update is called once per frame
	void Start(){
		saveTimer = Timer;
	}

	void cDown(){
		Timer = saveTimer;
		Visible = true;
		InvokeRepeating("Delay", 1, 1);
	}

	void Delay(){
		if(Timer !=0 ){
			Timer--;
		}else{
			GameObject.Find("Ball").SendMessage ("GoBall");
			Timer=saveTimer;
			Visible = false;
			CancelInvoke("Delay");
		}
	}
	void Update () {
	}

	void OnGUI(){
		GUI.skin = theSkin;
		if(Timer > 0 && Visible){
			GUI.Label( new Rect(  Screen.width/2 -25 , 25 , 100 ,200 ),"" + Timer );
		}
		if(Timer == 0 && Visible){
			GUI.Label( new Rect( Screen.width/2 -75 , 25 , 200 ,200 ),"Go!" );
		}
	}
}
