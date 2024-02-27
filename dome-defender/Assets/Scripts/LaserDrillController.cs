using Unity.VisualScripting;
using UnityEngine;

public class LaserDrillController : MonoBehaviour
{
    [SerializeField]
    private int damage = 15;
    [SerializeField]
    private float fireRate = 0.1f;
    [SerializeField]
    private float range = 5f;
    [SerializeField]
    private float width = 0.5f;
    [SerializeField]
    private Transform firePoint;

    private LineRenderer laser;
    private float lastFireTime;

    void Start()
    {
        laser = GetComponent<LineRenderer>();
        laser.startWidth = width;
        laser.endWidth = width;
        laser.enabled = false;
    }

    void Update()
    {
        UpdateLaser();
    }

    public void StartDrill()
    {
        laser.enabled = true;
    }
    public void StopDrill()
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

        Vector2 start = firePoint.position;
        Vector2 direction = transform.up;
        Vector2 size = new(width, width);

        RaycastHit2D[] hits = Physics2D.BoxCastAll(start, size, 0, direction, range);

        Vector2 end = hits.Length > 0
            ? Vector2.zero
            : start + direction * range;

        bool hitRock = false;
        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log(hit.point);
            if (hit.distance > Vector2.Distance(transform.position, end))
            {
                end = hit.point;
            }

            if (Time.time - lastFireTime >= fireRate)
            {
                if (hit.collider)
                {
                    hit.collider.gameObject.SendMessage("OnMine", damage, SendMessageOptions.DontRequireReceiver);
                    hitRock = true;
                }
            }
        }

        if (hitRock) lastFireTime = Time.time;

        laser.SetPosition(0, start);
        laser.SetPosition(1, end);

        Debug.DrawLine(start, end, Color.red);

        Vector2 forwardDir = direction.normalized * range;
        Vector2 rightDir = new Vector2(direction.y, -direction.x) * size.x / 2f;

        Vector2 topLeft = start + forwardDir / 2f + rightDir;
        Vector2 topRight = start + forwardDir / 2f - rightDir;
        Vector2 bottomLeft = start - forwardDir / 2f + rightDir;
        Vector2 bottomRight = start - forwardDir / 2f - rightDir;

        Debug.DrawLine(topLeft, topRight, Color.green);
        Debug.DrawLine(topRight, bottomRight, Color.green);
        Debug.DrawLine(bottomRight, bottomLeft, Color.green);
        Debug.DrawLine(bottomLeft, topLeft, Color.green);
    }
}
