using UnityEngine;

namespace Assets.Scripts.Entity
{
    /// <summary>
    /// スキル使用時に付与される、敵のこうげきをはねかえす一時効果。
    /// 判定は残り回数のあるあいだ被弾ごとに行い、成否にかかわらず回数を 1 消費する
    /// (= 「3 回のこうげきに対して、それぞれ 80% ではねかえす」であり、3 回はねかえす保証ではない)。
    /// </summary>
    internal sealed class ReflectState
    {
        public ReflectState(int remainingCount, float chance)
        {
            _remainingCount = remainingCount;
            _chance = chance;
        }

        private int _remainingCount;
        private readonly float _chance;

        public bool IsActive => _remainingCount > 0;

        /// <summary>はねかえしの判定を 1 回分行う。</summary>
        /// <returns>はねかえしが成立したら true。残り回数が無い場合は消費せず false。</returns>
        public bool TryReflect()
        {
            if (_remainingCount <= 0)
                return false;

            // 抽選の成否によらず回数を消費する。
            _remainingCount--;
            return Random.value < _chance;
        }
    }
}
