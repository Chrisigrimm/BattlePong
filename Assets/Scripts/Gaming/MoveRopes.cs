using UnityEngine;
using System.Collections;

public class MoveRopes : MonoBehaviour {

	public GameObject Player;
	public bool UpRope;
	public bool DownRope;
	public GameObject Anker;
	private Vector3 Startpos;
	private int side;
	// Use this for initializationss
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
			transform.position = new Vector3(Anker.transform.position.x+Player.transform.position.y*side,Anker.transform.position.y,Anker.transform.position.z);
		}else if(DownRope){
			transform.position = new Vector3(Anker.transform.position.x-Player.transform.position.y*side,Anker.transform.position.y,Anker.transform.position.z);
		}
	}
}
