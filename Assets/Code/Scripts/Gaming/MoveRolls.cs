using UnityEngine;
using System.Collections;

public class MoveRolls : MonoBehaviour {

	public GameObject Player;
	private int side;

	void Start(){
		if(Player.name=="Player01"){
			side=-1;
		}else{
			side=1;
		}
	}

	void Update () {
		transform.rotation = new Quaternion(0f,0f,(Player.transform.position.normalized.y*-4)*side,1f);
	}
}
