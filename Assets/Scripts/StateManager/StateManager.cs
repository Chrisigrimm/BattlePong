using UnityEngine;
using Assets.Code.States;
using Assets.Code.Interfaces;
using System.Collections;
using Serialization;

public class StateManager : MonoBehaviour {

	private IStateBase activeState;
	private static StateManager instanceRef;
	//Modus
	public static bool SinglePlayer, LocalCoop;
	//Gamemodus
	public static string GameModus;
	//Difficult
	public static string difficult;
	//Settings
	[SerializeThis]
	public static string Username;
	public static string Player1Up, Player1Down;
	public static string Player2Up, Player2Down;
	public static bool MouseControl;

	void Awake (){
		if (instanceRef == null) {
			instanceRef = this;
			DontDestroyOnLoad(gameObject);
		} else{
			Destroy(gameObject);
		}
	}

	void Start () {
		activeState = new MenüSate (this);
	}

	void Update () {
		if (activeState != null) {
			activeState.StateUpdate();
		}
	}
	void LateUpdate(){
		if (activeState != null) {
			activeState.StateLateUpdate();
		}
	}

	void OnGUI () {
		if (activeState != null) {
			activeState.ShowIt();
		}
	}

	public void SwitchState( IStateBase newState){
		activeState = newState;
	}

	//NGUI Feedback of Inputs
	public void OnClick(GameObject GmObj){
		activeState.NGUIfeedback (GmObj, "OnClick");
	}

	public void OnSubmit(GameObject GmObj){
		activeState.NGUIfeedback (GmObj, "OnSubmit");
	}


}
