using UnityEngine;
using Assets.Code.Interfaces;


namespace Assets.Code.States{
	public class GameState : IStateBase{
		
		private StateManager manager;
		private bool toggleESC;
		private float savedTimeScale;
		private bool scriptsLoaded = false;
		private GameObject pausedPanel;

		public GameState(StateManager managerRef) // Constructor
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;

			manager = managerRef;

			if (Application.loadedLevelName != "Game")
					Application.LoadLevel ("Game");
		}

		public void StateUpdate(){
			if (Application.loadedLevelName == "Game" && !scriptsLoaded && BattlePongScale.ScaleGame()) {
				scriptsLoaded = true;
				GameObject Player01, Player02;
				//SinglePlayer
				if( StateManager.SinglePlayer ){
					//Bot
					Player01 = GameObject.Find("Player01");
					Bot BotScript = Player01.AddComponent<Bot>();
					BotScript.Difficult = 3f;
					BotScript.ReaktionTime = 0.122f;
					BotScript.speed = 20f;
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
						GameManager.ResetScore ();
						GameObject.Find ("Ball").SendMessage ("ResetBall");
						//Bot
						GameObject.Find ("Player01").SendMessage ("ResetPath");
						//Player
						GameObject.Find ("Player02").SendMessage ("ResetPlayer");
						UnPauseGame ();
				}
				if (GmObj.name == "Menü") {
						UnPauseGame ();
						Screen.orientation = ScreenOrientation.Portrait;
						manager.SwitchState (new MenüSate (manager));
				}
			}
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