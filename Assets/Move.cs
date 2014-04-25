using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.D)) {
			rigidbody2D.velocity = new Vector2 (10, 0);
		} else if (Input.GetKey (KeyCode.A)) {
			rigidbody2D.velocity = new Vector2 (-10, 0);
		} else {
			rigidbody2D.velocity = new Vector2 (0, 0);
		}
	}
}
