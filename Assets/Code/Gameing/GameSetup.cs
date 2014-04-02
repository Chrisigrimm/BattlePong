using UnityEngine;
using System.Collections;

public class GameSetup : MonoBehaviour {

	public Camera mainCam;
	public BoxCollider2D topWall, bottomWall, leftWall, rightWall;
	public static GameObject pausedPanel;
	
	public Transform Player01, Player02, Logo, Goal01, Goal02;

	// Use this for initialization
	void Start () {
		//Logo
		Vector3 posLogo = Logo.position;
		posLogo.x = mainCam.ScreenToWorldPoint (new Vector3 (Screen.width - (Logo.localScale.x*100)-20, 0f, 0f)).x;
		posLogo.y = mainCam.ScreenToWorldPoint (new Vector3 (0f, Logo.localScale.y*100, 0f)).y;
		Logo.position = posLogo;
		//Goalsides
		Goal01.position = mainCam.ScreenToWorldPoint (new Vector3 (Goal01.localScale.x, Screen.height/2, 1f));
		//Goal01.position = Goal01pos;
		Goal02.position = mainCam.ScreenToWorldPoint (new Vector3 (Screen.width - Goal02.localScale.x, Screen.height/2, 1f));
		//Move each wall to its edge location
		topWall.size = new Vector2 ( mainCam.ScreenToWorldPoint ( new Vector3 (Screen.width * 2f , 0f, 0f)).x, 1f);
		topWall.center = new Vector2 (0f, mainCam.ScreenToWorldPoint ( new Vector3 ( 0f , Screen.height, 0f)).y + 0.5f);
		
		bottomWall.size = new Vector2 ( mainCam.ScreenToWorldPoint ( new Vector3 (Screen.width * 2f , 0f, 0f)).x, 1f);
		bottomWall.center = new Vector2 (0f, mainCam.ScreenToWorldPoint ( new Vector3 ( 0f , 0f, 0f)).y - 0.5f);
		
		leftWall.size = new Vector2 (1f, mainCam.ScreenToWorldPoint ( new Vector3 (0f , Screen.height*2f, 0f)).y);
		leftWall.center = new Vector2 (mainCam.ScreenToWorldPoint ( new Vector3 ( 0f , 0f , 0f)).x - 0.5f,0f);
		
		rightWall.size = new Vector2 (1f, mainCam.ScreenToWorldPoint ( new Vector3 (0f , Screen.height * 2f, 0f)).y);
		rightWall.center = new Vector2 (mainCam.ScreenToWorldPoint ( new Vector3 (  Screen.width ,0f, 0f)).x+ 0.5f, 0f);
		//Player
		Player01.position = new Vector3 (mainCam.ScreenToWorldPoint ( new Vector3 ( 30f , 0f, 0f)).x, 0f, 0f);
		Player02.position = new Vector3 (mainCam.ScreenToWorldPoint ( new Vector3 ( Screen.width - 30f , 0f, 0f)).x, 0f, 0f);

		pausedPanel = GameObject.Find("Window - Paused");
		NGUITools.SetActive(pausedPanel,false);
	}

	// Update is called once per frame
	void Update () {
	}
}
