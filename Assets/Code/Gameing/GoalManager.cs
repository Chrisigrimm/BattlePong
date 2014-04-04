using UnityEngine;
using System.Collections;

   //////////////
  ////G-Tec/////
 ////Keyjin////
//////////////

public class GoalManager : MonoBehaviour {
	private GameObject Ball;

	void Start(){
		Ball = GameObject.FindGameObjectWithTag("Ball");
	}
	//Because if the Ball is faster OnTrigger doesn't catch it
	void Update(){
		if(Ball.transform.position.x < -Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,0)).x && name == "leftWall"){
			Goal();
		}
		if(Ball.transform.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,0)).x && name == "rightWall"){
			Goal();
		}
	}

	void Goal(){
		string wallName = transform.name;
		GameManager.Score (wallName);
		Ball.SendMessage("ResetBall");
	}

	/*void OnTriggerEnter2D( Collider2D hitInfo ){
		if (hitInfo.name == "Ball") {
			string wallName = transform.name;
			GameManager.Score (wallName);
			hitInfo.gameObject.SendMessage ("ResetBall");
		}
	}*/
}
