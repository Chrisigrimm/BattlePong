using UnityEngine;
using System.Collections;

   //////////////
  ////G-Tec/////
 ////Keyjin////
//////////////

public class PlayerControls : MonoBehaviour {
	public KeyCode moveUp, moveDown;
	public float speed = 10.0f;
	private float TouchPosY;
	private float UpDown;
	private GameObject Ball;
	private BoxCollider2D PlayerCollider;
	private GameObject TopWall, ButtomWall;
	private string HitWall;
	// Use this for initialization
	void Start () {
		speed = TransformFormat.getTransVel(new Vector2(0,speed)).y;
		TopWall = GameObject.Find ("topWall");
		ButtomWall = GameObject.Find ("buttomWall");
		Ball = GameObject.FindGameObjectWithTag("Ball");
		PlayerCollider = GetComponent<BoxCollider2D>();
	}
	// Update is called once per frame
	void Update () {
		if ((TopWall.transform.position.y - TopWall.transform.localScale.y) < 
		    (transform.position.y + transform.localScale.y)) {
			HitWall = "TopWall";
		}else if ((ButtomWall.transform.position.y + ButtomWall.transform.localScale.y) > 
		    (transform.position.y - transform.localScale.y)) {
			HitWall = "ButtomWall";
		}else{
			HitWall = "";
		}
		if (Application.platform == RuntimePlatform.Android) {
			androidControl ();
		} else {
			computerControl ();
		}
	}

	void computerControl(){
		Vector2 v = rigidbody2D.velocity;
		Vector3 pos = rigidbody2D.transform.position;
		if (Input.GetKey (moveUp) && HitWall!="TopWall") {
			v.y = speed;
			rigidbody2D.velocity = v;
		}	else if (Input.GetKey (moveDown) && HitWall!="ButtomWall") {
			v.y = speed * -1.0f;
			rigidbody2D.velocity = v;
		}	else {
			v.y = speed * 0.0f;
			rigidbody2D.velocity = v;
		}
	}

	void androidControl(){
		if (Input.touchCount > 0) {
			for( int i=0; i < Input.touchCount; i++){
				Touch touch = Input.GetTouch(i);
				if (rigidbody2D.name == "Player01") {
					if (touch.position.x < Screen.width / 2) {
						TouchPosY = Camera.main.ScreenToWorldPoint(new Vector3(0,touch.position.y,0)).y;
						UpDown = Mathf.Clamp(TouchPosY-transform.position.y,-1,1);
						if (HitWall=="TopWall" && UpDown!=-1) {
							UpDown=0;
						} else if (HitWall=="ButtomWall" && UpDown!=1){
							UpDown=0;
						}
						rigidbody2D.velocity = new Vector2(0, speed * UpDown * Vector2.Distance(new Vector2(0,transform.position.y), new Vector2(0,TouchPosY)));
					}
				}
				if (rigidbody2D.name == "Player02") {
					if (touch.position.x > Screen.width / 2) {
						TouchPosY = Camera.main.ScreenToWorldPoint(new Vector3(0,touch.position.y,0)).y;
						UpDown = Mathf.Clamp(TouchPosY-transform.position.y,-1,1);
						if (HitWall=="TopWall" && UpDown!=-1) {
							UpDown=0;
						} else if (HitWall=="ButtomWall" && UpDown!=1){
							UpDown=0;
						}
						rigidbody2D.velocity = new Vector2(0, speed * UpDown * Vector2.Distance(new Vector2(0,transform.position.y), new Vector2(0,TouchPosY)));
					}
				}
			}
		}else{
			if(( transform.position.y >= TouchPosY && UpDown > 0) || HitWall=="TopWall" ){
				UpDown = 0;
				rigidbody2D.velocity = new Vector2(0,0);
			}
			if( (transform.position.y <= TouchPosY && UpDown < 0 ) || HitWall=="ButtomWall" ){
				UpDown = 0;
				rigidbody2D.velocity = new Vector2(0,0);
			}
		}
	}
}
