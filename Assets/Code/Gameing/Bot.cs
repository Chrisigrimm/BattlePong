using UnityEngine;
using System.Collections;

public class Bot : MonoBehaviour {
	public float speed = 10.0f;
	public string Botting = "Player02";
	private GameObject Botter;
	private Vector2 BallDestinationPos;
	private string SaveColideName ="";
	private Vector2 SaveScore;
	private RaycastHit2D hit;
	private Vector2 Position;
	private Vector2 savePos;
	private Vector2 Direction;
	private Vector2 saveDir;
	private Vector2 ReflectScale;
	// Use this for initialization
	void Start () {
		Botter = GameObject.Find (Botting);
		SaveScore = GameManager.getScore();
	}
	
	// Update is called once per frame
	void Update () {
		AI();
		FindBallPath();
		ResetAfterGoal();
	}
	
	void FindBallPath(){
		if(Mathf.Clamp(rigidbody2D.velocity.x,-1,1) == Mathf.Clamp(Botter.transform.position.x,-1,1)){
			hit = Physics2D.Raycast( Position , Direction , Mathf.Infinity , 9 );
			Debug.DrawLine(transform.position,hit.point);
			if( hit && hit.collider.name!="Infinity"){
				BallDestinationPos = hit.point;
				if( hit.collider.name == "topWall" || hit.collider.name == "buttomWall" ){
					if( SaveColideName == ""){
						float ScalingY = Mathf.Clamp (rigidbody2D.velocity.y,1,-1);
						float ScalingX = Mathf.Clamp (rigidbody2D.velocity.x,-1,1);
						ReflectScale = new Vector2(((transform.localScale/100)/2).y*ScalingX , ((transform.localScale/100)/2).y*ScalingY );
						SaveColideName = hit.collider.name;
						savePos = new Vector2(hit.point.x , hit.point.y)+ReflectScale;
						saveDir = new Vector2(rigidbody2D.velocity.x,-rigidbody2D.velocity.y);
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
						Position = transform.position+transform.localScale/100;
						Direction = new Vector2(rigidbody2D.velocity.x,rigidbody2D.velocity.y);
					}
				}
			}else{
				if(rigidbody2D.velocity.x!=0){
					Position = transform.position;
					Direction = rigidbody2D.velocity;
				}
			}
		}
	}

	void ResetPath(){
		SaveColideName = "";
		BallDestinationPos = new Vector2(0,0);
		Position = new Vector2(0,0);
		Direction = new Vector2(0,0);
	}

	void AI(){
		Vector2 PlayerPos = new Vector2(Botter.transform.position.x,Botter.transform.position.y);
		Vector2 BallPos = new Vector2(BallDestinationPos.x,BallDestinationPos.y);
		float UpDown = Mathf.Clamp(BallDestinationPos.y-PlayerPos.y,-1,1);
		Botter.rigidbody2D.velocity = new Vector2(Botter.rigidbody2D.velocity.x, speed * UpDown);
	}

	void OnCollisionEnter2D( Collision2D colInfo ){	
		if (colInfo.collider.tag == "Player") {
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
