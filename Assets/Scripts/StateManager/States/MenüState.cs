using UnityEngine;
using Assets.Code.Interfaces;

namespace Assets.Code.States{
	
	public class MenüSate : IStateBase{
		
		private StateManager manager;
		private float initialize;
		private bool scriptsLoaded;

		public MenüSate(StateManager managerRef) // Constructor
		{
			Screen.orientation = ScreenOrientation.Portrait;
			manager = managerRef;
			//New Screen Res... wait
		}
		public void StateUpdate(){
			if (Application.loadedLevelName == "Menü" && !scriptsLoaded) {
				scriptsLoaded = true;
				//-Change Settings Menü-
				//Panels
				GameObject SettingsPC, SettingsMob;
				SettingsPC = GameObject.Find("SettingsPanelPC");
				SettingsMob = GameObject.Find("SettingsPanelMob");
				#if UNITY_ANDROID || UNITY_IPHONE
								NGUITools.SetActive(SettingsPC,false);
								GameObject.Find("Settings").GetComponent<TweenSettings>().Menü = SettingsMob.transform;
				#else
								NGUITools.SetActive(SettingsMob,false);
								GameObject.Find("Settings").GetComponent<TweenSettings>().Menü = SettingsPC.transform;
				#endif
			}
		}

		public void StateLateUpdate(){
		}

		public void ShowIt(){
		}
			
		public void getClick(string ObjectName){
			if (ObjectName == "SinglePlayer") {
				manager.SwitchState (new SinglePlayerState (manager));
				Slideeffect.getSlided = false;
				TweenSlider.ShowMenue = null;
			}
			/*if (ObjectName == "MultiPlayer") {
				manager.SwitchState (new MultiplayerState (manager));
			}*/
			if (ObjectName == "LocalCoop") {
				manager.SwitchState (new LocalCoopState (manager));
				Slideeffect.getSlided = false;
				TweenSlider.ShowMenue = null;
			}
			if (ObjectName == "Exit") {
				Application.Quit();
			}
		}
	
	}
}