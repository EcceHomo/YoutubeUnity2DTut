using UnityEngine;

public class PathedProjectileSpawner : MonoBehaviour
{
    public Transform Destination;
    public PathedProjectile Projectile;

    public float Speed;
    public float FireRate;

    private float _nextShotInSecodns;

    public void Start()
    {
        _nextShotInSecodns = FireRate;
    }

    public void Update()
    {
        if ((_nextShotInSecodns -= Time.deltaTime) > 0)
            return;

        _nextShotInSecodns = FireRate;
        var projectile = (PathedProjectile) Instantiate(Projectile, transform.position, transform.rotation);
        projectile.Initalize(Destination, Speed);
    }

    public void OnDrawGizmos()
    {
        if (Destination == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, Destination.position);
    }
}
