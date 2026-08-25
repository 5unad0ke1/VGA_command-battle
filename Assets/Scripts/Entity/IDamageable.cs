using Assets.Scripts.ValueObject;

namespace Assets.Scripts.Entity
{
    /// <summary>
    /// ダメージを受けられる対象。はねかえしで攻撃元へダメージを返す必要があるため、
    /// 攻撃側も同じ抽象で受け渡しする。
    /// </summary>
    internal interface IDamageable
    {
        public bool IsDead { get; }

        /// <summary>ダメージを与える。</summary>
        /// <param name="attacker">はねかえし時の返し先となる攻撃元。</param>
        public void AddDamage(Damage damage, IDamageable attacker);
    }
}
