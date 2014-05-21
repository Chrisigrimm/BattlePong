using UnityEngine;
using Assets.Code.Interfaces;

namespace Assets.Code.States{
	
	public class MenüSate : IStateBase{
		
		private StateManager manager;
		float initialize;

		public MenüSate(StateManager managerRef) // Constructor
		{
			//PhotonNetwork.ConnectUsingSettings("1");
			Screen.orientation = ScreenOrientation.Portrait;
			manager = managerRef;
			//New Screen Res... wait
			initialize = Time.time + 0.5f;
		}
		public void StateUpdate(){
			if( initialize < Time.time ){
				initialize = 0f;
				if (Application.loadedLevelName != "Menü") {
					Application.LoadLevel("Menü");
				}
			}
		}

		public void StateLateUpdate(){
		}

		public void ShowIt(){
		}
			
		public void getClick(string ObjectName){
			if (ObjectName == "SinglePlayer") {
				manager.SwitchState (new SinglePlayerState (manager));
			}
			/*if (ObjectName == "MultiPlayer") {
				manager.SwitchState (new MultiplayerState (manager));
			}*/
			if (ObjectName == "LocalCoop") {
				manager.SwitchState (new LocalCoopState (manager));
			}
			if (ObjectName == "Exit") {
				Application.Quit();
			}
		}
	
	}
}