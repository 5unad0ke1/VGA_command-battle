using System;

namespace Assets.Scripts.ValueObject
{
    /// <summary>こうげき力。負値は 0 に丸められる不変値。</summary>
    internal readonly struct AttackPoint
    {
        public AttackPoint(int value)
        {
            Value = Math.Max(0, value);
        }

        public readonly int Value;
    }
}
