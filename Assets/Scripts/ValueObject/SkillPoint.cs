using System;

namespace Assets.Scripts.ValueObject
{
    /// <summary>スキルの使用回数。常に 0〜Max に丸められる不変値。</summary>
    internal readonly struct SkillPoint
    {
        public SkillPoint(int current, int max)
        {
            Max = Math.Max(0, max);
            Current = Math.Clamp(current, 0, Max);
        }

        public readonly int Current;
        public readonly int Max;

        /// <summary>1 消費した値を <paramref name="next"/> に返す。</summary>
        /// <returns>残量が無い場合は false(このとき <paramref name="next"/> は自身のまま)。</returns>
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
