using UnityEngine;
using System.Collections;

public class PhotonManager : Photon.MonoBehaviour {

	private static PhotonView ScenePhotonView;

	//Photon Start
	void Awake(){
	}

	// Use this for initialization
	void Start () {
		ScenePhotonView = this.GetComponent<PhotonView>();
	}
	
	// Update is called once per frame
	void Update () {
		if (PhotonNetwork.room.playerCount == PhotonNetwork.room.playerCount) {

		}
		if (PhotonNetwork.isMasterClient) {
			GameObject.Find("Player01").GetComponent<PlayerControls>().enabled = true;
			GameObject.Find("Player02").GetComponent<PlayerControls>().enabled = false;
		}else{
			GameObject.Find("Player01").GetComponent<PlayerControls>().enabled = false;
			GameObject.Find("Player02").GetComponent<PlayerControls>().enabled = true;
		}
		if (!photonView.isMine) {
			GameObject.FindGameObjectWithTag ("Ball").GetComponent<BallControl> ().enabled = false;
		}else{
			GameObject.FindGameObjectWithTag ("Ball").GetComponent<BallControl> ().enabled = true;
		}
	}

}
