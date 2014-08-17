using System.Collections.Generic;
using UnityEngine;

public class WeaponDatabase : MonoBehaviour {

    public List<Item> Items = new List<Item>();

    void Start()
    {
        Items.Add(new Item("Gun", 0, "Simple to use 9mm gun", 5, 2, Item.ItemType.Weapon));
        Items.Add(new Item("Gloves", 1, "Gloves made for traning", 3, 4, Item.ItemType.Weapon));
    }

}
