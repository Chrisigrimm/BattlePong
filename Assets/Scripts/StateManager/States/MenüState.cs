using UnityEngine;
using Assets.Code.Interfaces;

namespace Assets.Code.States{
	
	public class MenüSate : IStateBase{
		
		private StateManager manager;
		private bool scriptsLoaded = false;
		private GameObject SettingsPC, SettingsMob;
		private GameObject UsernameOutput, UsernameInput;

		public MenüSate(StateManager managerRef) // Constructor
		{
			Screen.orientation = ScreenOrientation.Portrait;

			manager = managerRef;

			if (Application.loadedLevelName != "newMenue")
					Application.LoadLevel ("newMenue");
		}
		public void StateUpdate(){
			if (Application.loadedLevelName == "newMenue" && !scriptsLoaded) {
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

				UsernameOutput = GameObject.Find("UsernameOutput");
				UsernameInput = GameObject.Find("UsernameInput");

				if( LevelSerializer.SavedGames["menue"].Count > 0 ){
					LevelSerializer.LoadNow (LevelSerializer.SavedGames["menue"][0].Data, false, false);
					UsernameOutput.GetComponent<UILabel>().text = StateManager.Username;
					UsernameInput.GetComponent<UIInput>().value = StateManager.Username;
				}else{
					UsernameOutput.GetComponent<UILabel>().text = "Defalt";
					UsernameInput.GetComponent<UIInput>().text = "Defalt";
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
					StateManager.SinglePlayer = true;
					StateManager.difficult = GameObject.Find("PopUp DC").GetComponent<UIPopupList>().value;
					manager.SwitchState (new GameState (manager));
					Slideeffect.getSlided = false;
					TweenSlider.ShowMenue = null;
				}
				if (GmObj.name == "MultiPlayer") {
					manager.SwitchState (new GameState (manager));
				}
				if (GmObj.name == "LocalCoop") {
					StateManager.MouseControl = false;
					StateManager.LocalCoop = true;
					manager.SwitchState (new GameState (manager));
					Slideeffect.getSlided = false;
					TweenSlider.ShowMenue = null;
				}
				//If pressed Down
				if (GmObj.name == "Settings" && TweenSlider.ShowMenue != "Settings") {
					StateManager.Username = UsernameOutput.GetComponent<UILabel>().text;
					#if !UNITY_ANDROID && !UNITY_IPHONE
					StateManager.Player1Up = GameObject.Find("InputUp1").GetComponent<UIInput>().label.text;
					StateManager.Player2Up = GameObject.Find("InputUp2").GetComponent<UIInput>().label.text;
					StateManager.Player1Down = GameObject.Find("InputDown1").GetComponent<UIInput>().label.text;
					StateManager.Player2Down = GameObject.Find("InputDown2").GetComponent<UIInput>().label.text;
					#endif
					LevelSerializer.SaveGame("menue");
				}
				if (GmObj.name == "Exit") {
					Application.Quit();
				}
			}

			if (Type == "OnSubmit") {
				if (GmObj.name == "UsernameInput") {
					UsernameOutput.GetComponent<UILabel>().text = UsernameInput.GetComponent<UIInput>().text;
					StateManager.Username = UsernameOutput.GetComponent<UILabel>().text;
					LevelSerializer.SaveGame("menue");
				}
			}
		}
	}
}