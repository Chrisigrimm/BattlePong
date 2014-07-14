using UnityEngine;
using System.Collections;
using Assets.Code.States;

   //////////////
  ////G-Tec/////
 ////Keyjin////
//////////////

public class Bot : MonoBehaviour {
	public float speed = 20.0f;
	//Normal Human ReaktionTime
	public float ReaktionTime = 0.112f;
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
	private GameObject TopWall, BottomWall;
	// Use this for initialization
	void Start () {
		//ScaleToFormat.getAspectRatio();
		TopWall = GameObject.Find ("TopWall");
		BottomWall = GameObject.Find ("BottomWall");
		//speed = ScaleToFormat.getVel(new Vector2(0,speed),new Vector2(16,9)).y;
		Ball = GameObject.FindGameObjectWithTag("Ball");
		SaveScore = GameState.getScore();
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
		/*if( Reaktion < Time.time){
			if(Reaktion==0f){
				Reaktion = Time.time + ReaktionTime;
				//RandPos = Mathf.RoundToInt(Random.Range( -(transform.localScale.y*0.8f) , (transform.localScale.y*0.8f)));
			}
			if(Mathf.Clamp(Ball.rigidbody2D.velocity.x,-1,1) == Mathf.Clamp(transform.position.x,-1,1)){
				FindBallPath();
			}
		}*/
		if(Mathf.Clamp(Ball.rigidbody2D.velocity.x,-1,1) == Mathf.Clamp(transform.position.x,-1,1)){
			FindBallPath();
		}
		//--Move--//
		float UpDown = Mathf.Clamp(BallDestinationPos.y-transform.position.y,-1,1);
		if (((TopWall.transform.position.y - (TopWall.GetComponent<BoxCollider2D> ().size.y * 0.5f)) <
		     (transform.position.y + (GetComponents<BoxCollider2D> () [0].size.y * 0.5f)))&& UpDown>0) {
			transform.position = new Vector2(transform.position.x,(TopWall.transform.position.y - (TopWall.GetComponent<BoxCollider2D> ().size.y)*TopWall.transform.localScale.y) - (GetComponents<BoxCollider2D> () [1].size.y * 0.5f)*transform.localScale.y);
			UpDown = 0;
		}else if(((BottomWall.transform.position.y + (BottomWall.GetComponent<BoxCollider2D> ().size.y * 0.5f)) >
		         (transform.position.y - (GetComponents<BoxCollider2D> () [0].size.y * 0.5f)))&& UpDown<0) {
			transform.position = new Vector2(transform.position.x,(BottomWall.transform.position.y + (BottomWall.GetComponent<BoxCollider2D> ().size.y * 0.5f)) + (GetComponents<BoxCollider2D> () [0].size.y * 0.5f));
			UpDown = 0;
		}
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, speed * UpDown );
	}

	void FindBallPath(){
		hit = Physics2D.Raycast( Position , Direction , Mathf.Infinity , 10 );
		Debug.DrawLine(Position,BallDestinationPos);
		if( hit ){
			Debug.Log(hit.collider.name);
			if( hit.collider.name != "RailWall" ){
			//letItSpin = Mathf.FloorToInt(Random.Range(Difficult,4));
			BallDestinationPos = hit.point;
				if( hit.collider.name == "TopWall" || hit.collider.name == "BottomWall" ){
					if(hit.collider.name == "TopWall"){
						ReflectScale = new Vector2(0,-Ball.GetComponent<CircleCollider2D>().radius);
					}
					if(hit.collider.name == "BottomWall"){
						ReflectScale = new Vector2(0,Ball.GetComponent<CircleCollider2D>().radius);
					}
					if( SaveColideName == ""){
						SaveColideName = hit.collider.name;
						savePos = hit.point + ReflectScale;
						saveDir = new Vector2(Ball.rigidbody2D.velocity.x,-Ball.rigidbody2D.velocity.y);
					}
					if( SaveColideName != hit.collider.name){
						SaveColideName = hit.collider.name;
						savePos = hit.point + ReflectScale;
						saveDir = new Vector2(saveDir.x,-saveDir.y);
					}
					Position = savePos;
					Direction = saveDir;
				}
			}else{
				BallDestinationPos = hit.point;
				Position = Ball.transform.position;
				Direction = Ball.rigidbody2D.velocity;
			}
		}else{
			if( Ball.rigidbody2D.velocity.x!=0 && tempBallDestination==Vector2.zero){
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
		if((Vector2.Distance(new Vector2(transform.position.x,transform.position.y),tempBallDestination))/speed >=
		   Vector2.Distance(new Vector2(Ball.transform.position.x,Ball.transform.position.y),tempBallDestination)/BallControl.getmaxVelocity()){
			BallDestinationPos = tempBallDestination;
			tempBallDestination = Vector2.zero;
		}else{
			BallDestinationPos = transform.position + new Vector3((GetComponents<BoxCollider2D>()[0].size.x*0.5f)*transform.localScale.x,0,0)*Mathf.Clamp(Direction.x,1,-1);
		}
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
