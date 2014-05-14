using UnityEngine;
using Assets.Code.Interfaces;

namespace Assets.Code.States{
	
	public class SinglePlayerState : IStateBase{
		
		private StateManager manager;
		bool toggleESC;
		float savedTimeScale;
		float initialize;
		bool loadScripts = false;

		public SinglePlayerState(StateManager managerRef) // Constructor
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
				//Bot
				Player01 = GameObject.Find("Player01");
				Bot BotScript = Player01.AddComponent<Bot>();
				BotScript.Difficult = 3;
				BotScript.ReaktionTime = 0.122f;
				BotScript.speed = 20f;
				//Player
				Player02 = GameObject.Find("Player02");
				PlayerControls PlyControls = Player02.AddComponent<PlayerControls>();
				PlyControls.moveUp = KeyCode.UpArrow;
				PlyControls.moveDown = KeyCode.DownArrow;
				PlyControls.speed = 20f;
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

		public void getClick(string ObjectName){
			if (ObjectName == "Restart") {
				UnPauseGame();
				GameManager.ResetScore();
				manager.Restart();
			}
			if( ObjectName == "Menü"){
				UnPauseGame();
				Screen.orientation = ScreenOrientation.Portrait;
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