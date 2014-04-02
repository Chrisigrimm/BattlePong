namespace Assets.Code.Interfaces{
	public interface IStateBase{
		void StateUpdate();
		void ShowIt();
		void getClick(string ButtonName);
		void StateLateUpdate();
	}
}