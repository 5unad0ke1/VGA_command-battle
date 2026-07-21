namespace Assets.Scripts
{
    public interface IState
    {
        public void Init();
        public void Update();
        public void Exit();
    }
}
