using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhotonView))]
public class BallNetwork : Photon.MonoBehaviour {
	
	
	private Vector3 correctBallPos;
	private Vector2 correctVelocity;// We lerp towards this
	void Start(){

	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{

			// We own this player: send the others our data

				//stream.SendNext(TransformFormat.getBackTransPosition(transform.position));
				//stream.SendNext(TransformFormat.getBackTransVel(rigidbody2D.velocity));

			//stream.SendNext(TransformFormat.getBackTransPosition(transform.position));
			/*if(rigidbody2D.velocity != saveVel){
				stream.SendNext(TransformFormat.getBackTransVel(rigidbody2D.velocity));
				saveVel = rigidbody2D.velocity;
			}*/
		}
		else
		{

			// Network player, receive data

				//this.correctBallPos =  TransformFormat.getTransPosition((Vector3)stream.ReceiveNext());
				//this.correctVelocity =  TransformFormat.getTransVel((Vector2)stream.ReceiveNext());

		}
	}

	// Update is called once per frame
	void Update(){
		if (!photonView.isMine){
			//Teest
			//transform.position = Vector3.Lerp(transform.position, this.correctBallPos, Time.deltaTime * 10);
			//transform.position = this.correctBallPos;
		}
	}
}
