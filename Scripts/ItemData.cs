using UnityEngine;

public class ItemData : MonoBehaviour
{

    public enum ItemType
    {
        Weapon,
        Consumable,
        QuestItem,
        Armor,
        Junk
    }

    public enum ItemRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    public enum PickableType
    {
        Pickable,
        NotPickable
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
