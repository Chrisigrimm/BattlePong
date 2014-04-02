using UnityEngine;
using Assets.Code.Interfaces;

namespace Assets.Code.States{
	
	public class MenüSate : IStateBase{
		
		private StateManager manager;
		
		public MenüSate(StateManager managerRef) // Constructor
		{
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
			if (ObjectName == "Button - Play") {
				manager.SwitchState (new LocalCoopState (manager));
			}
			if (ObjectName == "Button - Exit") {
				Application.Quit();
			}
		}
	}
}