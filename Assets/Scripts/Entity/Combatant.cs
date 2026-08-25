using Assets.Scripts.ValueObject;

namespace Assets.Scripts.Entity
{
    /// <summary>HP の保持と被ダメージ処理を共通化した戦闘者の基底。</summary>
    public abstract class Combatant : IDamageable
    {
        protected Combatant(HealthPoint hp)
        {
            Hp = hp;
        }

        public HealthPoint Hp { get; private set; }
        public bool IsDead => Hp.IsDead;

        /// <summary>
        /// 素直に HP を減らす既定の実装。はねかえしのような割り込みは派生側で override する。
        /// </summary>
        public virtual void AddDamage(Damage damage, IDamageable attacker)
        {
            Hp = Hp.Damage(damage);
        }

        /// <summary>HP を回復する。上限は <see cref="HealthPoint"/> 側で丸められる。</summary>
        public void Heal(int amount)
        {
            Hp = Hp.Heal(amount);
        }
    }
}
