using Assets.Scripts.Entity;

namespace Assets.Scripts.Command
{
    /// <summary>敵のこうげき。はねかえされた場合は実行者自身がダメージを受ける。</summary>
    internal sealed class EnemyAttackCommand : IBattleCommand
    {
        public EnemyAttackCommand(EnemyEntity actor, IDamageable target)
        {
            _actor = actor;
            _target = target;
        }

        private readonly EnemyEntity _actor;
        private readonly IDamageable _target;

        public void Execute() => _actor.Attack(_target);
    }
}
