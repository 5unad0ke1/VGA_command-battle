using Assets.Scripts.Entity;

namespace Assets.Scripts.Command
{
    /// <summary>
    /// プレイヤーのスキルこうげき。SP が無い場合は不発になるが、
    /// 現状は戻り値を見ていないためターンだけが消費される。
    /// </summary>
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
