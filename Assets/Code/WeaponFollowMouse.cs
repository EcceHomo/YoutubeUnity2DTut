using UnityEngine;
using System.Collections;

public class WeaponFollowMouse : MonoBehaviour
{

    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x)*Mathf.Rad2Deg;

        if ((angle >= 0.0 && angle <= 90.0) || (angle <= 0.0 && angle >= -90.0))
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
