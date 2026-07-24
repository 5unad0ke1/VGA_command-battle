using System;

namespace Assets.Scripts.ValueObject
{
    internal readonly struct SkillPoint
    {
        public SkillPoint(int current, int max)
        {
            Max = Math.Max(0, max);
            Current = Math.Clamp(current, 0, Max);
        }

        public readonly int Current;
        public readonly int Max;

        public bool TryConsume(out SkillPoint next)
        {
            if (Current <= 0)
            {
                next = this;
                return false;
            }

            next = new SkillPoint(Current - 1, Max);
            return true;
        }
    }
}
