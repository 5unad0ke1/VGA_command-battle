using System;

namespace Assets.Scripts.ValueObject
{
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

        public HealthPoint Damage(Damage damage) => new(Current - damage.Value, Max);
        public HealthPoint Heal(int amount) => new(Current + amount, Max);
    }
}
