using UnityEngine;
using System.Collections;

public class TransformFormat : MonoBehaviour {

	private Object[] Sprites;
	private static Vector2 aspectRatio;

	// Use this for initialization
	void Start () {
		aspectRatio = AspectRatio.GetAspectRatio (Screen.width, Screen.height);
	
		Camera.main.orthographicSize = (1080 * (aspectRatio.y / 9f) / 2) / 100;
		Sprites = FindObjectsOfType (typeof(GameObject));

		foreach (GameObject Sprit in Sprites) {
			SpriteRenderer SpRe = Sprit.GetComponent<SpriteRenderer> ();
			if (SpRe) {
				if(!Sprit.transform.parent){
					Sprit.transform.localScale = new Vector3 (Sprit.transform.localScale.x * (aspectRatio.x / 16f), Sprit.transform.localScale.y * (aspectRatio.y / 9f), Sprit.transform.localScale.z);
					Sprit.transform.position = new Vector3 (Sprit.transform.position.x * (aspectRatio.x / 16f), Sprit.transform.position.y * (aspectRatio.y / 9f), Sprit.transform.position.z);
				}
				if( Sprit.GetComponent<SpriteRenderer> ().material.HasProperty("_LocalPos1") ){
					SpRe.material.SetVector("_LocalPos1", new Vector4(SpRe.material.GetVector("_LocalPos1").x*(aspectRatio.x / 16f),SpRe.material.GetVector("_LocalPos1").y*(aspectRatio.y / 9f),SpRe.material.GetVector("_LocalPos1").z,SpRe.material.GetVector("_LocalPos1").w));
				}
				if( Sprit.GetComponent<SpriteRenderer> ().material.HasProperty("_LocalPos2") ){
					SpRe.material.SetVector("_LocalPos2", new Vector4(SpRe.material.GetVector("_LocalPos2").x*(aspectRatio.x / 16f),SpRe.material.GetVector("_LocalPos2").y*(aspectRatio.y / 9f),SpRe.material.GetVector("_LocalPos2").z,SpRe.material.GetVector("_LocalPos2").w));
				}
			}
		}
	}

	public static Vector2 getTransVel(Vector2 Velocity){
		aspectRatio = AspectRatio.GetAspectRatio (Screen.width, Screen.height);
		return new Vector2 (Velocity.x*(aspectRatio.x / 16f),Velocity.y*(aspectRatio.y / 9f));
	}
}

