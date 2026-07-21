using System;

namespace Assets.Scripts.ValueObject
{
    internal readonly struct Damage
    {
        public Damage(int value)
        {
            Value = Math.Max(0, value);
        }
        public readonly int Value;
    }
}
