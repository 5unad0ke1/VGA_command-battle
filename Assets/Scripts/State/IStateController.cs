namespace Assets.Scripts.State
{
    /// <summary>State から次の State への遷移を要求するための窓口。</summary>
    internal interface IStateController
    {
        /// <summary>
        /// 現在の State を <paramref name="next"/> に切り替える。
        /// 遷移先は呼び出し側の State が明示的に渡す(遷移先を型から推測させない)。
        /// </summary>
        public void ChangeState(IState next);
    }
}
