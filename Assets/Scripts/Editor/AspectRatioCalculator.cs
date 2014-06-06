using UnityEngine;
using UnityEditor;

public class AspectRatioCalculator : EditorWindow{
	Vector2 xy=new Vector2(800,600);
	string result="Aspect Ratio = 4:3 (800x600)";
	[MenuItem("Window/Aspect Ratio Calculator")]
	static void Init(){
		AspectRatioCalculator window = (AspectRatioCalculator)EditorWindow.GetWindow(typeof(AspectRatioCalculator));
	}
	
	void OnGUI(){
		xy = EditorGUI.Vector2Field(new Rect(3, 3, Screen.width - 6,10), "Resolution", xy);
		xy = new Vector2(xy.x < 1 ? 1 : (int)xy.x, xy.y < 1 ? 1 : (int)xy.y);
		if(GUI.Button(new Rect(3, 50, Screen.width - 6, 40), "Calculate Aspect Ratio" + "\n" + result)){
			Vector2 aspectRatio = AspectRatio.GetAspectRatio((int)xy.x, (int)xy.y);
			result = "Aspect Ratio = " + aspectRatio.x + ":" + aspectRatio.y + " (" + xy.x + "x" + xy.y + ")";
		}
	}
}