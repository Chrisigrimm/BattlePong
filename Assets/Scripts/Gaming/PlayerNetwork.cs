using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhotonView))]
public class PlayerNetwork : Photon.MonoBehaviour {
	
	
	private Vector3 correctPlayerPos; // We lerp towards this
	private GameObject Player01;

	void Start(){
		this.correctPlayerPos = transform.position;
	}

	// Update is called once per frame
	
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			// We own this player: send the others our data
			//stream.SendNext(TransformFormat.getBackTransPosition(transform.position).y);
		}
		else
		{
			// Network player, receive data
			//this.correctPlayerPos = new Vector3(transform.position.x,TransformFormat.getTransPosition(new Vector3(0,(float)stream.ReceiveNext(),0)).y,transform.position.z);

		}
	}

	void Update(){
		if (!photonView.isMine){
			transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 10);
		}
	}
}
