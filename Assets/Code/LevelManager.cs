using UnityEngine;
using System.Collections;

class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public Player Player { get; private set; }
    public CameraController Camera { get; private set; }

    public void Awake()
    {

    }

    public void Start()
    {
    }

    public void KilPlayer()
    {
    }

    private IEnumerator KillPlayerCo()
    {
        yield break;
    }

}