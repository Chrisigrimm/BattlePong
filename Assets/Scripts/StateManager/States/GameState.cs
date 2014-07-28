using UnityEngine;
using Assets.Code.Interfaces;
using System.Collections;

namespace Assets.Code.States{
	public class GameState : IStateBase{
		
		private StateManager manager;
		private bool toggleESC, bCountDown = true;
		private float savedTimeScale, timer, saveTimer;
		private bool scriptsLoaded = false;
		private GameObject PausePanel, CounterPanel;
		static int Score1, Score2;
		private static GameObject GScore1, GScore2, Name1, Name2, LCountDown;
		private GameObject Ball;

		public GameState(StateManager managerRef){	
			#if UNITY_ANDROID || UNITY_IPHONE
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			#endif

			manager = managerRef;

			if (Application.loadedLevelName != "NewGame")
				Application.LoadLevel ("NewGame");
		}

		public void StateUpdate(){
			//Load Controls... Name etc... on Start and everything loaded
			if (Application.loadedLevelName == "NewGame" && !scriptsLoaded && Time.timeSinceLevelLoad>0.1f) {
				scriptsLoaded = true;
				//Scale
				float targetaspect = 16.0f / 9.0f;
				float windowaspect = (float)Screen.width / (float)Screen.height;
				float scaleheight = windowaspect / targetaspect;
				if (scaleheight < 1.0f) {  
					Rect rect = Camera.main.rect;
					
					rect.width = 1.0f;
					rect.height = scaleheight;
					rect.x = 0;
					rect.y = (1.0f - scaleheight) / 2.0f;
					
					Camera.main.rect = rect;
				} else { // add pillarbox
					float scalewidth = 1.0f / scaleheight;
					
					Rect rect = Camera.main.rect;
					
					rect.width = scalewidth;
					rect.height = 1.0f;
					rect.x = (1.0f - scalewidth) / 2.0f;
					rect.y = 0;
					
					Camera.main.rect = rect;
				}
				//Score
				GScore1 = GameObject.Find("Score1");
				GScore2 = GameObject.Find("Score2");
				ResetScore ();
				//Names
				Name1 = GameObject.Find("Name1");
				Name2 = GameObject.Find("Name2");

				GameObject Player01, Player02;
				Player01 = GameObject.Find("Player01");
				Player02 = GameObject.Find("Player02");
				//SinglePlayer

				if( StateManager.SinglePlayer ){
					//Bot
					Name1.GetComponent<UILabel>().text = "Bot - Siegfried";
					Bot BotScript = Player01.AddComponent<Bot>();
					if( StateManager.difficult == "Easy" ){
						BotScript.ReaktionTime = 0.9f;
						BotScript.speed = 2F;
					}else if( StateManager.difficult == "Normal" ){
						BotScript.ReaktionTime = 0.6f;
						BotScript.speed = 3F;
					}else if ( StateManager.difficult == "Hard" ){
						BotScript.ReaktionTime = 0.4f;
						BotScript.speed = 4F;
					}
				}
				//Single-,Local- Player
				if( StateManager.SinglePlayer || StateManager.LocalCoop ){
					//Player2
					Name2.GetComponent<UILabel>().text = StateManager.Username;
					PlayerControls PlyControls = Player02.AddComponent<PlayerControls>();
					#if !UNITY_ANDROID && !UNITY_IPHONE
					PlyControls.moveUp = (KeyCode) System.Enum.Parse(typeof(KeyCode), StateManager.Player1Up);
					PlyControls.moveDown = (KeyCode) System.Enum.Parse(typeof(KeyCode), StateManager.Player1Down);
					#endif
					PlyControls.speed = 2000f;
				
					if( StateManager.LocalCoop ){
						//Player1
						Name1.GetComponent<UILabel>().text = "Friend";
						PlayerControls PlyControls1 = Player01.AddComponent<PlayerControls>();
						#if !UNITY_ANDROID && !UNITY_IPHONE
						PlyControls1.moveUp = (KeyCode) System.Enum.Parse(typeof(KeyCode), StateManager.Player2Up);
						PlyControls1.moveDown = (KeyCode) System.Enum.Parse(typeof(KeyCode), StateManager.Player2Down);
						#endif
						PlyControls1.speed = 2000f;
					}
				}
				//Ball
				Ball = GameObject.Find("Ball");
				//PauseMenue
				PausePanel = GameObject.Find("Window - Paused");
				NGUITools.SetActive(PausePanel,false);
				//Countdown-Label
				LCountDown = GameObject.Find("LCountDown");
				//set Countdown to 3
				saveTimer = 3;
				timer = saveTimer;
				CounterPanel = GameObject.Find("CounterPanel");
				NGUITools.SetActive(CounterPanel,true);
			}
			//Wenn script geladen und Map geladen
			if (scriptsLoaded) {
				if( Ball.transform.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,0)).x ||
				   (Ball.transform.position.x > 0 && (Ball.transform.position.y > -Camera.main.ScreenToWorldPoint(new Vector3(Screen.height,0,0)).y ||
				    Ball.transform.position.y < Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).y))){
					Ball.SendMessage ("ResetBall");
					Score("Right");
					bCountDown = true;
					NGUITools.SetActive(CounterPanel,true);
				}
				if( Ball.transform.position.x < Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).x ||
				   (Ball.transform.position.x < 0 && (Ball.transform.position.y > -Camera.main.ScreenToWorldPoint(new Vector3(Screen.height,0,0)).y ||
				    Ball.transform.position.y < Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).y))){
					Ball.SendMessage ("ResetBall");
					Score("Left");
					bCountDown = true;
					NGUITools.SetActive(CounterPanel,true);
				}

				if (bCountDown) {
					CountDown();
					LCountDown.GetComponent<UILabel>().text = Mathf.Round(timer).ToString();
				}
			}

			if (Input.GetKeyDown ("escape")){
				toggleESC=!toggleESC;
				if( toggleESC == true ){
					PauseGame();
				}else {
					UnPauseGame();
				}
			}
		}

		public void StateLateUpdate(){
		}

		public void ShowIt(){
		}

		// Feedback from NGUI send by Statemanager
		public void NGUIfeedback(GameObject GmObj, string Type){
			if (Type == "OnClick"){
				if (GmObj.name == "Restart") {
					ResetScore ();
					Ball.SendMessage ("ResetBall");
					if( StateManager.SinglePlayer ){
						//Bot
						GameObject.Find ("Player01").SendMessage ("ResetPath");
					}
					if( StateManager.LocalCoop || StateManager.SinglePlayer ){
						//Player
						GameObject.Find ("Player02").SendMessage ("ResetPlayer");
						if( StateManager.LocalCoop ){
							GameObject.Find ("Player01").SendMessage ("ResetPlayer");
						}
					}
					UnPauseGame ();
				}
				if (GmObj.name == "Menü") {
					UnPauseGame ();
					Screen.orientation = ScreenOrientation.Portrait;
					manager.SwitchState (new MenüSate (manager));
				}
			}
		}

		// Set Score
		public static void Score(string side){
			if (side == "Left") {Score2 += 1;}
			if (side == "Right") {Score1 += 1;}
			GScore1.GetComponent<UILabel>().text = "" + Score1;
			GScore2.GetComponent<UILabel>().text = "" + Score2;
		}

		public static void ResetScore(){
			Score2 = 0;
			Score1 = 0;
			GScore1.GetComponent<UILabel>().text = "" + Score1;
			GScore2.GetComponent<UILabel>().text = "" + Score2;
		}

		public static Vector2 getScore(){
			return new Vector2(Score1,Score2);
		}

		void CountDown(){
			if(timer <= 0){
				bCountDown = false;
				timer = saveTimer;
				Ball.SendMessage("GoBall");
				NGUITools.SetActive(CounterPanel,false);
			}else{
				if( timer == 1 ){
					CounterPanel.GetComponent<TweenScale>().Play();
				}
				else if( timer == 2 ){
					//CounterPanel.GetComponent<TweenScale>().ResetToBeginning();
					CounterPanel.GetComponent<TweenScale>().Play();
				}
				else if( timer == 3 ){
					//CounterPanel.GetComponent<TweenScale>().ResetToBeginning();
					CounterPanel.GetComponent<TweenScale>().Play();
				}
	

				timer -= Time.deltaTime;
			}
		}


		void PauseGame() {
			NGUITools.SetActive(PausePanel,true);
			savedTimeScale = Time.timeScale;
			Time.timeScale = 0;
			AudioListener.pause = true;
		}
		
		void UnPauseGame() {
			NGUITools.SetActive(PausePanel,false);
			Time.timeScale = savedTimeScale;
			AudioListener.pause = false;
		}
	}
}