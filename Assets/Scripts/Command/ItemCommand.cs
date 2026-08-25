using Assets.Scripts.Entity;

namespace Assets.Scripts.Command
{
    /// <summary>
    /// プレイヤーのどうぐ使用。インデックスが所持品の範囲外なら不発になるが、
    /// 現状は戻り値を見ていないためターンだけが消費される。
    /// </summary>
    public sealed class ItemCommand : IBattleCommand
    {
        public ItemCommand(PlayerEntity actor, int itemIndex, PlayerEntity target)
        {
            _actor = actor;
            _itemIndex = itemIndex;
            _target = target;
        }

        private readonly PlayerEntity _actor;
        private readonly int _itemIndex;
        private readonly PlayerEntity _target;

        public void Execute() => _actor.TryUseItem(_itemIndex, _target);
    }
}
