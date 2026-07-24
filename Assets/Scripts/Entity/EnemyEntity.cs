using Assets.Scripts.ValueObject;

namespace Assets.Scripts.Entity
{
    internal sealed class EnemyEntity : Combatant
    {
        public EnemyEntity(HealthPoint hp, AttackPoint atk) : base(hp)
        {
            _atk = atk;
        }

        private readonly AttackPoint _atk;

        public void Attack(IDamageable target)
        {
            target.AddDamage(new Damage(_atk.Value), this);
        }
    }
}
