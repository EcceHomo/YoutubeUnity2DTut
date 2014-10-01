using UnityEngine;

public class DelegatesAndEvents:MonoBehaviour
{
    //private delegate void MojDelegat(int broj);

    //private MojDelegat _mojDelegat;

    //void Start()
    //{
    //    _mojDelegat = Printanje;

    //    if(_mojDelegat!=null)
    //        _mojDelegat(3);
    //}

    //void Printanje(int broj)
    //{
    //    print("Broj: "+broj);
    //}

    public delegate void MojDelegat();

    public event MojDelegat KliknutiDelegat;

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width/2 - 50, 5, 100, 30), "Clicked"))
        {
            if (KliknutiDelegat != null)
                KliknutiDelegat();
        }
    }
}