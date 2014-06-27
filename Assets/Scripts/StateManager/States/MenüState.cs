using UnityEngine;
using Assets.Code.Interfaces;

namespace Assets.Code.States{
	
	public class MenüSate : IStateBase{
		
		private StateManager manager;
		private bool scriptsLoaded = false;
		private GameObject SettingsPC, SettingsMob;
		private GameObject UsernameOutput, UsernameInput;
		#if UNITY_ANDROID || UNITY_IPHONE
		private ScreenOrientation saveOri;
		#endif

		public MenüSate(StateManager managerRef) // Constructor
		{
			#if UNITY_ANDROID || UNITY_IPHONE
			Screen.orientation = ScreenOrientation.AutoRotation;
			saveOri = Screen.orientation;
			#endif

			manager = managerRef;

			Application.LoadLevel ("Menue");
		}
		public void StateUpdate(){
			if (Application.loadedLevelName == "Menue" && !scriptsLoaded && Time.timeSinceLevelLoad>0.1f) {
				scriptsLoaded = true;
				//Set Gaming-States False
				StateManager.SinglePlayer = false;
				StateManager.LocalCoop = false;
				//-Change Settings Menü by device-
				SettingsPC = GameObject.Find("Panel-Settings-PC");
				SettingsMob = GameObject.Find("Panel-Settings-Mobile");
				#if UNITY_ANDROID || UNITY_IPHONE
				SettingsPC.SetActive(false);
					GameObject.Find("Settings").GetComponent<TweenSettings>().Menü = SettingsMob.transform;
				#else
					NGUITools.SetActive(SettingsMob,false);
					GameObject.Find("Settings").GetComponent<TweenSettings>().Menü = SettingsPC.transform;
				#endif
				UsernameOutput = GameObject.Find("Output-Username");
				UsernameInput = GameObject.Find("Input-Username");

				if( LevelSerializer.SavedGames["menue"].Count > 0 ){
					LevelSerializer.LoadNow (LevelSerializer.SavedGames["menue"][0].Data, false, false);
					UsernameOutput.GetComponent<UILabel>().text = StateManager.Username;
					UsernameInput.GetComponent<UIInput>().value = StateManager.Username;
					#if !UNITY_ANDROID && !UNITY_IPHONE
					GameObject.Find("Input-Up-1").GetComponent<UIInput>().label.text = StateManager.Player1Up;
					GameObject.Find("Input-Up-2").GetComponent<UIInput>().label.text = StateManager.Player2Up;
					GameObject.Find("Input-Down-1").GetComponent<UIInput>().label.text = StateManager.Player1Down;
					GameObject.Find("Input-Down-2").GetComponent<UIInput>().label.text = StateManager.Player2Down;
					GameObject.Find("Checkbox-Mousecontrol").GetComponent<UIToggle>().value = StateManager.MouseControl;
					#endif
				}else{
					UsernameOutput.GetComponent<UILabel>().text = "Defalt";
					UsernameInput.GetComponent<UIInput>().text = "Defalt";
					#if !UNITY_ANDROID && !UNITY_IPHONE
					GameObject.Find("Input-Up-1").GetComponent<UIInput>().label.text = "W";
					GameObject.Find("Input-Up-2").GetComponent<UIInput>().label.text = "O";
					GameObject.Find("Input-Down-1").GetComponent<UIInput>().label.text = "S";
					GameObject.Find("Input-Down-2").GetComponent<UIInput>().label.text = "L";
					GameObject.Find("Checkbox-Mousecontrol").GetComponent<UIToggle>().value = false;
					StateManager.Player1Up = "W";
					StateManager.Player2Up = "O";
					StateManager.Player1Down = "S";
					StateManager.Player2Down = "L";
					StateManager.MouseControl = false;
					#endif
				}
			}
			#if UNITY_ANDROID || UNITY_IPHONE
			if (saveOri != Screen.orientation) {
				manager.SwitchState(new MenüSate(manager));
			}
			#endif
		}

		public void StateLateUpdate(){
		}
		
		public void ShowIt(){
		}
		
		public void NGUIfeedback(GameObject GmObj, string Type){
			if (Type == "OnClick") {
				if (GmObj.name == "SinglePlayer") {
					StateManager.SinglePlayer = true;
					StateManager.difficult = GameObject.Find("PopUpList-Difficult").GetComponent<UIPopupList>().value;
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
					StateManager.Username = UsernameInput.GetComponent<UIInput>().text;
					#if !UNITY_ANDROID && !UNITY_IPHONE
					StateManager.Player1Up = GameObject.Find("Input-Up-1").GetComponent<UIInput>().label.text;
					StateManager.Player2Up = GameObject.Find("Input-Up-2").GetComponent<UIInput>().label.text;
					StateManager.Player1Down = GameObject.Find("Input-Down-1").GetComponent<UIInput>().label.text;
					StateManager.Player2Down = GameObject.Find("Input-Down-2").GetComponent<UIInput>().label.text;
					StateManager.MouseControl = GameObject.Find("Checkbox-Mousecontrol").GetComponent<UIToggle>().value;
					#endif
					LevelSerializer.SaveGame("menue");
				}
				if (GmObj.name == "Exit") {
					Application.Quit();
				}
			}

			if (Type == "OnSubmit") {
				if (GmObj.name == "Input-Username") {
					UsernameOutput.GetComponent<UILabel>().text = UsernameInput.GetComponent<UIInput>().text;
					StateManager.Username = UsernameOutput.GetComponent<UILabel>().text;
					LevelSerializer.SaveGame("menue");
				}
			}
		}
	}
}