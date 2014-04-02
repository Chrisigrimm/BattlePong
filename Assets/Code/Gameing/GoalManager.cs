using UnityEngine;
using System.Collections;

public class GoalManager : MonoBehaviour {
	void OnTriggerEnter2D( Collider2D hitInfo ){
		if (hitInfo.name == "Ball") {
			string wallName = transform.name;
			GameManager.Score (wallName);
			hitInfo.gameObject.SendMessage ("ResetBall");
		}
	}
}
