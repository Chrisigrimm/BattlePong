using UnityEngine;

namespace Assets.Code.Interfaces{
	public interface IStateBase{
		void StateUpdate();
		void ShowIt();
		void StateLateUpdate();
		void NGUIfeedback(GameObject GmObj,string Type);
	}
}
