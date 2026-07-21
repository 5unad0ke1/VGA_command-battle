using System;

namespace Assets.Scripts.ValueObject
{
    internal readonly struct AttackPoint
    {
        public AttackPoint(int value)
        {
            Value = Math.Max(0, value);
        }

        public readonly int Value;
    }
}
