using Assets.Scripts.ValueObject;

namespace Assets.Scripts.Entity
{
    internal sealed class PlayerEntity : Combatant
    {
        public PlayerEntity(
            HealthPoint hp,
            SkillPoint sp,
            AttackPoint normalAtk,
            AttackPoint skillAtk,
            Inventory inventory) : base(hp)
        {
            Sp = sp;
            Inventory = inventory;
            _normalAtk = normalAtk;
            _skillAtk = skillAtk;
        }

        public SkillPoint Sp { get; private set; }
        public Inventory Inventory { get; }

        private readonly AttackPoint _normalAtk;
        private readonly AttackPoint _skillAtk;
        private ReflectState _reflect;

        public void Attack(IDamageable target)
        {
            target.AddDamage(new Damage(_normalAtk.Value), this);
        }

        // 80%の確率で3回、敵のこうげきをはねかえす効果を付与しつつ攻撃する。SPが無ければ何もしない。
        public bool TrySkillAttack(IDamageable target)
        {
            if (!Sp.TryConsume(out var next))
                return false;

            Sp = next;
            _reflect = new ReflectState(remainingCount: 3, chance: 0.8f);
            target.AddDamage(new Damage(_skillAtk.Value), this);
            return true;
        }

        public bool TryUseItem(int itemIndex, PlayerEntity target)
        {
            if (!Inventory.TryTake(itemIndex, out var item))
                return false;

            target.Heal(item.HealAmount);
            return true;
        }

        public override void AddDamage(Damage damage, IDamageable attacker)
        {
            if (_reflect is { IsActive: true } && _reflect.TryReflect())
            {
                attacker.AddDamage(damage, this);
                return;
            }

            base.AddDamage(damage, attacker);
        }
    }
}
