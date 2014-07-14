using UnityEngine;
using System.Collections;

public class MoveRolls : MonoBehaviour {

	public GameObject Player;
	private int side;

	void Start(){
		if(Player.name=="Player01"){
			side=1;
		}else{
			side=-1;
		}
	}

	void Update () {
		Quaternion test = transform.rotation;
		test.eulerAngles = new Vector3 (0, 0, Player.transform.position.y*side);
		transform.rotation = test;
	}
}
