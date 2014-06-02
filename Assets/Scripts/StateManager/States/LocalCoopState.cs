using UnityEngine;
using Assets.Code.Interfaces;

namespace Assets.Code.States{
	
	public class LocalCoopState : IStateBase{
		
		private StateManager manager;
		bool toggleESC;
		float savedTimeScale;
		float initialize;
		bool loadScripts = false;

		public LocalCoopState(StateManager managerRef) // Constructor
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			manager = managerRef;
			//New Screen Res... wait
			initialize = Time.time + 0.5f;

		}

		public void StateUpdate(){
			if( initialize < Time.time ){
				initialize = 0f;

				if (Application.loadedLevelName != "Game") {
					Application.LoadLevel("Game");
				}
			}

			if (Application.loadedLevelName == "Game" && !loadScripts) {
				loadScripts = true;
				GameObject Player01, Player02;
				//Player1
				Player01 = GameObject.Find("Player01");
				PlayerControls PlyControls1 = Player01.AddComponent<PlayerControls>();
				PlyControls1.moveUp = KeyCode.W;
				PlyControls1.moveDown = KeyCode.S;
				PlyControls1.speed = 20f;
				//Player2
				Player02 = GameObject.Find("Player02");
				PlayerControls PlyControls2 = Player02.AddComponent<PlayerControls>();
				PlyControls2.moveUp = KeyCode.UpArrow;
				PlyControls2.moveDown = KeyCode.DownArrow;
				PlyControls2.speed = 20f;
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

		public void ShowIt(){
		}

		public void StateLateUpdate(){
		}

		public void getClick(string ObjectName){
			if (ObjectName == "Restart") {
				GameManager.ResetScore();
				GameObject.Find("Ball").SendMessage ("ResetBall");
				//Player
				GameObject.Find("Player01").SendMessage ("ResetPlayer");
				GameObject.Find("Player02").SendMessage ("ResetPlayer");
				UnPauseGame();
			}
			if( ObjectName == "Menü"){
				UnPauseGame();
				manager.SwitchState (new MenüSate (manager));
			}
			if (ObjectName == "Exit") {
				UnPauseGame();
				Application.Quit();
			}
		}

		void PausedGame() {
			NGUITools.SetActive(GameManager.pausedPanel,true);
			savedTimeScale = Time.timeScale;
			Time.timeScale = 0;
			AudioListener.pause = true;
		}
		
		void UnPauseGame() {
			NGUITools.SetActive(GameManager.pausedPanel,false);
			Time.timeScale = savedTimeScale;
			AudioListener.pause = false;
		}
	}
}