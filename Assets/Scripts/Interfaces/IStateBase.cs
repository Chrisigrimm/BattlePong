namespace Assets.Code.Interfaces{
	public interface IStateBase{
		void StateUpdate();
		void ShowIt();
		void StateLateUpdate();
		void getClick(string ButtonName);
	}
}