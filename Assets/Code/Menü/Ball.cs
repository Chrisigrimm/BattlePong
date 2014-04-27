using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public float ballSpeed = 50;
	public AudioClip Hit1;
	public AudioClip Hit2;
	private float maxVelocity;
	private AudioSource Sound;
	// Use this for initialization	
	void Start () {
		ballSpeed = ballSpeed * 0.25f;
		maxVelocity = ballSpeed/5;
		rigidbody2D.AddForce( new Vector2(ballSpeed,Random.Range (-25,25)));
		Sound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate() {
		if(Mathf.Abs(rigidbody2D.velocity.x) < maxVelocity || Mathf.Abs(rigidbody2D.velocity.y) < maxVelocity)
		{
			Vector2 newVelocity = rigidbody2D.velocity.normalized;
			newVelocity *= maxVelocity;
			rigidbody2D.velocity = newVelocity;
		}
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxVelocity || Mathf.Abs(rigidbody2D.velocity.y) > maxVelocity)
		{
			Vector2 newVelocity = rigidbody2D.velocity.normalized;
			newVelocity *= maxVelocity;
			rigidbody2D.velocity = newVelocity;
		}
	}

	void OnCollisionEnter2D( Collision2D colInfo ){	
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x+Random.Range (-2,2),rigidbody2D.velocity.y+Random.Range (-2,2));
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
}
