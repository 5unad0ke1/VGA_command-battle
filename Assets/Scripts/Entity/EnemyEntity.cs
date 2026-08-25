using Assets.Scripts.ValueObject;

namespace Assets.Scripts.Entity
{
    /// <summary>敵。単一のこうげき力を持つだけの単純な戦闘者。</summary>
    public sealed class EnemyEntity : Combatant
    {
        public EnemyEntity(HealthPoint hp, AttackPoint atk) : base(hp)
        {
            _atk = atk;
        }

        private readonly AttackPoint _atk;

        /// <summary>対象にこうげきする。はねかえされた場合は自分がダメージを受ける。</summary>
        public void Attack(IDamageable target)
        {
            target.AddDamage(new Damage(_atk.Value), this);
        }
    }
}
