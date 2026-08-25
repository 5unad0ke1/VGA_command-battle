using Assets.Scripts.Entity;

namespace Assets.Scripts.Command
{
    /// <summary>プレイヤーの通常こうげき。消費リソースが無いため必ず成立する。</summary>
    internal sealed class AttackCommand : IBattleCommand
    {
        public AttackCommand(PlayerEntity actor, IDamageable target)
        {
            _actor = actor;
            _target = target;
        }

        private readonly PlayerEntity _actor;
        private readonly IDamageable _target;

        public void Execute() => _actor.Attack(_target);
    }
}
