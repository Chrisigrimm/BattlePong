using UnityEngine;
using System.Collections;
using Assets.Code.States;

   //////////////
  ////G-Tec/////
 ////Keyjin////
//////////////

public class Bot : MonoBehaviour {
	public float speed = 2f;
	//Normal Human ReaktionTime
	public float ReaktionTime = 0.112f;
	private GameObject Ball;
	private string SaveColideName ="";
	private Vector2 SaveScore;
	private RaycastHit2D hit;
	private Vector2 Position, Direction, ReflectScale, BallDestinationPos, LastHitPoint;
	private float Reaktion;
	private int RandPos;
	private Vector2 tempBallDestination;
	private GameObject TopWall, BottomWall;
	private float DistanceToBot = 0;
	// Use this for initialization
	void Start () {
		TopWall = GameObject.Find ("TopWall");
		BottomWall = GameObject.Find ("BottomWall");
		Ball = GameObject.FindGameObjectWithTag("Ball");
		SaveScore = GameState.getScore();
		Reaktion = RealTime.time + ReaktionTime;
	}
	
	// Update is called once per frame
	void Update () {
		AI();
		ResetAfterGoal();
	}

	void FixedUpdate(){
	}

	void AI(){ 	
		//--Killable--//
		//Reaktion
		//if( Reaktion < RealTime.time){
			if(Mathf.Clamp(Ball.rigidbody2D.velocity.x,-1,1) == Mathf.Clamp(transform.position.x,-1,1)){
				if(tempBallDestination == Vector2.zero){
					BallPath2();
				}else{
					spinBall();
				}
			}
		//}
		//--Move--//
		float UpDown = BallDestinationPos.y - transform.position.y;
		if (((TopWall.transform.position.y - (TopWall.GetComponent<BoxCollider2D> ().size.y * 0.5f)) <
		     (transform.position.y + (GetComponents<BoxCollider2D> () [0].size.y * 0.5f)))&& UpDown>0) {
			transform.position = new Vector2(transform.position.x,(TopWall.transform.position.y - (TopWall.GetComponent<BoxCollider2D> ().size.y * 0.5f)) - (GetComponents<BoxCollider2D> () [0].size.y * 0.5f));
			UpDown = 0;
		}else if(((BottomWall.transform.position.y + (BottomWall.GetComponent<BoxCollider2D> ().size.y * 0.5f)) >
		         (transform.position.y - (GetComponents<BoxCollider2D> () [0].size.y * 0.5f)))&& UpDown<0) {
			transform.position = new Vector2(transform.position.x,(BottomWall.transform.position.y + (BottomWall.GetComponent<BoxCollider2D> ().size.y * 0.5f)) + (GetComponents<BoxCollider2D> () [0].size.y * 0.5f));
			UpDown = 0;
		}
		if( UpDown < 1 || UpDown > -1){
			UpDown = UpDown * speed;
		}
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,  UpDown);
	}

	/*void BallPath(){
		
		hit = Physics2D.Raycast( Ball.transform.position , Ball.rigidbody2D.velocity , Mathf.Infinity , ~(1 << 10) );
		if( hit ){
			if(DistanceToBot == 0){
				DistanceToBot = Vector2.Distance(Ball.transform.position,hit.point);
			}else{
				DistanceToBot += Vector2.Distance(LastHitPoint,hit.point);
			}

			if( hit.collider.name == "TopWall" || hit.collider.name == "BottomWall" ){
				if(hit.collider.name != "Player01" || hit.collider.name != "RailWall"){
						if(hit.collider.name == "TopWall"){
							ReflectScale = new Vector2(0,-Ball.GetComponent<CircleCollider2D>().radius);
						}
						if(hit.collider.name == "BottomWall"){
							ReflectScale = new Vector2(0,Ball.GetComponent<CircleCollider2D>().radius);
						}
						if( SaveColideName == ""){
							SaveColideName = hit.collider.name;
							Position = hit.point + ReflectScale;
							Direction = new Vector2(Ball.rigidbody2D.velocity.x,-Ball.rigidbody2D.velocity.y);
						}
						if( SaveColideName != hit.collider.name){
							SaveColideName = hit.collider.name;
							Position = hit.point + ReflectScale;
							Direction = new Vector2(Direction.x,-Direction.y);
						}
					hit = Physics2D.Raycast( Position , Direction , Mathf.Infinity , ~(1 << 10) );
				}
			}

			if(hit.collider.name == "Player01" || hit.collider.name == "RailWall"){
				//BallDestinationPos = hit.point;
				tempBallDestination = hit.point;
			}

			LastHitPoint = hit.point;
		}else{
			SaveColideName = "";
			Position = Vector2.zero;
			Direction = Vector2.zero;
			tempBallDestination = Vector2.zero;
			LastHitPoint = Vector2.zero;
			DistanceToBot = 0;
		}
	}*/

	void BallPath2(){
		if( Position == Vector2.zero){
			hit = Physics2D.CircleCast(Ball.transform.position ,Ball.GetComponent<CircleCollider2D>().radius, Ball.rigidbody2D.velocity , Mathf.Infinity , ~(1 << 10) );
		}
		if( hit != null ){
			if( hit.collider.name == "TopWall" || hit.collider.name == "BottomWall" ){

				if( hit.collider.name == "TopWall" ){
					Position = hit.point - new Vector2(0,Ball.GetComponent<CircleCollider2D>().radius);
				}else if( hit.collider.name == "BottomWall" ){
					Position = hit.point + new Vector2(0,Ball.GetComponent<CircleCollider2D>().radius);
				}

				if( SaveColideName == ""){
					Direction = new Vector2(Ball.rigidbody2D.velocity.x,-Ball.rigidbody2D.velocity.y);
				}
				if( SaveColideName != hit.collider.name){
					Direction = new Vector2(Direction.x,-Direction.y);
				}

				SaveColideName = hit.collider.name;
				hit = Physics2D.CircleCast(Position ,Ball.GetComponent<CircleCollider2D>().radius, Direction , Mathf.Infinity , ~(1 << 10) );
			}
			if(hit.collider.name == "Player01" || hit.collider.name == "RailWall"){
				//BallDestinationPos = hit.point;
				tempBallDestination = hit.point;
			}
		}else{
			SaveColideName = "";
			Position = Vector2.zero;
			Direction = Vector2.zero;
			tempBallDestination = Vector2.zero;
			LastHitPoint = Vector2.zero;
			DistanceToBot = 0;
		}
		Debug.DrawLine (Position, hit.point);
	}

	void ResetPath(){
		SaveColideName = "";
		Position = Vector2.zero;
		Direction = Vector2.zero;
		Reaktion = RealTime.time + ReaktionTime;
		tempBallDestination = Vector2.zero;
		DistanceToBot = 0;
	}	

	void spinBall(){
		//Debug.Log ("Bot:" + (Vector2.Distance(transform.position,tempBallDestination))/(Mathf.Abs(transform.position.y - tempBallDestination.y)*speed));
		//Debug.Log ("Ball:" + DistanceToBot / (BallControl.sBallSpeed / 5));
		/*if((Vector2.Distance(new Vector2(transform.position.x,transform.position.y),tempBallDestination))/((speed/5) * tempBallDestination.y - transform.position.y) <=
		   DistanceToBot/(BallControl.sBallSpeed/5)){

		}else{
			BallDestinationPos = tempBallDestination;
		}*/
		BallDestinationPos = tempBallDestination;
	}

	void OnCollisionEnter2D( Collision2D colInfo ){	
		if (colInfo.collider.tag == "Ball") {
			ResetPath();
		}
	}

	void ResetAfterGoal(){
		if(SaveScore != GameState.getScore()){
			SaveScore = GameState.getScore();
			ResetPath();
			BallDestinationPos = Vector2.zero;
		}
	}
}
