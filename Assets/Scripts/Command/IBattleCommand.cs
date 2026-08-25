namespace Assets.Scripts.Command
{
    /// <summary>
    /// 実行者・対象・効果を確定させた 1 行動。生成(コマンド選択)と実行(結果の解決)を
    /// 切り離すための単位で、実行タイミングは <see cref="State.ActionResolveState"/> が握る。
    /// </summary>
    public interface IBattleCommand
    {
        /// <summary>行動を 1 回実行する。</summary>
        public void Execute();
    }
}
