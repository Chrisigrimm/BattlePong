using UnityEngine;
using System.Collections;

public class NaturalOrientation{
	#if UNITY_ANDROID  && !UNITY_EDITOR
	public static int ORIENTATION_UNDEFINED = 0x00000000;
	public static int ORIENTATION_PORTRAIT = 0x00000001;
	public static int ORIENTATION_LANDSCAPE = 0x00000002;
	
	public static int ROTATION_0 	= 0x00000000;
	public static int ROTATION_180 	= 0x00000002;
	public static int ROTATION_270 	= 0x00000003;
	public static int ROTATION_90 	= 0x00000001;
	
	public static int PORTRAIT = 0;
	public static int PORTRAIT_UPSIDEDOWN = 1;
	public static int LANDSCAPE = 2;
	public static int LANDSCAPE_LEFT = 3;

	static AndroidJavaObject mConfig;
	
	//adapted from http://stackoverflow.com/questions/4553650/how-to-check-device-natural-default-orientation-on-android-i-e-get-landscape/4555528#4555528
	public static string GetDeviceDefaultOrientation(){
		if (mConfig == null){
			using (AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").
			       GetStatic<AndroidJavaObject>("currentActivity")){
				mConfig = activity.Call<AndroidJavaObject>("getResources").Call<AndroidJavaObject>("getConfiguration");
			}
		}

		int dOrientation = mConfig.Get<int>("orientation");

		if( dOrientation == ORIENTATION_LANDSCAPE ){
			return ("LANDSCAPE");
		}else if(dOrientation == ORIENTATION_PORTRAIT ){
			return ("PORTRAIT");
		}

		return ("None");
	} 
	
	#endif
}
