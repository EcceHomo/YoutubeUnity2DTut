using System.Collections.Generic;
using UnityEngine;

public class WeaponDatabase : MonoBehaviour {

    public List<Item> Items = new List<Item>();

    void Start()
    {

        Items.Add(new Item("Gloves", 0, "Gloves made for traning", 3, 4, Item.ItemType.Weapon));
    }

}
