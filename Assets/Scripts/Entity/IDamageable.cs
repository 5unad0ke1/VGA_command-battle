using Assets.Scripts.ValueObject;

namespace Assets.Scripts.Entity
{
    internal interface IDamageable
    {
        public bool IsDead { get; }
        public void AddDamage(Damage damage, IDamageable attacker);
    }
}
