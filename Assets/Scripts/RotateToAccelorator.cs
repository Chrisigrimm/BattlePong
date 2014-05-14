using UnityEngine;
using System.Collections;

public class RotateToAccelorator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ((Input.acceleration.y+1)*180);
		//if (Application.platform == RuntimePlatform.Android) {
		transform.rotation = new Quaternion(transform.rotation.x,transform.rotation.y,Input.acceleration.z,transform.rotation.w);
		//}
	}
}
