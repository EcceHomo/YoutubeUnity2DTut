using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
    public Transform Player;

    public Vector2 
        Margin,
        Smoothing;

    public BoxCollider2D Bounds;
    public BoxCollider2D ShopBounds;

    private Vector3
        _min,
        _max,
        _shopMin,
        _shopMax;

    public bool IsFollowing { get; set; }

    public void Start()
    {
        _min = Bounds.bounds.min;
        _max = Bounds.bounds.max;
        _shopMin = ShopBounds.bounds.min;
        _shopMax = ShopBounds.bounds.max;
        IsFollowing = true;
    }

    public void Update()
    {
        var x = transform.position.x;
        var y = transform.position.y;

        if (IsFollowing)
        {
            if (Mathf.Abs(x - Player.position.x) > Margin.x)
                x = Mathf.Lerp(x, Player.position.x, Smoothing.x * Time.deltaTime);

            if (Mathf.Abs(y - Player.position.y) > Margin.y)
                y = Mathf.Lerp(y, Player.position.y, Smoothing.y * Time.deltaTime);

        }

        // Veličina kamere kada je Shop uključen


        var cameraHalfWidth = camera.orthographicSize * ((float)Screen.width / Screen.height);


        //transform.position = new Vector3(x, y, transform.position.z);

        if (!Shop.ShopActive)
        {
            x = Mathf.Clamp(x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
            y = Mathf.Clamp(y, _min.y + camera.orthographicSize, _max.y - camera.orthographicSize);

            transform.position = new Vector3(x, y, transform.position.z);
            Camera.main.orthographicSize = 9.52f;
        }

        else if (Shop.ShopActive)
        {
            x = Mathf.Clamp(x, _shopMin.x + cameraHalfWidth, _shopMax.x - cameraHalfWidth);
            y = Mathf.Clamp(y, _shopMin.y + camera.orthographicSize, _shopMax.y - camera.orthographicSize);

            transform.position = new Vector3(x, y, transform.position.z);
            Camera.main.orthographicSize = 6.52f;
        }

    }
}
