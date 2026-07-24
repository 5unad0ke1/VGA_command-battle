using Assets.Scripts.ValueObject;

namespace Assets.Scripts.Entity
{
    internal abstract class Combatant : IDamageable
    {
        protected Combatant(HealthPoint hp)
        {
            Hp = hp;
        }

        public HealthPoint Hp { get; private set; }
        public bool IsDead => Hp.IsDead;

        public virtual void AddDamage(Damage damage, IDamageable attacker)
        {
            Hp = Hp.Damage(damage);
        }

        public void Heal(int amount)
        {
            Hp = Hp.Heal(amount);
        }
    }
}
