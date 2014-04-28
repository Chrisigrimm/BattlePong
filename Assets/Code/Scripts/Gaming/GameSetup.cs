using UnityEngine;
using System.Collections;

   //////////////
  ////G-Tec/////
 ////Keyjin////
//////////////

public class GameSetup : MonoBehaviour {
	
	public Camera mainCam;
	public BoxCollider2D leftWall, rightWall;
	//public Transform  ButtomWall, TopWall, Player01, Player02;

	public static GameObject pausedPanel;

	// Use this for initialization
	void Start () {
		/*ButtomWall.transform.position = new Vector3 (0f,mainCam.ScreenToWorldPoint ( new Vector3 (0f, Screen.height , 0f)).y - ButtomWall.GetComponent<BoxCollider2D>().size.y/2, 1f);
		TopWall.transform.position = new Vector3 (0f,mainCam.ScreenToWorldPoint ( new Vector3 (0f, 0f, 0f)).y + TopWall.GetComponent<BoxCollider2D>().size.y/2, 1f);
		*/
		/*leftWall.size = new Vector2 (1f, mainCam.ScreenToWorldPoint ( new Vector3 (0f , Screen.height*2f, 0f)).y);
		leftWall.center = new Vector2 (mainCam.ScreenToWorldPoint ( new Vector3 ( 0f , 0f , 0f)).x - 0.5f,0f);
		
		rightWall.size = new Vector2 (1f, mainCam.ScreenToWorldPoint ( new Vector3 (0f , Screen.height * 2f, 0f)).y);
		rightWall.center = new Vector2 (mainCam.ScreenToWorldPoint ( new Vector3 (  Screen.width ,0f, 0f)).x+ 0.5f, 0f);*/
		
		pausedPanel = GameObject.Find("Window - Paused");
		NGUITools.SetActive(pausedPanel,false);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
