namespace Assets.Scripts.ValueObject
{
    internal readonly struct ItemData
    {
        public ItemData(string name, int healAmount)
        {
            Name = name;
            HealAmount = healAmount;
        }

        public readonly string Name;
        public readonly int HealAmount;
    }
}
