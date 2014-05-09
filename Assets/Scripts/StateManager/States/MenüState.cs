using UnityEngine;
using Assets.Code.Interfaces;

namespace Assets.Code.States{
	
	public class MenüSate : IStateBase{
		
		private StateManager manager;

		public MenüSate(StateManager managerRef) // Constructor
		{
			Screen.orientation = ScreenOrientation.Portrait;
			manager = managerRef;
			if (Application.loadedLevelName != "Menü") {
				Application.LoadLevel("Menü");
			}
		}
		public void StateUpdate(){
		}

		public void StateLateUpdate(){
		}

		public void ShowIt(){
		}
			
		public void getClick(string ObjectName){
			if (ObjectName == "SinglePlayer") {
				manager.SwitchState (new SinglePlayerState (manager));
			}
			if (ObjectName == "LocalCoop") {
				manager.SwitchState (new LocalCoopState (manager));
			}
			if (ObjectName == "Exit") {
				Application.Quit();
			}
		}
	}
}