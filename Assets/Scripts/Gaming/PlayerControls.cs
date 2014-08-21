using UnityEngine;
using Assets.Code.States;

   //////////////
  ////G-Tec/////
 ////Keyjin////
//////////////

public class PlayerControls : MonoBehaviour {
	public KeyCode moveUp, moveDown;
	public float speed = 10.0f;
	private float TouchPosY, MousePosY;
	private float UpDown;
	private GameObject Ball;
	private BoxCollider2D PlayerCollider;
	private GameObject TopWall, BottomWall;
	private string HitWall = "";
	// Use this for initialization
	void Start () {
		TopWall = GameObject.Find ("TopWall");
		BottomWall = GameObject.Find ("BottomWall");
		Ball = GameObject.FindGameObjectWithTag("Ball");
		PlayerCollider = GetComponent<BoxCollider2D>();
	}
	// Update is called once per frame
	void Update () {
		if( HitWall == "" ){
			if ((TopWall.transform.position.y - (TopWall.GetComponent<BoxCollider2D> ().size.y * 0.5f)) <
					(transform.position.y + (GetComponents<BoxCollider2D> () [0].size.y * 0.5f))) {
				transform.position = new Vector2(transform.position.x,(TopWall.transform.position.y - (TopWall.GetComponent<BoxCollider2D> ().size.y *0.5f)) - (GetComponents<BoxCollider2D> () [0].size.y * 0.5f));
				HitWall = "TopWall";
			}
			if((BottomWall.transform.position.y + (BottomWall.GetComponent<BoxCollider2D> ().size.y * 0.5f)) >
			         (transform.position.y - (GetComponents<BoxCollider2D> () [0].size.y * 0.5f))) {
				transform.position = new Vector2(transform.position.x,(BottomWall.transform.position.y + (BottomWall.GetComponent<BoxCollider2D> ().size.y * 0.5f)) + (GetComponents<BoxCollider2D> () [0].size.y * 0.5f));
				HitWall = "BottomWall";
			}
		}
		#if UNITY_ANDROID || UNITY_IPHONE
			androidControl ();
		#else
			computerControl ();
		#endif
	}

	void computerControl(){
		if (StateManager.MouseControl) {
			MousePosY = Camera.main.ScreenToWorldPoint(new Vector3(0,Input.mousePosition.y,0)).y;
			UpDown = MousePosY - transform.position.y;
			if (HitWall=="TopWall" && UpDown>0) {
				UpDown=0;
			}else if(HitWall=="TopWall" && UpDown<0){
				HitWall = "";
			}
			if (HitWall=="BottomWall" && UpDown<0){
				UpDown=0;
			}else if(HitWall=="BottomWall" && UpDown>0){
				HitWall = "";
			}
			if( UpDown < 1 || UpDown > -1){
				UpDown = UpDown * speed;
			}
			rigidbody2D.velocity = new Vector2(0, UpDown );
		}else{
			Vector2 v = rigidbody2D.velocity;
			Vector3 pos = rigidbody2D.transform.position;
			if (Input.GetKey (moveUp) && HitWall!="TopWall") {
				if(HitWall=="BottomWall"){
					HitWall = "";
				}
				v.y = speed;
				rigidbody2D.velocity = v;
			}else if (Input.GetKey (moveDown) && HitWall!="BottomWall") {
				if(HitWall=="TopWall"){
					HitWall = "";
				}
				v.y = speed * -1.0f;
				rigidbody2D.velocity = v;
			}	else {
				v.y = 0.0f;
				rigidbody2D.velocity = v;
			}
		}
	}

	void androidControl(){
		if (Input.touchCount > 0) {
			for( int i=0; i < Input.touchCount; i++){
				Touch touch = Input.GetTouch(i);
				if (rigidbody2D.name == "Player01") {
					if (touch.position.x < Screen.width / 2) {
						TouchPosY = Camera.main.ScreenToWorldPoint(new Vector3(0,touch.position.y,0)).y;
						UpDown = TouchPosY - transform.position.y;
						if (HitWall=="TopWall" && UpDown>0) {
							UpDown=0;
						}else if(HitWall=="TopWall" && UpDown<0){
							HitWall = "";
						}
						if (HitWall=="BottomWall" && UpDown<0){
							UpDown=0;
						}else if(HitWall=="BottomWall" && UpDown>0){
							HitWall = "";
						}
						if( UpDown < 1 || UpDown > -1){
							UpDown = UpDown * speed;
						}
						rigidbody2D.velocity = new Vector2(0, UpDown);
					}
				}
				if (rigidbody2D.name == "Player02") {
					if (touch.position.x > Screen.width / 2) {
						TouchPosY = Camera.main.ScreenToWorldPoint(new Vector3(0,touch.position.y,0)).y;
						UpDown = TouchPosY - transform.position.y;
						if (HitWall=="TopWall" && UpDown>0) {
							UpDown=0;
						}else if(HitWall=="TopWall" && UpDown<0){
							HitWall = "";
						}
						if (HitWall=="BottomWall" && UpDown<0){
							UpDown=0;
						}else if(HitWall=="BottomWall" && UpDown>0){
							HitWall = "";
						}
						if( UpDown < 1 || UpDown > -1){
							UpDown = UpDown * speed;
						}
						rigidbody2D.velocity = new Vector2(0, UpDown);
					}
				}
			}
		}
	}

	void ResetPlayer(){
		transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
		rigidbody2D.velocity = new Vector2 (0, 0);
	}
}
