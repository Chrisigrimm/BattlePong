using UnityEngine;
using Assets.Code.Interfaces;

namespace Assets.Code.States{
	
	public class MultiplayerState : IStateBase{
		
		private StateManager manager;
		bool toggleESC;
		float savedTimeScale;
		float initialize;
		bool loadScripts = false;
		
		public MultiplayerState(StateManager managerRef) // Constructor
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			manager = managerRef;
			//New Screen Res... wait
			initialize = Time.time + 0.5f;
			
		}
		
		public void StateUpdate(){
			if( initialize < Time.time ){
				initialize = 0f;
				if (Application.loadedLevelName != "GameMulti") {
					PhotonNetwork.LoadLevel("GameMulti");
				}
			}
			
			if (Application.loadedLevelName == "GameMulti" && !loadScripts) {
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

		}
		
		public void ShowIt(){
		}
		
		public void StateLateUpdate(){
		}
		
		public void getClick(string ObjectName){
		}
	}
}