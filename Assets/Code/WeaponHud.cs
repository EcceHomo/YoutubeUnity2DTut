using UnityEngine;
using System.Collections;

public class WeaponHud : MonoBehaviour {

    public GUISkin Skin;

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
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            _showTooltip = false;
            _singleItem = _weaponDatabase.Items[0];
            print("Mouse ScrollWheel Up, Gun");
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            _showTooltip = false;
            _singleItem = _weaponDatabase.Items[1]; ;
            print("Mouse ScrollWheel Down, Gloves");
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
        Rect weponRect = new Rect(60, 60, 50, 50);
        GUI.Box(weponRect, "", Skin.GetStyle("Weapon"));

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
        _tooltip = "<color=#4DA4BF>" + item.ItemName + "</color>\n\n" + "<color=#f2f2f2>" + item.ItemDesc + "</color>";
        return _tooltip;
    }
}
