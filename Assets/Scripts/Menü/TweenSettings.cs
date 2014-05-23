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
	// Use this for initialization
	void Start () {
		ScaleToFormat.getAspectRatio();
		if( !ScaleToFormat.isLandScape() ){
			MenüPos = ScaleToFormat.getPos (MenüPos, new Vector2 (10, 16));
		}

		SettingsTween = HOTween.To(Menü, 0.5f, new TweenParms().AutoKill(false)
		                       .Prop("position",MenüPos, true) // Position tween (set as relative)
		                       .Ease(EaseType.EaseInOutQuad) // Ease
		                       );
		SettingsTween.Pause ();

		State = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){
		if( State == 0 && TweenSlider.ShowMenue == null){
			TweenSlider.ShowMenue = name;
			SettingsTween.PlayForward ();
			OldIcon = Icon.GetComponent<UISprite>().spriteName;
			Icon.GetComponent<UISprite>().spriteName = "ArrowBack";
			transform.localRotation = new Quaternion(0,0,-1,1);
			Menü.GetComponent<AudioSource>().Play();
			State = 1;
		}else if (State == 1){
			TweenSlider.ShowMenue = null;
			SettingsTween.PlayBackwards ();
			Icon.GetComponent<UISprite>().spriteName = OldIcon;
			transform.rotation = new Quaternion(0,0,0,0);
			Menü.GetComponent<AudioSource>().Play();
			State = 0;
		}
	}
}
