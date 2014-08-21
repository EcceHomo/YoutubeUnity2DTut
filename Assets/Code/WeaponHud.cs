using UnityEngine;
using System.Collections;

public class WeaponHud : MonoBehaviour {

    public GUISkin Skin;
    public int TotalAmmo = 20;
    public int AmmoToTake = 1;

    private bool _showWaeponHud;
    private bool _showTooltip;
    private string _tooltip;
    private WeaponDatabase _weaponDatabase;
    private Item _singleItem;

    // Use this for initialization
    void Start()
    {
        _weaponDatabase = GameObject.FindGameObjectWithTag("Weapon Database").GetComponent<WeaponDatabase>();
        _singleItem = _weaponDatabase.Items[0];
        _showWaeponHud = !_showWaeponHud;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Shop.ShopActive)
        {
            _showWaeponHud = true;
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                Player.IsFiring = true;
                _showTooltip = false;
                _singleItem = _weaponDatabase.Items[0];
                print("Mouse ScrollWheel Up, Gun");
            }

            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                _showTooltip = false;
                _singleItem = _weaponDatabase.Items[1];
                print("Mouse ScrollWheel Down, Gloves");
            }
        }
        else if (Shop.ShopActive)
        {
            _showWaeponHud = false;
            _showTooltip = false;
        }

        if (Input.GetMouseButtonDown(0) && _singleItem == _weaponDatabase.Items[1])
        {
            TotalAmmo -= AmmoToTake;
        }

        if (TotalAmmo <= 0 && _singleItem == _weaponDatabase.Items[1])
        {
            TotalAmmo = 0;
            Player.IsFiring = false;
            //print("Nema pucanja");
        }
    }

    void OnGUI()
    {
        _tooltip = "";
        if (_showWaeponHud)
        {
            DrawWeponGui(_singleItem.ItemID);
        }

        if (_showTooltip)
        {
            GUI.Box(new Rect(Event.current.mousePosition.x + 15f, Event.current.mousePosition.y, 200, 200), _tooltip, Skin.GetStyle("Tooltip"));
        }
    }

    void DrawWeponGui(int id)
    {
        Rect weponRect = new Rect(100, 100, 50, 50);
        GUI.Box(weponRect, "", Skin.GetStyle("Weapon"));
        if (_singleItem == _weaponDatabase.Items[1])
        {
            //GameManager.Instance.TakeAmmo(AmmoToTake);
            //print("AmmoToTake: " + AmmoToTake + " TotalAmmo: " + TotalAmmo);
            GUI.Label(new Rect(104, 130, 50, 50), TotalAmmo.ToString()); // Ammo count
        }

        GUI.DrawTexture(weponRect, _singleItem.ItemIcone);

        if (weponRect.Contains(Event.current.mousePosition))
        {
            _tooltip = CreateTooltip(_weaponDatabase.Items[id]);
            _showTooltip = true;
        }


        if (_tooltip == "")
        {
            _showTooltip = false;
        }

    }

    string CreateTooltip(Item item)
    {
        _tooltip = "<color=#ff552a>" + item.ItemName + "</color>\n\n" + "<color=#d2d2fd>" + item.ItemDesc + "</color>\n\n"+
            "<color=#d4ff2a>" + "Power: " + "</color>" + "<color=#000000>" + item.ItemPower + "</color>\n\n"+
             "<color=#d4ff2a>" + "Speed: " + "</color>" + "<color=#000000>" + item.ItemSpeed + "</color>";
        return _tooltip;
    }
}
