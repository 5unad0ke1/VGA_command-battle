using Assets.Scripts.ValueObject;

namespace Assets.Scripts.Entity
{
    /// <summary>プレイヤー。HP に加えて SP・所持品・はねかえし効果を持つ。</summary>
    public sealed class PlayerEntity : Combatant
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

        /// <summary>通常こうげき。消費リソースが無いため必ず成立する。</summary>
        public void Attack(IDamageable target)
        {
            target.AddDamage(new Damage(_normalAtk.Value), this);
        }

        /// <summary>
        /// SP を 1 消費してスキルこうげきを行い、はねかえし効果を付与する。
        /// 効果は「次に受ける 3 回のこうげきに対して、それぞれ 80% ではねかえす」で、
        /// 失敗しても回数は消費される(<see cref="ReflectState"/>)。
        /// 効果が残っているうちに再使用すると、残り回数は 3 に上書きされる。
        /// </summary>
        /// <returns>SP が足りず不発だった場合は false。</returns>
        public bool TrySkillAttack(IDamageable target)
        {
            if (!Sp.TryConsume(out var next))
                return false;

            Sp = next;
            _reflect = new ReflectState(remainingCount: 3, chance: 0.8f);
            target.AddDamage(new Damage(_skillAtk.Value), this);
            return true;
        }

        /// <summary>所持品から 1 つ消費して対象を回復する。</summary>
        /// <returns>インデックスが範囲外で不発だった場合は false。</returns>
        public bool TryUseItem(int itemIndex, PlayerEntity target)
        {
            if (!Inventory.TryTake(itemIndex, out var item))
                return false;

            target.Heal(item.HealAmount);
            return true;
        }

        /// <summary>
        /// はねかえしが成立した場合は自分は無傷で、同じダメージを攻撃元へ返す。
        /// </summary>
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
