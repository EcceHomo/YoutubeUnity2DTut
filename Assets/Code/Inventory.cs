using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{

    public int SlotsX, SlotsY;
    public GUISkin skin;
    public List<Item> inventory = new List<Item>();
    public List<Item> Slots = new List<Item>();
    public int PointsToTake = 10;

    private ItemDatabase _database;
    private bool _showInventroy;
    private bool _showTooltip;
    private string _tooltip;
    private bool _draggingItem;
    private Item _draggedItem;
    private int _prevIndex;
    private WeaponDatabase _weaponDatabase;


    void Start()
    {
        for (int i = 0; i < (SlotsX * SlotsY); i++)
        {
            Slots.Add(new Item());
            inventory.Add(new Item());
        }
        _database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
        AddItem(1);
        //RemoveItem(0);
        //print(InventoryContatins(1));
        _weaponDatabase = GameObject.FindGameObjectWithTag("Weapon Database").GetComponent<WeaponDatabase>();
    }


    void Update()
    {
        if (Shop.ShopActive)
        {
            _showInventroy = Shop.ShopActive;
        }
        else
        {
            _showInventroy = false;
            _showTooltip = false;
        }
    }

    void OnGUI()
    {
        _tooltip = "";
        GUI.skin = skin;
        if (_showInventroy)
        {
                DrawInventory();
        }

        if (_showTooltip)
        {
            skin.GetStyle("Tooltip").fontSize = Screen.width / 55; ;
            GUI.Box(new Rect(Event.current.mousePosition.x + 15f, Event.current.mousePosition.y, Screen.width / 7f, Screen.height / 3f), _tooltip, skin.GetStyle("Tooltip"));
        }

        if (_draggingItem)
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, Screen.width / 24f, Screen.height / 14f), _draggedItem.ItemIcone);
        }
    }

    void DrawInventory()
    {
        Event e = Event.current;
        int i = 0;
        for (int y = 3; y < SlotsY+3; y++)
        {
            // Počinjalo je od o te je bilo samo manje < od slotsx
            // poziciju u desno mijenjaš sa x, isto je bilo i sa slots y
            // y = 0, y = slotsY
            for (int x = 8; x < SlotsX+8; x++)
            {
                Rect slotRect = new Rect(x * (Screen.width / 23), y * (Screen.height / 13), Screen.width / 24f, Screen.height / 14f);
                GUI.Box(slotRect,"",skin.GetStyle("Slot"));
                Slots[i] = inventory[i];
                Item item = Slots[i];

                if (Slots[i].ItemName != null)
                {
                    GUI.DrawTexture(slotRect, Slots[i].ItemIcone);
                    if (slotRect.Contains(e.mousePosition))
                    {
                        _tooltip = CreateTooltip(Slots[i]);
                        _showTooltip = true;

                        if (e.button == 0 && e.type == EventType.mouseDrag && !_draggingItem)
                        {
                            _draggingItem = true;
                            _prevIndex = i;
                            _draggedItem = item;
                            inventory[i] = new Item();
                        }

                        if (e.type == EventType.mouseUp && _draggingItem)
                        {
                            inventory[_prevIndex] = inventory[i];
                            inventory[i] = _draggedItem;
                            _draggingItem = false;
                            _draggedItem = null;
                        }


                        if (e.isMouse && e.type == EventType.mouseDown && e.button == 1)
                        {
                            // Minimalno 10 pointa za kupovinu oružja
                            if (item.TypeOfItem == Item.ItemType.Weapon && GameManager.Instance.Points >= 10)
                            {
                                BuyWeapon(Slots[i], i, true);
                            }
                        }

                    }
                }
                else
                {
                    if (slotRect.Contains(e.mousePosition))
                    {
                        if (e.type == EventType.mouseUp && _draggingItem)
                        {
                            inventory[i] = _draggedItem;
                            _draggingItem = false;
                            _draggedItem = null;
                        }
                    }
                }

                if (_tooltip == "")
                {
                    _showTooltip = false;
                }

                i++;
            }
        }
    }

    string CreateTooltip(Item item)
    {
        _tooltip = "<color=#386D9A>" + item.ItemName + "</color>\n\n" + "<color=#FFFF00>" + item.ItemDesc + "</color>";
        return _tooltip;
    }


    void RemoveItem(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].ItemID == id)
            {
                inventory[i] = new Item();
                break;
            }
        }
    }

    private void BuyWeapon(Item item, int slot, bool deleteItem)
    {
                
        //PlayerStats.IncreaseStat(statid, butfamount, buffduration);
        GameManager.Instance.TakePoints(PointsToTake);  // uzmi 10 bodova na consumable
        print("Used consumeable: "+item.ItemName);
        _weaponDatabase.Items.Add(item);
                
        if (deleteItem)
        {
            inventory[slot] = new Item();
        }
    }

    void AddItem(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].ItemName == null)
            {
                for (int j = 0; j < _database.Items.Count; j++)
                {
                    if (_database.Items[j].ItemID == id)
                    {
                        inventory[i] = _database.Items[j];
                    }
                }
                break;
            }
        }
    }
}
