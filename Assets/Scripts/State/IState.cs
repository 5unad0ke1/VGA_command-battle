namespace Assets.Scripts.State
{
    /// <summary>バトル進行を構成する State の最小インタフェース。</summary>
    public interface IState
    {
        /// <summary>この State に入った直後に 1 度だけ呼ばれる。</summary>
        public void Init();

        /// <summary>この State が現在の State である間、毎フレーム呼ばれる。</summary>
        public void Update();

        /// <summary>他の State へ遷移する直前に呼ばれる。</summary>
        public void Exit();
    }
}
