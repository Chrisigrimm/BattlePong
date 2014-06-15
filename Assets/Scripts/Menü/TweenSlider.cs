using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class TweenSlider : MonoBehaviour {

	public Transform Slider;
	public Transform Menue;
	public Vector3 BackPos ,HoverPos ,ClickedPos , MenuePos;
	private Tweener HoverTween, ClickTween , BackTween, MenueTween;
	public GameObject Icon;
	private string OldIcon;
	private Vector3 OldScale;
	private Sequence SlideSeq;
	public static string ShowMenue;
	private int Stage;
	// Use this for initialization
	void Start () {
		ScaleToFormat.getAspectRatio();
		if( !ScaleToFormat.isLandScape() ){
			BackPos = ScaleToFormat.getPos (BackPos, new Vector2 (10, 16));
			HoverPos = ScaleToFormat.getPos (HoverPos, new Vector2 (10, 16));
			ClickedPos = ScaleToFormat.getPos (ClickedPos, new Vector2 (10, 16));
			MenuePos = ScaleToFormat.getPos (MenuePos, new Vector2 (10, 16));
		}

		BackTween = HOTween.To(Slider, 0.5f, new TweenParms().AutoKill(false)
		                        .Prop("position",BackPos, true) // Position tween (set as relative)
		                        .Ease(EaseType.EaseInOutQuad) // Ease
		                        );
		BackTween.PlayForward ();
		Stage = 1;

		HoverTween = HOTween.To(Slider, 0.1f, new TweenParms().AutoKill(false)
		                     .Prop("position",HoverPos, true) // Position tween (set as relative)
		                     .Ease(EaseType.EaseInOutQuad) // Ease
		                     );
		HoverTween.Pause ();

		ClickTween = HOTween.To(Slider, 0.5f, new TweenParms().AutoKill(false)
		                        .Prop("position",ClickedPos, true) // Position tween (set as relative)
		                        .Ease(EaseType.EaseInOutQuad) // Ease
		                        );
		ClickTween.Pause ();

		MenueTween = HOTween.To(Menue, 0.8f, new TweenParms().AutoKill(false)
		                        .Prop("position",MenuePos, true) // Position tween (set as relative)
		                        .Ease(EaseType.EaseInOutQuad) // Ease
		                        );
		MenueTween.Pause ();

	}
	
	// Update is called once per frame
	void Update () {
		if (ShowMenue != null && ShowMenue != name && Stage == 1) {
			BackTween.PlayBackwards();
			Stage = 0;
		}
		if (ShowMenue == null && Stage == 0) {
			BackTween.PlayForward();
			Slideeffect.getSlided = false;
			Stage = 1;
		}
		if (ShowMenue != null && Stage == 2 && ShowMenue != name ){
			MenueTween.PlayBackwards(); 
			ClickTween.PlayBackwards();
			StartCoroutine(ShowSettings());
			GetComponent<AudioSource>().Play();
			Icon.GetComponent<UISprite>().spriteName = OldIcon;
			OldIcon = null;
			Slideeffect.getSlided = false;
			Stage = 0;
		}
	}

	void OnPress(bool isDown){
		//On Android
		if (Application.platform == RuntimePlatform.Android){
			if(!Slideeffect.getSlided){
				if( isDown ){
					HoverTween.PlayForward();
				}else{
					HoverTween.PlayBackwards();
			}
		}else{
				if( Slideeffect.getSlided && Stage == 2 && ShowMenue == name && isDown){
					ClickTween.PlayBackwards();
					MenueTween.PlayBackwards();
					GetComponent<AudioSource>().Play();
					Icon.GetComponent<UISprite>().spriteName = OldIcon;
					OldIcon = null;
					ShowMenue = null;
					Slideeffect.getSlided = false;
					Stage = 1;
				}
				if( Slideeffect.getSlided && ShowMenue == null && !isDown){
					ShowMenue = name;
					ClickTween.PlayForward();
					MenueTween.PlayForward();
					GetComponent<AudioSource>().Play();
					OldIcon = Icon.GetComponent<UISprite>().spriteName;
					Icon.GetComponent<UISprite>().spriteName = "ArrowBack";
					HoverTween.GoTo(0f);
					Stage = 2;
				}
			}
		}
	}

	void OnClick(){
		if (Application.platform == RuntimePlatform.Android) {
		}else{
			//ShowMenue
			if(Slideeffect.getSlided && ShowMenue == name){
				ClickTween.PlayBackwards();
				MenueTween.PlayBackwards();
				GetComponent<AudioSource>().Play();
				Icon.GetComponent<UISprite>().spriteName = OldIcon;
				OldIcon = null;
				ShowMenue = null;
				Slideeffect.getSlided = false;
				Stage = 1;
			}
			if(Slideeffect.getSlided && ShowMenue == null){
				ShowMenue = name;
				ClickTween.PlayForward();
				MenueTween.PlayForward();
				GetComponent<AudioSource>().Play();
				OldIcon = Icon.GetComponent<UISprite>().spriteName;
				Icon.GetComponent<UISprite>().spriteName = "ArrowBack";
				HoverTween.GoTo(0f);
				Stage = 2;
			}
		}
	}

	void OnHover(bool Hover){
		//on PC
		if(BackTween.isPaused){
			if( !Slideeffect.getSlided){
				if(Hover){
					HoverTween.PlayForward();

				}else{
					HoverTween.PlayBackwards();
				}
			}else{
				HoverTween.PlayBackwards ();
			}
		}
	}

	IEnumerator ShowSettings(){
		yield return new WaitForSeconds(0.5f);
		BackTween.PlayBackwards();
	}
}
