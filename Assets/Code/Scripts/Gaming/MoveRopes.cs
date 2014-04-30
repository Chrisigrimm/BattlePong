using UnityEngine;
using System.Collections;

public class MoveRopes : MonoBehaviour {

	public GameObject Player;
	public bool UpRope;
	public bool DownRope;
	private Vector3 Startpos;
	private int side;
	// Use this for initialization
	void Start () {
		Startpos = transform.position;
		if(Player.name=="Player01"){
			side=-1;
		}else{
			side=1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(UpRope){
			transform.position =  Startpos + new Vector3(Player.transform.position.y*side,0,0);
		}else if(DownRope){
			transform.position =  Startpos + new Vector3(-Player.transform.position.y*side,0,0);
		}
	}
}
