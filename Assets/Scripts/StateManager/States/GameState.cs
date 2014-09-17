using UnityEngine;
using Assets.Code.Interfaces;
using System.Collections;

namespace Assets.Code.States{
	public class GameState : IStateBase{
		
		private StateManager manager;
		private bool toggleESC = false, bCountDown;
		private float savedTimeScale, saveTimer, timer, saveTime;
		private bool scriptsLoaded = false;
		private GameObject PausePanel, CounterPanel, FinishPanel;
		static int Score1, Score2;
		private static GameObject GScore1, GScore2, Name1, Name2, LCountDown;
		private GameObject Ball;
		private string CDAktion;
		private int maxScore;

		public GameState(StateManager managerRef){	
			#if UNITY_ANDROID || UNITY_IPHONE
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			#endif

			manager = managerRef;

			if (Application.loadedLevelName != "Game")
				Application.LoadLevel ("Game");
		}

		public void StateUpdate(){
			//Load Controls... Name etc... on Start and everything loaded
			if (Application.loadedLevelName == "Game" && !scriptsLoaded && Time.timeSinceLevelLoad>0.1f) {
				scriptsLoaded = true;
				//Scale
				float targetaspect = 16.0f / 9.0f;
				float windowaspect = (float)Screen.width / (float)Screen.height;
				float scaleheight = windowaspect / targetaspect;
				if (scaleheight < 1.0f) {  
					Rect rect = Camera.main.rect;
					
					rect.width = 1.0f;
					rect.height = scaleheight;
					rect.x = 0;
					rect.y = (1.0f - scaleheight) / 2.0f;
					
					Camera.main.rect = rect;
				} else { // add pillarbox
					float scalewidth = 1.0f / scaleheight;
					
					Rect rect = Camera.main.rect;
					
					rect.width = scalewidth;
					rect.height = 1.0f;
					rect.x = (1.0f - scalewidth) / 2.0f;
					rect.y = 0;
					
					Camera.main.rect = rect;
				}
				//-- Get World Object for interaction
				//Score
				GScore1 = GameObject.Find("Score1");
				GScore2 = GameObject.Find("Score2");
				ResetScore ();
				//Names
				Name1 = GameObject.Find("Name1");
				Name2 = GameObject.Find("Name2");

				GameObject Player01, Player02;
				Player01 = GameObject.Find("Player01");
				Player02 = GameObject.Find("Player02");

				//Ball
				Ball = GameObject.Find("Ball");
				//Countdown-Label
				LCountDown = GameObject.Find("LCountDown");
				//Panels
				CounterPanel = GameObject.Find("CounterPanel");
				NGUITools.SetActive(CounterPanel,false);
				PausePanel = GameObject.Find("PausePanel");
				NGUITools.SetActive(PausePanel,false);
				FinishPanel = GameObject.Find("FinishPanel");
				NGUITools.SetActive(FinishPanel,false);
				//--GameModus
				maxScore = 10;
				if( StateManager.GameModus == "Standard" ){
					maxScore = 10;
				}
				if( StateManager.GameModus == "Survival" ){
					maxScore = 1;
				}

				//--PlayModus
				//SinglePlayer
				if( StateManager.SinglePlayer ){
					//Bot
					Name1.GetComponent<UILabel>().text = "Bot - Siegfried";
					Bot BotScript = Player01.AddComponent<Bot>();
					if( StateManager.difficult == "Easy" ){
						BotScript.ReaktionTime = 1f;
						BotScript.speed = 2F;
					}else if( StateManager.difficult == "Normal" ){
						BotScript.ReaktionTime = 0.65f;
						BotScript.speed = 3F;
					}else if ( StateManager.difficult == "Hard" ){
						BotScript.ReaktionTime = 0.5f;
						BotScript.speed = 4F;
					}
				}
				//Single-,Local- Player
				if( StateManager.SinglePlayer || StateManager.LocalCoop ){
					//Player2
					Name2.GetComponent<UILabel>().text = StateManager.Username;
					PlayerControls PlyControls2 = Player02.AddComponent<PlayerControls>();
					#if !UNITY_ANDROID && !UNITY_IPHONE
					PlyControls2.moveUp = (KeyCode) System.Enum.Parse(typeof(KeyCode), StateManager.Player1Up);
					PlyControls2.moveDown = (KeyCode) System.Enum.Parse(typeof(KeyCode), StateManager.Player1Down);
					if(StateManager.MouseControl){
						PlyControls2.speed = 20f;
					}else{
						PlyControls2.speed = 2000f;
					}
					#else
					PlyControls2.speed = 20f;
					#endif
				
					if( StateManager.LocalCoop ){
						//Player1
						Name1.GetComponent<UILabel>().text = "Friend";
						PlayerControls PlyControls1 = Player01.AddComponent<PlayerControls>();
						#if !UNITY_ANDROID && !UNITY_IPHONE
						PlyControls1.moveUp = (KeyCode) System.Enum.Parse(typeof(KeyCode), StateManager.Player2Up);
						PlyControls1.moveDown = (KeyCode) System.Enum.Parse(typeof(KeyCode), StateManager.Player2Down);
						if(StateManager.MouseControl){
							PlyControls1.speed = 20f;
						}else{
							PlyControls1.speed = 2000f;
						}
						#else
						PlyControls1.speed = 20f;
						#endif
					}
				}

				//Start Countdown
				CountDown(3,"Reset");
			}
			//Wenn script geladen und Map geladen
			if (scriptsLoaded && !GameFinished()) {
				if( Ball.transform.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,0)).x ||
				   (Ball.transform.position.x > 0 && (Ball.transform.position.y > -Camera.main.ScreenToWorldPoint(new Vector3(Screen.height,0,0)).y ||
				    Ball.transform.position.y < Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).y))){
					Score("Right");
				}
				if( Ball.transform.position.x < Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).x ||
				   (Ball.transform.position.x < 0 && (Ball.transform.position.y > -Camera.main.ScreenToWorldPoint(new Vector3(Screen.height,0,0)).y ||
				    Ball.transform.position.y < Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).y))){
					Score("Left");
				}

				if (bCountDown) {
					timer = saveTimer - RealTime.time;
					if(Mathf.Round(timer) < saveTime){
						CounterPanel.GetComponent<TweenScale>().ResetToBeginning();
						LCountDown.GetComponent<TweenAlpha>().ResetToBeginning();
						CounterPanel.GetComponent<TweenScale>().Play();
						LCountDown.GetComponent<TweenAlpha>().Play ();
						saveTime = Mathf.Round(timer);
					}
					LCountDown.GetComponent<UILabel>().text = Mathf.RoundToInt(timer).ToString();
					if(timer < -0.5){
						bCountDown = false;
						NGUITools.SetActive(CounterPanel,false);
						timer = 0;
						if(CDAktion == "Reset"){
							Ball.SendMessage("GoBall");
						}else if(CDAktion == "Pause"){
							Time.timeScale = 1;
						}
						CDAktion = null;
					}
				}
			}

			if (Input.GetKeyDown ("escape")){
				toggleESC = !toggleESC;
				if( toggleESC == true ){
					PauseGame();
				}else {
					UnPauseGame();
				}
			}
		}

		public void StateLateUpdate(){
		}

		public void ShowIt(){
		}

		// Feedback from NGUI send by Statemanager
		public void NGUIfeedback(GameObject GmObj, string Type){
			if (Type == "OnClick"){
				if (GmObj.name == "Button-Continue") {
					toggleESC = !toggleESC;
					UnPauseGame ();
				}

				if (GmObj.name == "Button-Restart") {
					NGUITools.SetActive(FinishPanel,false);
					toggleESC = !toggleESC;
					ResetScore ();
					Ball.SendMessage ("ResetBall");
					if( StateManager.SinglePlayer ){
						//Bot
						GameObject.Find ("Player01").SendMessage ("ResetPath");
					}
					if( StateManager.LocalCoop || StateManager.SinglePlayer ){
						//Player
						GameObject.Find ("Player02").SendMessage ("ResetPlayer");
						if( StateManager.LocalCoop ){
							GameObject.Find ("Player01").SendMessage ("ResetPlayer");
						}
					}
					CDAktion = "Reset";
					UnPauseGame ();
				}

				if (GmObj.name == "Button-Menue") {
					UnPauseGame ();
					Time.timeScale = 1;
					Screen.orientation = ScreenOrientation.Portrait;
					manager.SwitchState (new MenüSate (manager));
				}
			}
		}

		// Set Score
		void Score(string side){
			if (side == "Left") {Score2 += 1;}
			if (side == "Right") {Score1 += 1;}
			GScore1.GetComponent<UILabel>().text = "" + Score1;
			GScore2.GetComponent<UILabel>().text = "" + Score2;
			//Check if game finished
			if(!GameFinished()){
				Ball.SendMessage ("ResetBall");
				CountDown(3,"Reset");
			}else{
				Time.timeScale = 0;
				bCountDown = false;
				NGUITools.SetActive(CounterPanel,false);
				NGUITools.SetActive(FinishPanel,true);
				if( StateManager.SinglePlayer ){
					if( Score2 == maxScore ){
						FinishPanel.transform.FindChild("Label").GetComponent<UILabel>().text = "You Won!";
					}else{
						FinishPanel.transform.FindChild("Label").GetComponent<UILabel>().text = "You Lost!";
					}
				}
				if( StateManager.LocalCoop ){
					if( Score2 == maxScore ){
						FinishPanel.transform.FindChild("Label").GetComponent<UILabel>().text = StateManager.Username+" Won!";
					}else{
						FinishPanel.transform.FindChild("Label").GetComponent<UILabel>().text = "Your Friend Won!";
					}
				}
				FinishPanel.transform.FindChild("Label").GetComponent<TypewriterEffect>().enabled = true;
			}
		}

		// Finsih
		bool GameFinished(){
			if(Score1 >= maxScore || Score2 >= maxScore){
				return true;
			}
			return false;
		}

		public static void ResetScore(){
			Score2 = 0;
			Score1 = 0;
			GScore1.GetComponent<UILabel>().text = "" + Score1;
			GScore2.GetComponent<UILabel>().text = "" + Score2;
		}

		public static Vector2 getScore(){
			return new Vector2(Score1,Score2); 
		}

		void CountDown(int CDTime, string mAktion){
			bCountDown = true;
			NGUITools.SetActive(CounterPanel,true);
			saveTimer = RealTime.time + CDTime + 0.4f;
			saveTime =  Mathf.Round((saveTimer - RealTime.time));
			CDAktion = mAktion;
		}

		void PauseGame() {
			NGUITools.SetActive(PausePanel,true);
			Time.timeScale = 0;
			bCountDown = false;
			NGUITools.SetActive(CounterPanel,false);
		}
		
		void UnPauseGame() {
			NGUITools.SetActive(PausePanel,false);
			if(CDAktion == "Reset"){
				Time.timeScale = 1;
				CountDown(3,"Reset");
			}else{
				CountDown(3,"Pause");
			}
		}
	}
}