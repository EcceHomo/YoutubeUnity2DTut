using UnityEngine;
using System.Collections;

public class WeaponHud : MonoBehaviour {

    public GUISkin Skin;
    public static int TotalAmmo = 20;
    public int AmmoToTake = 1;

    private bool _showWaeponHud;
    private bool _showTooltip;
    private string _tooltip;
    private WeaponDatabase _weaponDatabase;

    public static Item SingleItem;

    // Use this for initialization
    void Start()
    {
        _weaponDatabase = GameObject.FindGameObjectWithTag("Weapon Database").GetComponent<WeaponDatabase>();
        SingleItem = _weaponDatabase.Items[0];
        _showWaeponHud = !_showWaeponHud;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Shop.ShopActive)
        {
            _showWaeponHud = true;
            if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetButtonDown("Weapon1"))
            {
                Player.IsFiring = true;
                _showTooltip = false;
                SingleItem = _weaponDatabase.Items[0];
                print("Mouse ScrollWheel Up, Gun");
            }

            else if (Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetButtonDown("Weapon2"))
            {
                if (_weaponDatabase.Items.Count <= 1)
                {
                    print("Drugo oružje još nije implemenitrano");
                }
                else
                {
                    _showTooltip = false;
                    SingleItem = _weaponDatabase.Items[1];
                    print("Mouse ScrollWheel Down, Gloves");
                }
            }
        }
        else if (Shop.ShopActive)
        {
            _showWaeponHud = false;
            _showTooltip = false;
        }

        if (TotalAmmo <= 0 && SingleItem == _weaponDatabase.Items[1])
        {
            TotalAmmo = 0;
            Player.IsFiring = false;
            // print("Nema pucanja");
        }
        else
        {
            Player.IsFiring = true;
        }
    }

    void OnGUI()
    {
        _tooltip = "";
        if (_showWaeponHud)
        {
            DrawWeponGui(SingleItem.ItemID);
        }

        if (_showTooltip)
        {
            Skin.GetStyle("Tooltip").fontSize = Screen.width / 55; ;
            GUI.Box(new Rect(Event.current.mousePosition.x + 15f, Event.current.mousePosition.y, Screen.width / 7f, Screen.height / 3f), _tooltip, Skin.GetStyle("Tooltip"));}
    }

    void DrawWeponGui(int id)
    {
        Rect weponRect = new Rect(Screen.width / 5f, Screen.height / 80f, Screen.width / 24f, Screen.height / 14f);
        GUI.Box(weponRect, "", Skin.GetStyle("Weapon"));
        if (SingleItem != _weaponDatabase.Items[0])
        {
            //GameManager.Instance.TakeAmmo(AmmoToTake);
            //print("AmmoToTake: " + AmmoToTake + " TotalAmmo: " + TotalAmmo);
            Skin.GetStyle("Ammo").fontSize = Screen.width / 100; ;
            GUI.Label(new Rect(Screen.width / 5.1f, Screen.height / 26f, Screen.width / 24f, Screen.height / 14f), TotalAmmo.ToString(), Skin.GetStyle("Ammo")); // Ammo count
        }

        GUI.DrawTexture(weponRect, SingleItem.ItemIcone);

        if (weponRect.Contains(Event.current.mousePosition) && SingleItem != _weaponDatabase.Items[0])
        {
            _tooltip = CreateTooltip(_weaponDatabase.Items[1]);
            _showTooltip = true;
        }

        else if (weponRect.Contains(Event.current.mousePosition))
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
        _tooltip = "<color=#ff552a>" + item.ItemName + "</color>\n" + "<color=#d2d2fd>" + item.ItemDesc + "</color>\n\n"+
            "<color=#d4ff2a>" + "Power: " + "</color>" + "<color=#000000>" + item.ItemPower + "</color>\n"+
             "<color=#d4ff2a>" + "Speed: " + "</color>" + "<color=#000000>" + item.ItemSpeed + "</color>";
        return _tooltip;
    }
}
