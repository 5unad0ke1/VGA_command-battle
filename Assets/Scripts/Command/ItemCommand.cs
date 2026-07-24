using Assets.Scripts.Entity;

namespace Assets.Scripts.Command
{
    internal sealed class ItemCommand : IBattleCommand
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
