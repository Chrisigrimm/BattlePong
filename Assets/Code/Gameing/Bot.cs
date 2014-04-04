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
	// Use this for initialization
	void Start () {
		Ball = GameObject.FindGameObjectWithTag("Ball");
		SaveScore = GameManager.getScore();
	}
	
	// Update is called once per frame
	void Update () {
		AI();
		FindBallPath();
		ResetAfterGoal();
	}
	
	void FindBallPath(){
		if(Mathf.Clamp(Ball.rigidbody2D.velocity.x,-1,1) == Mathf.Clamp(transform.position.x,-1,1) && Reaktion < Time.time){
			hit = Physics2D.Raycast( Position , Direction , Mathf.Infinity , 9 );
			Debug.DrawLine(Ball.transform.position,hit.point);
			if( hit && hit.collider.name!="Infinity"){
				BallDestinationPos = hit.point;
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
			}else{
				if(Ball.rigidbody2D.velocity.x!=0){
					Position = Ball.transform.position;
					Direction = Ball.rigidbody2D.velocity;
				}
			}
		}
	}

	void ResetPath(){
		SaveColideName = "";
		BallDestinationPos = new Vector2(0,0);
		Position = new Vector2(0,0);
		Direction = new Vector2(0,0);
		Reaktion=0f;
	}

	void AI(){
		//--Var--//
		Vector2 PlayerPos = new Vector2(transform.position.x,transform.position.y);
		float UpDown = Mathf.Clamp(BallDestinationPos.y-PlayerPos.y,-1,1);
		//--Killable--//
		//Reaktion
		if( Reaktion < Time.time && Reaktion==0f){
			Reaktion = Time.time + ReaktionTime;
		}
		//--Move--//
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, speed * UpDown);
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
		}
	}
}
