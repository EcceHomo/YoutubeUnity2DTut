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
        AddItem(0);
        AddItem(1);
        AddItem(2);
        AddItem(3);
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
        if (GUI.Button(new Rect(40, 400, 100, 40), "Save"))
            SaveInventory();
        if (GUI.Button(new Rect(40, 450, 100, 40), "Load"))
            LoadInventory();

        _tooltip = "";
        GUI.skin = skin;
        if (_showInventroy)
        {
                DrawInventory();
        }

        if (_showTooltip)
        {
            GUI.Box(new Rect(Event.current.mousePosition.x + 15f, Event.current.mousePosition.y, 200, 200), _tooltip, skin.GetStyle("Tooltip") );
        }

        if (_draggingItem)
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50),_draggedItem.ItemIcone);
        }
    }

    void DrawInventory()
    {
        Event e = Event.current;
        int i = 0;
        for (int y = 0; y < SlotsY; y++)
        {
            for (int x = 0; x < SlotsX; x++)
            {
                Rect slotRect = new Rect(x*60,y*60, 50, 50);
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
                            if (item.TypeOfItem == Item.ItemType.Consumable)
                            {
                                UseConsumable(Slots[i], i, true);
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

    private void UseConsumable(Item item, int slot, bool deleteItem)
    {
        switch (item.ItemID)
        {
            case 3:
            {
                //PlayerStats.IncreaseStat(statid, butfamount, buffduration);
                GameManager.Instance.TakePoints(PointsToTake);  // uzmi 10 bodova na consumable
                print("Used consumeable: "+item.ItemName);
                _weaponDatabase.Items.Add(item);
                break;
            }
        }
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

    bool InventoryContatins(int id)
    {
        bool result = false;
        for (int i = 0; i < inventory.Count; i++)
        {
            result = inventory[i].ItemID == id;
            if (result)
            {
                break;
            }
        }
        return result;
    }

    void SaveInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
        PlayerPrefs.SetInt("Inventory " + i, inventory[i].ItemID);
    }

    void LoadInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
            inventory[i] = PlayerPrefs.GetInt("Inventory " + i, -1) >= 0
                ? _database.Items[PlayerPrefs.GetInt("Inventory " + i)]
                : new Item();
    }
}
