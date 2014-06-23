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
	public static string ShowMenue = null;
	private int Stage = 0;
	private bool scriptLoaded = false;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		if( Time.timeSinceLevelLoad> 0.5f && !scriptLoaded){
			scriptLoaded = true;
			ShowMenue = null;

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
					Backward();
				}
				if( Slideeffect.getSlided && ShowMenue == null && !isDown){
					Forward();
				}
			}
		}
	}

	void OnClick(){
		if (Application.platform == RuntimePlatform.Android) {
		}else{
			//ShowMenue
			if(Slideeffect.getSlided && ShowMenue == name){
				Backward();
			}
			if(Slideeffect.getSlided && ShowMenue == null){
				Forward();
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

	void Forward(){
		MenueTween.PlayForward();
		ClickTween.PlayForward();
		ShowMenue = name;
		GetComponent<AudioSource>().Play();
		OldIcon = Icon.GetComponent<UISprite>().spriteName;
		Icon.GetComponent<UISprite>().spriteName = "ArrowBack";
		Slider.GetComponent<UIButton>().normalSprite = "ArrowBack";
		Icon.transform.localScale = Icon.transform.localScale/1.2f;
		HoverTween.GoTo(0f);
		Stage = 2;
	}
	void Backward(){
		ClickTween.PlayBackwards();
		MenueTween.PlayBackwards();
		GetComponent<AudioSource>().Play();
		Icon.GetComponent<UISprite>().spriteName = OldIcon;
		Slider.GetComponent<UIButton>().normalSprite = OldIcon;
		Icon.transform.localScale = Icon.transform.localScale*1.2f;
		OldIcon = null;
		ShowMenue = null;
		Slideeffect.getSlided = false;
		Stage = 1;
	}

	IEnumerator ShowSettings(){
		yield return new WaitForSeconds(0.5f);
		BackTween.PlayBackwards();
	}
}
