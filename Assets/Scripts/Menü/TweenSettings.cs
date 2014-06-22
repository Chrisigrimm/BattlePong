using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class TweenSettings : MonoBehaviour {

	public Transform Menü;
	public Vector3 MenüPos;
	private Tweener SettingsTween;
	public GameObject Icon;
	private string OldIcon;
	private int State;
	private Transform MenüOld;
	// Use this for initialization
	void Start () {
		SettingsTween = HOTween.To(Menü, 0.5f, new TweenParms().AutoKill(false)
		                       .Prop("position",MenüPos, true) // Position tween (set as relative)
		                       .Ease(EaseType.EaseInOutQuad) // Ease
		                       );
		SettingsTween.Pause ();

		State = 0;
		//Needed because of Mobile Devices
		MenüOld = Menü;
	}
	
	// Update is called once per frame
	void Update () {
		if (MenüOld.name != Menü.name) {
			MenüOld = Menü;
			SettingsTween = HOTween.To(Menü, 0.5f, new TweenParms().AutoKill(false)
			                           .Prop("position",MenüPos, true) // Position tween (set as relative)
			                           .Ease(EaseType.EaseInOutQuad) // Ease
			                           );
			SettingsTween.Pause ();
		}
	}

	void OnClick(){
		if( State == 0 ){
			TweenSlider.ShowMenue = name;
			SettingsTween.PlayForward ();
			OldIcon = Icon.GetComponent<UISprite>().spriteName;
			Icon.GetComponent<UISprite>().spriteName = "ArrowBack";
			Icon.GetComponent<UIButton>().normalSprite = "ArrowBack";
			transform.Rotate(0,0,90);
			Menü.GetComponent<AudioSource>().Play();
			State = 1;
		}else if (State == 1){
			TweenSlider.ShowMenue = null;
			SettingsTween.PlayBackwards ();
			Icon.GetComponent<UISprite>().spriteName = OldIcon;
			Icon.GetComponent<UIButton>().normalSprite = OldIcon;
			transform.Rotate(0,0,-90);
			Menü.GetComponent<AudioSource>().Play();
			State = 0;
		}
	}
}
