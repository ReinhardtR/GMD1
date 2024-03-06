using System;
using System.Collections.Generic;
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
    private float width = 0.6f; // Should be even number
    [SerializeField]
    private Transform firePoint;

    private LineRenderer laser;
    private float lastFireTime;

    void Awake()
    {
        laser = GetComponent<LineRenderer>();
    }

    void Start()
    {
        laser.startWidth = width;
        laser.endWidth = width;
        laser.enabled = false;

        lastFireTime = 0;
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

        bool isOnCooldown = Time.time < lastFireTime + fireRate;

        // Adjust 0.1f based on desired raycast spacing
        int numberOfRays = Mathf.CeilToInt(width / 0.1f);
        float farthestDistance = 0;
        List<int> collidersHit = new();
        for (int i = 0; i < numberOfRays; i++)
        {
            float offset = (width / 2) - (i * width / (numberOfRays - 1));
            Vector2 rayStart = start + ((Vector2)transform.right * offset);

            // Debug.DrawRay(rayStart, direction * range, Color.red);
            RaycastHit2D hit = Physics2D.Raycast(rayStart, direction, range);
            if (!hit.collider)
            {
                farthestDistance = range;
                continue;
            }

            // Ensure the same collider doesn't get hit multiple times
            int colliderID = hit.collider.GetInstanceID();
            if (collidersHit.Contains(colliderID)) continue;
            collidersHit.Add(colliderID);

            // To calculate where to end the laser, we need to find the farthest collider
            float distanceToCollider = Vector2.Distance(start, hit.collider.bounds.center);
            if (distanceToCollider > farthestDistance)
            {
                farthestDistance = distanceToCollider;
            }

            if (isOnCooldown) continue;

            Mineable mineable = hit.collider.gameObject.GetComponent<Mineable>();
            if (mineable)
            {
                mineable.TakeDamage(damage);
                lastFireTime = Time.time;
            }
        }

        float distance = farthestDistance > range ? range : farthestDistance;
        Vector2 end = start + direction * distance;

        // Debug.DrawLine(start, end, Color.red, 0.1f);
        laser.SetPosition(0, start);
        laser.SetPosition(1, end);
    }
}
