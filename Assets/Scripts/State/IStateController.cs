namespace Assets.Scripts.State
{
    internal interface IStateController
    {
        // 旧: 型のswitchで次状態を推測していたExitCalloutを廃止し、
        // 各Stateが遷移先を明示的に渡す方式にする。
        public void ChangeState(IState next);
    }
}
