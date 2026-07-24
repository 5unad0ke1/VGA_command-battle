using Assets.Scripts.Entity;

namespace Assets.Scripts.Command
{
    internal sealed class SkillCommand : IBattleCommand
    {
        public SkillCommand(PlayerEntity actor, IDamageable target)
        {
            _actor = actor;
            _target = target;
        }

        private readonly PlayerEntity _actor;
        private readonly IDamageable _target;

        public void Execute() => _actor.TrySkillAttack(_target);
    }
}
