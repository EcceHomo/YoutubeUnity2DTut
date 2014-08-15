using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class Item
{
    public string ItemName;
    public int ItemID;
    public string ItemDesc;
    public Texture2D ItemIcone;
    public int ItemPower;
    public int ItemSpeed;
    public ItemType TypeOfItem;

    public enum ItemType
    {
        Weapon,
        Consumable,
        Quest
    }

    public Item(string name, int id, string desc, int power, int speed, ItemType type)
    {
        ItemName = name;
        ItemID = id;
        ItemDesc = desc;
        ItemIcone = Resources.Load<Texture2D>("Item Icons/" + name);
        ItemPower = power;
        ItemSpeed = speed;
        TypeOfItem = type;
    }

    public Item()
    {
        ItemID = -1;
    }
}
