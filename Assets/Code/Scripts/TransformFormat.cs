using UnityEngine;
using System.Collections;

public class TransformFormat : MonoBehaviour {

	private Object[] Sprites;
	private static Vector2 aspectRatio;

	// Use this for initialization
	void Start () {
		StartCoroutine(Scale());
	}
	IEnumerator Scale()
	{
		yield return new WaitForSeconds(0.1f);
		aspectRatio = AspectRatio.GetAspectRatio (Screen.width, Screen.height);
		
		Camera.main.orthographicSize = (1080 * (aspectRatio.y / 9f) / 2) / 100;
		Sprites = FindObjectsOfType (typeof(GameObject));
		foreach (GameObject Sprit in Sprites) {
			if (Sprit.GetComponent<SpriteRenderer> () && !Sprit.transform.parent) {
				Sprit.transform.localScale = new Vector3 (Sprit.transform.localScale.x * (aspectRatio.x / 16f), Sprit.transform.localScale.y * (aspectRatio.y / 9f), Sprit.transform.localScale.z);
				Sprit.transform.position = new Vector3 (Sprit.transform.position.x * (aspectRatio.x / 16f), Sprit.transform.position.y * (aspectRatio.y / 9f), Sprit.transform.position.z);
			}
		}
	}
	public static Vector2 getTransVel(Vector2 Velocity){
		return new Vector2 (Velocity.x*(aspectRatio.x / 16f),Velocity.y*(aspectRatio.y / 9f));
	}
}
