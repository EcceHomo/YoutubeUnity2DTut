using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ItemDatabase : MonoBehaviour {
    public List<Item> Items = new List<Item>();

    void Start()
    {
        Items.Add(new Item("Gun", 1, "Simple to use 9mm gun", 5, 2, Item.ItemType.Weapon));

    }
}
