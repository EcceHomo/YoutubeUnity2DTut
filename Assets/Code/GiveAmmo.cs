using UnityEngine;
using System.Collections;

public class GiveAmmo : MonoBehaviour
{

    public int CollectedAmmo;

    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player == null)
            return;

        WeaponHud.TotalAmmo += CollectedAmmo;

        gameObject.SetActive(false);
    }
}
