using Assets.Scripts.ValueObject;

namespace Assets.Scripts.Entity
{
    internal class PlayerEntity : IDamageable
    {
        public PlayerEntity(HealthPoint health, SkillPoint skill)
        {
            _hp = health;
            _sp = skill;
        }
        public void AddDamage(Damage damage)
        {
            _hp = new(_hp.Value - damage.Value);
        }
        public void SkillAttack(IDamageable target)
        {
            _sp = new(_sp.Value - 1);
            target.AddDamage(new(_skillAtk.Value));
        }
        public void Attack(IDamageable target)
        {
            target.AddDamage(new(_normalAtk.Value));
        }

        private HealthPoint _hp;
        private SkillPoint _sp;
        private AttackPoint _normalAtk;
        private AttackPoint _skillAtk;
    }
}
