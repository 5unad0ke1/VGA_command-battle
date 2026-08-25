using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.ValueObject;

namespace Assets.Scripts.Entity
{
    /// <summary>
    /// プレイヤーの所持品。<see cref="Capacity"/> 個までを保持し、使用した分は取り除かれる
    /// (個数の概念は持たず、1 要素 = 1 個)。
    /// </summary>
    public sealed class Inventory
    {
        private const int Capacity = 5;

        /// <summary>上限を超えた分は切り捨てて初期化する。</summary>
        public Inventory(IEnumerable<ItemData> items)
        {
            _items = items.Take(Capacity).ToList();
        }

        private readonly List<ItemData> _items;

        public IReadOnlyList<ItemData> Items => _items;

        /// <summary>指定インデックスのアイテムを取り出し、所持品から取り除く。</summary>
        /// <returns>インデックスが範囲外なら false(このとき所持品は変化しない)。</returns>
        public bool TryTake(int index, out ItemData item)
        {
            if (index < 0 || index >= _items.Count)
            {
                item = default;
                return false;
            }

            item = _items[index];
            _items.RemoveAt(index);
            return true;
        }
    }
}
