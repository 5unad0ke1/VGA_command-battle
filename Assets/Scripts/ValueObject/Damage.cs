using System;

namespace Assets.Scripts.ValueObject
{
    /// <summary>1 回のこうげきで与えるダメージ量。負値は 0 に丸められる不変値。</summary>
    internal readonly struct Damage
    {
        public Damage(int value)
        {
            Value = Math.Max(0, value);
        }
        public readonly int Value;
    }
}
