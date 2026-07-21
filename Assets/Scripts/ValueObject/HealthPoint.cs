using System;

namespace Assets.Scripts.ValueObject
{
    internal readonly struct HealthPoint
    {
        public HealthPoint(int value)
        {
            Value = Math.Max(0, value);
        }

        public readonly int Value;
    }
}
