public enum ShopItemType
{
    Heal,
    TempAttackBuff
}

[System.Serializable]
public class ShopItem
{
    public string itemName;
    public string description;
    public int cost;
    public ShopItemType itemType;
    public int effectValue;
}
