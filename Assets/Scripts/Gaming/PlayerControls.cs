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
	private string HitWall;
	// Use this for initialization
	void Start () {
		//ScaleToFormat.getAspectRatio();
		//speed = ScaleToFormat.getVel(new Vector2(0,speed),new Vector2(16,9)).y;
		TopWall = GameObject.Find ("TopWall");
		BottomWall = GameObject.Find ("BottomWall");
		Ball = GameObject.FindGameObjectWithTag("Ball");
		PlayerCollider = GetComponent<BoxCollider2D>();
	}
	// Update is called once per frame
	void Update () {
		if ((TopWall.transform.position.y - (TopWall.GetComponent<BoxCollider2D> ().size.y)*TopWall.transform.localScale.y) <
				(transform.position.y + (GetComponents<BoxCollider2D> () [1].size.y * 0.5f)*transform.localScale.y)) {
			transform.position = new Vector2(transform.position.x,(TopWall.transform.position.y - (TopWall.GetComponent<BoxCollider2D> ().size.y)*TopWall.transform.localScale.y) - (GetComponents<BoxCollider2D> () [1].size.y * 0.5f)*transform.localScale.y);
				HitWall = "TopWall";
		}else if((BottomWall.transform.position.y + (BottomWall.GetComponent<BoxCollider2D> ().size.y)*BottomWall.transform.localScale.y) >
		         (transform.position.y - (GetComponents<BoxCollider2D> () [1].size.y * 0.5f)*transform.localScale.y)) {
			transform.position = new Vector2(transform.position.x,(BottomWall.transform.position.y + (BottomWall.GetComponent<BoxCollider2D> ().size.y)*BottomWall.transform.localScale.y) + (GetComponents<BoxCollider2D> () [1].size.y * 0.5f)*transform.localScale.y);
			HitWall = "BottomWall";
		}else{
			HitWall = "";
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
			UpDown = Mathf.Clamp(MousePosY-transform.position.y,-1,1);
			if (HitWall=="TopWall" && UpDown!=-1) {
				UpDown=0;
			} else if (HitWall=="BottomWall" && UpDown!=1){
				UpDown=0;
			}
			rigidbody2D.velocity = new Vector2(0, speed * UpDown * Vector2.Distance(new Vector2(0,transform.position.y), new Vector2(0,MousePosY)));
		}else{
			Vector2 v = rigidbody2D.velocity;
			Vector3 pos = rigidbody2D.transform.position;
			if (Input.GetKey (moveUp) && HitWall!="TopWall") {
				v.y = speed;
				Debug.Log("Test");
				rigidbody2D.velocity = v;
			}	else if (Input.GetKey (moveDown) && HitWall!="BottomWall") {
				v.y = speed * -1.0f;
				rigidbody2D.velocity = v;
			}	else {
				v.y = speed * 0.0f;
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
						UpDown = Mathf.Clamp(TouchPosY-transform.position.y,-1,1);
						if (HitWall=="TopWall" && UpDown!=-1) {
							UpDown=0;
						} else if (HitWall=="BottomWall" && UpDown!=1){
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
						} else if (HitWall=="BottomWall" && UpDown!=1){
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
			if( (transform.position.y <= TouchPosY && UpDown < 0 ) || HitWall=="BottomWall" ){
				UpDown = 0;
				rigidbody2D.velocity = new Vector2(0,0);
			}
		}
	}

	void ResetPlayer(){
		transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
		rigidbody2D.velocity = new Vector2 (0, 0);
	}
}
