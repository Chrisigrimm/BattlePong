using UnityEngine;
using Assets.Code.States;
using Assets.Code.Interfaces;

public class StateManager : MonoBehaviour {

	private IStateBase activeState;
	private static StateManager instanceRef;

	void Awake (){
		if (instanceRef == null) {
			instanceRef = this;
			DontDestroyOnLoad(gameObject);
		} else{
			DestroyImmediate(gameObject);
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

	public void Restart (){
		Application.LoadLevel(Application.loadedLevelName);
	}

	public void SwitchState( IStateBase newState){
		activeState = newState;
	}
	
	public void getClick(string ObjectName){
		activeState.getClick(ObjectName);
	}
}
