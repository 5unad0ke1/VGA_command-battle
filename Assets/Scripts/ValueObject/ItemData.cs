namespace Assets.Scripts.ValueObject
{
    /// <summary>どうぐ 1 個分の定義。現状は回復のみを表す。</summary>
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
