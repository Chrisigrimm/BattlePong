using UnityEngine;
using Assets.Code.Interfaces;

namespace Assets.Code.States{
	
	public class MenüSate : IStateBase{
		
		private StateManager manager;
		private bool scriptsLoaded = false;
		private GameObject SettingsPC, SettingsMob;

		public MenüSate(StateManager managerRef) // Constructor
		{
			Screen.orientation = ScreenOrientation.Portrait;

			manager = managerRef;

			if (Application.loadedLevelName != "Menue")
					Application.LoadLevel ("Menue");
		}
		public void StateUpdate(){
			if (Application.loadedLevelName == "Menue" && !scriptsLoaded && BattlePongScale.ScaleGame()) {
				scriptsLoaded = true;
				//Set Gaming-States False
				StateManager.SinglePlayer = false;
				StateManager.LocalCoop = false;
				//-Change Settings Menü by device-
				SettingsPC = GameObject.Find("SettingsPanelPC");
				SettingsMob = GameObject.Find("SettingsPanelMob");
				#if UNITY_ANDROID || UNITY_IPHONE
					NGUITools.SetActive(SettingsPC,false);
					GameObject.Find("Settings").GetComponent<TweenSettings>().Menü = SettingsMob.transform;
				#else
					NGUITools.SetActive(SettingsMob,false);
					GameObject.Find("Settings").GetComponent<TweenSettings>().Menü = SettingsPC.transform;
				#endif

				StateManager.UsernameOutput = GameObject.Find("UsernameOutput");
				StateManager.UsernameInput = GameObject.Find("UsernameInput");
				
				if( LevelSerializer.SavedGames["menue"].Count > 0 ){
					LevelSerializer.LoadNow (LevelSerializer.SavedGames["menue"][0].Data, false, false);
				}else{
					StateManager.UsernameOutput.GetComponent<UILabel>().text = "Defalt";
					StateManager.UsernameInput.GetComponent<UIInput>().text = "Defalt";
				}
			}

		}

		public void StateLateUpdate(){
		}
		
		public void ShowIt(){
		}
		
		public void NGUIfeedback(GameObject GmObj, string Type){
			if (Type == "OnClick") {
				if (GmObj.name == "SinglePlayer") {
					#if !UNITY_ANDROID && !UNITY_IPHONE
					SaveSettings();
					#endif
					StateManager.SinglePlayer = true;
					manager.SwitchState (new GameState (manager));
					Slideeffect.getSlided = false;
					TweenSlider.ShowMenue = null;
				}
				if (GmObj.name == "MultiPlayer") {
					#if !UNITY_ANDROID && !UNITY_IPHONE
					SaveSettings();
					#endif
					manager.SwitchState (new GameState (manager));
				}
				if (GmObj.name == "LocalCoop") {
					#if !UNITY_ANDROID && !UNITY_IPHONE
					SaveSettings();
					#endif
					StateManager.MouseControl = false;
					StateManager.LocalCoop = true;
					manager.SwitchState (new GameState (manager));
					Slideeffect.getSlided = false;
					TweenSlider.ShowMenue = null;
				}
				//If pressed Down
				if (GmObj.name == "Settings" && TweenSlider.ShowMenue != "Settings") {
					LevelSerializer.SaveGame("menue");
				}
				if (GmObj.name == "Exit") {
					Application.Quit();
				}
			}

			if (Type == "OnSubmit") {
				if (GmObj.name == "UsernameInput") {
					StateManager.UsernameOutput.GetComponent<UILabel>().text = StateManager.UsernameInput.GetComponent<UIInput>().text;
					LevelSerializer.SaveGame("menue");
				}
			}
		}
		#if !UNITY_ANDROID && !UNITY_IPHONE
		// For other States
		void SaveSettings(){
			StateManager.Player1Up = GameObject.Find("InputUp1").GetComponent<UIInput>().label.text;
			StateManager.Player2Up = GameObject.Find("InputUp2").GetComponent<UIInput>().label.text;
			StateManager.Player1Down = GameObject.Find("InputDown1").GetComponent<UIInput>().label.text;
			StateManager.Player2Down = GameObject.Find("InputDown2").GetComponent<UIInput>().label.text;
			StateManager.MouseControl = GameObject.Find("InputMouseControl").GetComponent<UICheckbox>().mChecked;
		}
		#endif
	}
}