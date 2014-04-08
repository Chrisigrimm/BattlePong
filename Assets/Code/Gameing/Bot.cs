using UnityEngine;
using System.Collections;

   //////////////
  ////G-Tec/////
 ////Keyjin////
//////////////

public class Bot : MonoBehaviour {
	public float speed = 20.0f;
	//Normal Human ReaktionTime
	public float ReaktionTime = 0.112f;
	public float Difficult = 1;
	private GameObject Ball;
	private Vector2 BallDestinationPos;
	private string SaveColideName ="";
	private Vector2 SaveScore;
	private RaycastHit2D hit;
	private Vector2 Position;
	private Vector2 savePos;
	private Vector2 Direction;
	private Vector2 saveDir;
	private Vector2 ReflectScale;
	private float Reaktion;
	private int RandPos;
	private Vector2 tempBallDestination;
	// Use this for initialization
	void Start () {
		Ball = GameObject.FindGameObjectWithTag("Ball");
		SaveScore = GameManager.getScore();
	}
	
	// Update is called once per frame
	void Update () {
		AI();
		ResetAfterGoal();
	}

	void FixedUpdate(){
	}

	void AI(){ 	
		//--Var--//
		Vector2 PlayerPos = new Vector2(transform.position.x,transform.position.y);
		//--Killable--//
		//Reaktion
		if( Reaktion < Time.time){
			if(Reaktion==0f){
				Reaktion = Time.time + ReaktionTime;
				RandPos = Mathf.RoundToInt(Random.Range( -(transform.localScale.y*0.8f) , (transform.localScale.y*0.8f)));
			}
			if(Mathf.Clamp(Ball.rigidbody2D.velocity.x,-1,1) == Mathf.Clamp(transform.position.x,-1,1)){
				FindBallPath();
			}
		}
		//--Move--//
		float UpDown = Mathf.Clamp(BallDestinationPos.y-PlayerPos.y,-1,1);
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, speed * UpDown );

	}

	void FindBallPath(){
		hit = Physics2D.Raycast( Position , Direction , Mathf.Infinity , 9 );
		Debug.DrawLine(Ball.transform.position,BallDestinationPos); 
		if( hit ){
			if(BallDestinationPos != hit.point){
				BallDestinationPos = hit.point;
				spinBall();
	
				if( hit.collider.name == "topWall" || hit.collider.name == "buttomWall" ){
					if( SaveColideName == ""){
						float ScalingY = Mathf.Clamp (Ball.rigidbody2D.velocity.y,1,-1);
						float ScalingX = Mathf.Clamp (Ball.rigidbody2D.velocity.x,-1,1);
						ReflectScale = new Vector2(((Ball.transform.localScale/100)/2).y*ScalingX , ((Ball.transform.localScale/100)/2).y*ScalingY );
						SaveColideName = hit.collider.name;
						savePos = new Vector2(hit.point.x , hit.point.y)+ReflectScale;
						saveDir = new Vector2(Ball.rigidbody2D.velocity.x,-Ball.rigidbody2D.velocity.y);
					}
					if( SaveColideName != hit.collider.name){
						SaveColideName = hit.collider.name;
						ReflectScale = -ReflectScale;
						savePos = new Vector2(hit.point.x+ReflectScale.x, hit.point.y+ReflectScale.y);
						saveDir = new Vector2(saveDir.x,-saveDir.y);
					}
					Position = savePos;
					Direction = saveDir;
				}else{
					if( SaveColideName == ""){
						Position = Ball.transform.position+Ball.transform.localScale/100;
						Direction = new Vector2(Ball.rigidbody2D.velocity.x,Ball.rigidbody2D.velocity.y);
					}
				}
			}
		}else{
			if( Ball.rigidbody2D.velocity.x > 0 && tempBallDestination==Vector2.zero){
				Position = Ball.transform.position;
				Direction = Ball.rigidbody2D.velocity;
			}
		}
	}
	
	void ResetPath(){
		SaveColideName = "";
		Position = Vector2.zero;
		Direction = Vector2.zero;
		Reaktion=0f;
		tempBallDestination = Vector2.zero;
	}	

	void spinBall(){
		if(tempBallDestination == Vector2.zero){
			tempBallDestination = BallDestinationPos;
		}
		if((new Vector2(transform.position.x,transform.position.y)-BallDestinationPos).magnitude >=
		   ((new Vector2(Ball.transform.position.x,Ball.transform.position.y)-BallDestinationPos)).magnitude){
			BallDestinationPos = tempBallDestination;
			tempBallDestination = Vector2.zero;
		}else{
			if(Mathf.Round(BallDestinationPos.sqrMagnitude) != Mathf.Round(new Vector2(transform.position.x,transform.position.y).sqrMagnitude)){
				BallDestinationPos = transform.position;
			}
		}
	}

	void OnCollisionEnter2D( Collision2D colInfo ){	
		if (colInfo.collider.tag == "Ball") {
			ResetPath();
		}
	}

	void ResetAfterGoal(){
		if(SaveScore != GameManager.getScore()){
			SaveScore = GameManager.getScore();
			ResetPath();
			BallDestinationPos = Vector2.zero;
		}
	}
}
