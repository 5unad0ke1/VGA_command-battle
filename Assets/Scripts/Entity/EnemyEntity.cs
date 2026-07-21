using Assets.Scripts.ValueObject;

namespace Assets.Scripts.Entity
{
    internal class EnemyEntity : IDamageable
    {
        public EnemyEntity(HealthPoint hp, AttackPoint atk)
        {
            _hp = hp;
            _atk = atk;
        }
        public void AddDamage(Damage damage)
        {
            _hp = new(_hp.Value - damage.Value);
        }

        private HealthPoint _hp;
        private AttackPoint _atk;

    }
}
