using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.ValueObject;

namespace Assets.Scripts.Entity
{
    internal sealed class Inventory
    {
        private const int Capacity = 5;

        public Inventory(IEnumerable<ItemData> items)
        {
            _items = items.Take(Capacity).ToList();
        }

        private readonly List<ItemData> _items;

        public IReadOnlyList<ItemData> Items => _items;

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
