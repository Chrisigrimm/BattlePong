using UnityEngine;
using System.Collections;

   //////////////
  ////G-Tec/////
 ////Keyjin////
//////////////

public class BallControl : MonoBehaviour {
	public float ballSpeed = 100;
	public int spread = 15;
	public AudioClip Hit1;
	public AudioClip Hit2;
	private float maxVelocity;
	private AudioSource Sound;
	public static float sBallSpeed;
	// Use this for initialization
	void Start () {
		sBallSpeed = ballSpeed;
		maxVelocity = ballSpeed/5;
		Sound = GetComponent<AudioSource> ();
	}

	void OnCollisionEnter2D( Collision2D colInfo ){	
		if (colInfo.collider.tag == "Player") {
			Vector2 vColInfo = colInfo.collider.rigidbody2D.velocity;
			Vector2 vBall = rigidbody2D.velocity;
			vBall.y = vBall.y/2 + vColInfo.y/3;
			rigidbody2D.velocity = vBall;
		}
		int randSound = Random.Range (1, 1);
		switch (randSound) {
		case 1:
			Sound.clip = Hit1;
			break;
		case 2:
			Sound.clip = Hit2;
			break;
		default:
			Sound.clip = Hit1;
			break;
		}
		Sound.Play();
	}

	public void ResetBall(){
		gameObject.GetComponent<TrailRenderer>().enabled=false;
		rigidbody2D.velocity = new Vector2 (0, 0);
		transform.position = new Vector3 (0, 0, transform.position.z);
	}


	void GoBall(){
		gameObject.GetComponent<TrailRenderer>().enabled=true;
		int rNumber = Random.Range(0, 2);
		if( rNumber <= 0.5){
			rigidbody2D.AddForce( new Vector2(ballSpeed,Random.Range (-spread,spread)));
		} else {
			rigidbody2D.AddForce( new Vector2(-ballSpeed,-Random.Range (-spread,spread)));
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Min Speed
		if (rigidbody2D.velocity.magnitude < maxVelocity && rigidbody2D.velocity.magnitude > 0) {
			float diffrence = maxVelocity / rigidbody2D.velocity.magnitude;
			rigidbody2D.velocity *= diffrence;
		}
	}
}
