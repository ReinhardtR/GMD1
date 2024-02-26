using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField]
    private float range = 5f;
    public int Damage = 15;
    [SerializeField]
    private float fireRate = 0.1f;
    [SerializeField]
    private Transform firePoint;

    private LineRenderer laser;
    private float lastFireTime;

    void Start()
    {
        laser = GetComponent<LineRenderer>();
        laser.enabled = false;
    }

    void Update()
    {
        UpdateLaser();
    }

    public void StartLaser()
    {
        laser.enabled = true;
    }
    public void StopLaser()
    {
        laser.enabled = false;
    }

    public void Rotate(Vector2 direction)
    {
        if (direction == Vector2.zero) return;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, -angle);
    }

    private void UpdateLaser()
    {
        if (!laser.enabled) return;

        Vector3 start = firePoint.position;
        Vector3 direction = transform.up;

        RaycastHit2D hit = Physics2D.Raycast(start, direction, range);

        Vector3 end = hit.collider ? hit.point : (start + direction * range);

        laser.SetPosition(0, start);
        laser.SetPosition(1, end);

        if (Time.time - lastFireTime >= fireRate)
        {
            if (hit.collider)
            {
                hit.collider.gameObject.SendMessage("OnLaserHit", this, SendMessageOptions.DontRequireReceiver);
            }

            lastFireTime = Time.time;
        }
    }
}