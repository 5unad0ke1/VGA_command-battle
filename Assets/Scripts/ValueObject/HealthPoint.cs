using System;

namespace Assets.Scripts.ValueObject
{
    /// <summary>
    /// HP。常に 0〜Max に丸められる不変値で、増減は新しいインスタンスを返す
    /// (「範囲外の HP」を型として作れないようにするのが狙い)。
    /// </summary>
    internal readonly struct HealthPoint
    {
        public HealthPoint(int current, int max)
        {
            Max = Math.Max(0, max);
            Current = Math.Clamp(current, 0, Max);
        }

        public readonly int Current;
        public readonly int Max;

        public bool IsDead => Current <= 0;

        /// <summary>ダメージを引いた値を返す。0 未満にはならない。</summary>
        public HealthPoint Damage(Damage damage) => new(Current - damage.Value, Max);

        /// <summary>回復した値を返す。Max は超えない。</summary>
        public HealthPoint Heal(int amount) => new(Current + amount, Max);
    }
}
