using UnityEngine;
using Assets.Code.Interfaces;
using System.Collections;

namespace Assets.Code.States{
	public class GameState : IStateBase{
		
		private StateManager manager;
		private bool toggleESC;
		private float savedTimeScale;
		private bool scriptsLoaded = false;
		private GameObject pausedPanel;
		static int Score01, Score02;
		private static GameObject GScore01, GScore02;

		public GameState(StateManager managerRef) // Constructor
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;

			manager = managerRef;

			if (Application.loadedLevelName != "Game")
					Application.LoadLevel ("Game");
		}

		public void StateUpdate(){
			if (Application.loadedLevelName == "Game" && !scriptsLoaded && Time.timeSinceLevelLoad>1f) {
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
				GScore01 = GameObject.Find("Score1");
				GScore02 = GameObject.Find("Score2");
				ResetScore ();
				GameObject Player01, Player02;
				//SinglePlayer
				if( StateManager.SinglePlayer ){
					//Bot
					Player01 = GameObject.Find("Player01");
					Bot BotScript = Player01.AddComponent<Bot>();
					if( StateManager.difficult == "Easy" ){
						BotScript.ReaktionTime = 1f;
						BotScript.speed = 5F;
					}else if( StateManager.difficult == "Normal" ){
						BotScript.ReaktionTime = 1f;
						BotScript.speed = 7f;
					}else if ( StateManager.difficult == "Hard" ){
						BotScript.ReaktionTime = 0.122f;
						BotScript.speed = 25f;
					}
				}
				//Single-,Local- Player
				if( StateManager.SinglePlayer || StateManager.LocalCoop ){
					//Player2
					Player02 = GameObject.Find("Player02");
					PlayerControls PlyControls = Player02.AddComponent<PlayerControls>();
					#if !UNITY_ANDROID && !UNITY_IPHONE
					PlyControls.moveUp = (KeyCode) System.Enum.Parse(typeof(KeyCode), StateManager.Player1Up);
					PlyControls.moveDown = (KeyCode) System.Enum.Parse(typeof(KeyCode), StateManager.Player1Down);
					#endif
					PlyControls.speed = 20f;

					if( StateManager.LocalCoop ){
						//Player1
						Player01 = GameObject.Find("Player01");
						PlayerControls PlyControls1 = Player01.AddComponent<PlayerControls>();
						#if !UNITY_ANDROID && !UNITY_IPHONE
						PlyControls1.moveUp = (KeyCode) System.Enum.Parse(typeof(KeyCode), StateManager.Player2Up);
						PlyControls1.moveDown = (KeyCode) System.Enum.Parse(typeof(KeyCode), StateManager.Player2Down);
						#endif
						PlyControls1.speed = 20f;
					}
				}
				//PauseMenue
				pausedPanel = GameObject.Find("Window - Paused");
				NGUITools.SetActive(pausedPanel,false);
			}

			if (Input.GetKeyDown ("escape")){
				toggleESC=!toggleESC;
				if( toggleESC == true ){
					PausedGame();
				}else {
					UnPauseGame();
				}
			}
		}

		public void StateLateUpdate(){
		}

		public void ShowIt(){
		}

		public void NGUIfeedback(GameObject GmObj, string Type){
			if (Type == "OnClick"){
				if (GmObj.name == "Restart") {
					ResetScore ();
					GameObject.Find ("Ball").SendMessage ("ResetBall");
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
		public static void Score(string wallName){
			if (wallName == "leftWall") {Score02 += 1;}
			if (wallName == "rightWall") {Score01 += 1;}
			GScore01.GetComponent<UILabel>().text = "" + Score01;
			GScore02.GetComponent<UILabel>().text = "" + Score02;
		}

		public static void ResetScore(){
			Score02 = 0;
			Score01 = 0;
		}

		public static Vector2 getScore(){
			return new Vector2(Score01,Score02);
		}

		void PausedGame() {
			NGUITools.SetActive(pausedPanel,true);
			savedTimeScale = Time.timeScale;
			Time.timeScale = 0;
			AudioListener.pause = true;
		}
		
		void UnPauseGame() {
			NGUITools.SetActive(pausedPanel,false);
			Time.timeScale = savedTimeScale;
			AudioListener.pause = false;
		}
	}
}