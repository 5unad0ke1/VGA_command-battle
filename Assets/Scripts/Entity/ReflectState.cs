using UnityEngine;

namespace Assets.Scripts.Entity
{
    // スキル使用時に発動する「80%の確率で3回だけ敵のこうげきをはねかえす」を表す一時効果。
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

        public bool TryReflect()
        {
            if (_remainingCount <= 0)
                return false;

            _remainingCount--;
            return Random.value < _chance;
        }
    }
}
