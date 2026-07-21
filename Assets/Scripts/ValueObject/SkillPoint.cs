using System;

namespace Assets.Scripts.ValueObject
{
    internal readonly struct SkillPoint
    {
        public SkillPoint(int value)
        {
            Value = Math.Max(0, value);
        }
        public readonly int Value;
    }
}
