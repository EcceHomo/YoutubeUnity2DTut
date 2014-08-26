using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ItemDatabase : MonoBehaviour {
    public List<Item> Items = new List<Item>();

    void Start()
    {
        Items.Add(new Item("Amulet of Prayers",0, "An amulator  enchanted by prayers", 2, 0, Item.ItemType.Weapon));
        Items.Add(new Item("White Shirt", 1, "White Shirt of mana", 0, 0, Item.ItemType.Weapon));
        Items.Add(new Item("Amulet of Prayers", 2, "That potion that temporarily increases your power", 0, 0, Item.ItemType.Consumable));
        Items.Add(new Item("Gun", 3, "Simple to use 9mm gun", 5, 2, Item.ItemType.Consumable));

    }
}
