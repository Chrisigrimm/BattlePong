﻿using UnityEngine;
using Assets.Code.Interfaces;

namespace Assets.Code.States{
	
	public class SinglePlayerState : IStateBase{
		
		private StateManager manager;
		bool toggleESC;
		float savedTimeScale;
		float initialize;

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
				if (Application.loadedLevelName != "SinglePlayer") {
					Application.LoadLevel("SinglePlayer");
				}
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
			if (ObjectName == "Button - Restart") {
				UnPauseGame();
				GameManager.ResetScore();
				manager.Restart();
			}
			if( ObjectName == "Button - Menu"){
				UnPauseGame();
				Screen.orientation = ScreenOrientation.Portrait;
				manager.SwitchState (new MenüSate (manager));
			}
			if (ObjectName == "Button - Exit") {
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