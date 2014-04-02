using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour {
	public float ballSpeed = 100;
	// Use this for initialization
	void Start () {
		GameObject.Find("CountDown").SendMessage ("cDown");
	}

	void OnCollisionEnter2D( Collision2D colInfo ){	
			if (colInfo.collider.tag == "Player") {
			Vector2 vColInfo = colInfo.collider.rigidbody2D.velocity;
			Vector2 vBall = rigidbody2D.velocity;
			vBall.y = vBall.y/2 + vColInfo.y/3;
			rigidbody2D.velocity = vBall;
			}
	}

	public void ResetBall(){
		gameObject.GetComponent<TrailRenderer>().enabled=false;
		rigidbody2D.velocity = new Vector2 (0, 0);
		transform.position = new Vector3 (0, 0, 0);
		GameObject.Find("CountDown").SetActive(true);
		GameObject.Find("CountDown").SendMessage ("cDown");
	}

	void GoBall(){
		gameObject.GetComponent<TrailRenderer>().enabled=true;
		int rNumber = Random.Range(0, 2);
		if( rNumber <= 0.5){
			rigidbody2D.AddForce( new Vector2(ballSpeed,Random.Range (-25,25)));
			//rigidbody2D.AddForce( new Vector2(5,360));
		} else {
			rigidbody2D.AddForce( new Vector2(-ballSpeed,-Random.Range (-25,25)));
			//rigidbody2D.AddForce( new Vector2(-5,360));
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (rigidbody2D.velocity.x < ballSpeed/5 && rigidbody2D.velocity.x >0){
			rigidbody2D.velocity = new Vector2(20,rigidbody2D.velocity.y);
		}
		if (rigidbody2D.velocity.x > -ballSpeed/5 && rigidbody2D.velocity.x <0) {
			rigidbody2D.velocity = new Vector2(-20,rigidbody2D.velocity.y);
		}
	}
}
