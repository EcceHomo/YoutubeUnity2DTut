using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

    private Vector2 _direction;

    public static bool ShopActive;
    public Transform Player;


	// Use this for initialization
	void Start () {
        _direction = new Vector2(0, -1);
	}
	
	// Update is called once per frame
	void Update () {
        var raycast = Physics2D.Raycast(transform.position, _direction, 0.01f, 1 << LayerMask.NameToLayer("Player"));
	    if (!raycast)
	    {
	        ShopActive = false;
	       // print(Player.position);
	        return;
	    }
	    else
	    {
	       // print("I SEEEEEE YOUUUUU");
	        ShopActive = true;
	    }

	    Debug.DrawRay(transform.position, new Vector3(0, 0.01f, 0), Color.green);
	}
}
