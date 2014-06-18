using UnityEngine;
using System.Collections;

public class ScaleToFormat {
	private static Vector2 aspectRatio;

	public static Vector2 getAspectRatio(){
		/*if (Cam.aspect >= 1.7) {aspectRatio = new Vector2(16,9);}//16:9
		else if (Cam.aspect >= 1.6){aspectRatio = new Vector2(16,10);}//16:10
		else if (Cam.aspect >= 1.5){aspectRatio = new Vector2(3,2);}//3:2
		else if(Cam.aspect >= 1.333){aspectRatio = new Vector2(4,3);}//4:3
		else if (Cam.aspect >= 1.25){aspectRatio = new Vector2(5,4);}//5:4
		else if (Cam.aspect >= 0.666){aspectRatio = new Vector2(2,3);}//2:3
		else{aspectRatio = new Vector2(10,16);}//10:16*/
		aspectRatio = AspectRatio.GetAspectRatio(Screen.width, Screen.height);

		return aspectRatio;
	}

	public static Vector2 getAspectRatioSoft(Camera Cam){
		if (Cam.aspect >= 1.7) {aspectRatio = new Vector2(16,9);}//16:9
		else if (Cam.aspect >= 1.6){aspectRatio = new Vector2(16,10);}//16:10
		else if (Cam.aspect >= 1.5){aspectRatio = new Vector2(3,2);}//3:2
		else if(Cam.aspect >= 1.333){aspectRatio = new Vector2(4,3);}//4:3
		else if (Cam.aspect >= 1.25){aspectRatio = new Vector2(5,4);}//5:4
		else if (Cam.aspect >= 0.666){aspectRatio = new Vector2(2,3);}//2:3
		else{aspectRatio = new Vector2(10,16);}//10:16

		return aspectRatio;
	}

	public static bool isLandScape(){
		if (aspectRatio.x > aspectRatio.y) {
			return true;
		}else{
			return false;
		}
	}

	public static void setNewOrthographicSize(Camera Cam,float stdWeidth, float stdFormatY){
		Cam.orthographicSize = (stdWeidth * (aspectRatio.y / stdFormatY) / 2) / 100;
	}

	public static void reScaleToFormat(Vector2 stdFormat, GameObject GmObj){

		GmObj.transform.localScale = new Vector3 (GmObj.transform.localScale.x * (aspectRatio.x / stdFormat.x), GmObj.transform.localScale.y * (aspectRatio.y / stdFormat.y), GmObj.transform.localScale.z);
		GmObj.transform.position = new Vector3 (GmObj.transform.position.x * (aspectRatio.x / stdFormat.x), GmObj.transform.position.y * (aspectRatio.y / stdFormat.y), GmObj.transform.position.z);

	}

	public static void reScaleShaderToFormat(Vector2 stdFormat, GameObject GmObj){
		//ForShaders
		if( GmObj.GetComponent<SpriteRenderer> ().material.HasProperty("_LocalPos1") ){
			GmObj.GetComponent<SpriteRenderer>().material.SetVector("_LocalPos1", new Vector4(GmObj.GetComponent<SpriteRenderer>().material.GetVector("_LocalPos1").x*(aspectRatio.x / stdFormat.x),GmObj.GetComponent<SpriteRenderer>().material.GetVector("_LocalPos1").y*(aspectRatio.y / stdFormat.y),GmObj.GetComponent<SpriteRenderer>().material.GetVector("_LocalPos1").z,GmObj.GetComponent<SpriteRenderer>().material.GetVector("_LocalPos1").w));
		}
		if( GmObj.GetComponent<SpriteRenderer> ().material.HasProperty("_LocalPos2") ){
			GmObj.GetComponent<SpriteRenderer>().material.SetVector("_LocalPos2", new Vector4(GmObj.GetComponent<SpriteRenderer>().material.GetVector("_LocalPos2").x*(aspectRatio.x / stdFormat.x),GmObj.GetComponent<SpriteRenderer>().material.GetVector("_LocalPos2").y*(aspectRatio.y / stdFormat.y),GmObj.GetComponent<SpriteRenderer>().material.GetVector("_LocalPos2").z,GmObj.GetComponent<SpriteRenderer>().material.GetVector("_LocalPos2").w));
		}
	}

	public static Vector2 getVel(Vector2 Velocity, Vector2 stdFormat){
		return new Vector2 (Velocity.x*(aspectRatio.x / stdFormat.x),Velocity.y*(aspectRatio.y / stdFormat.y));
	}

	public static Vector2 getBackVel(Vector2 Velocity, Vector2 stdFormat){
		return new Vector2 (Velocity.x/(aspectRatio.x / stdFormat.x),Velocity.y/(aspectRatio.y / stdFormat.y));
	}

	public static Vector3 getPos(Vector3 Position ,Vector2 stdFormat){
		return new Vector3 (Position.x*(aspectRatio.x / stdFormat.x),Position.y*(aspectRatio.y / stdFormat.y),Position.z);
	}

	public static Vector3 getBackPos(Vector3 Position ,Vector2 stdFormat){
		return new Vector3 (Position.x/(aspectRatio.x / stdFormat.x),Position.y/(aspectRatio.y / stdFormat.y),Position.z);
	}

}

