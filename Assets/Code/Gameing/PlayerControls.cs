using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
	public KeyCode moveUp, moveDown;
	public float speed = 10.0f;


	// Use this for initialization
	void Start () {
	}

	void androidControl(){
		if (Input.touchCount > 0) {
			for( int i=0; i < Input.touchCount; i++){
				if(Input.GetTouch(i).phase == TouchPhase.Began){
					rigidbody2D.velocity = new Vector2(0, Input.GetTouch (i).position.y - transform.position.y);
				}
				if (Input.GetTouch (i).phase == TouchPhase.Moved) {
					if (rigidbody2D.name == "Player01") {
						if (Input.GetTouch (i).position.x < Screen.width / 2) {
							// Get movement of the finger since last frame
							//Vector2 touchDeltaPosition = Input.GetTouch (i).deltaPosition;
							// Move object across XY plane
							rigidbody2D.velocity = new Vector2(0,Input.GetTouch (i).position.y);
							//transform.Translate (0,touchDeltaPosition.y * speed, 0);
						}
					}
					if (rigidbody2D.name == "Player02") {
						if (Input.GetTouch (i).position.x > Screen.width / 2) {
							// Get movement of the finger since last frame
							//Vector2 touchDeltaPosition = Input.GetTouch (i).deltaPosition;
			
							// Move object across XY plane
							rigidbody2D.velocity = new Vector2(0,Input.GetTouch (i).position.y);
							//transform.Translate (0,touchDeltaPosition.y * speed, 0);
						}
					}
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {

		if (Application.platform == RuntimePlatform.Android) {
			androidControl();
		}else{
		Vector2 v = rigidbody2D.velocity;
		Vector3 pos = rigidbody2D.transform.position;

		if (Input.GetKey (moveUp)) {
			v.y = speed;
			rigidbody2D.velocity = v;
		}	else if (Input.GetKey (moveDown)) {
			v.y = speed * -1.0f;
			rigidbody2D.velocity = v;
			}	else {
					v.y = speed * 0.0f;
					rigidbody2D.velocity = v;
				}
		if( pos.y > 7){pos.y = 7;rigidbody2D.transform.position = pos;}
		if (pos.y < -7) {pos.y = -7;rigidbody2D.transform.position = pos;}
		}
	}
}
