using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed { get; } = 5f;
    public bool IsControllingTower => towerController != null && towerController.IsControlled;

    private LaserDrillController drill;
    private Rigidbody2D rb;

    private Vector2 direction;
    private TowerController towerController;
    private GameObject controlCenter;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        drill = GetComponentInChildren<LaserDrillController>();
    }

    void Start()
    {
        controlCenter = null;
        towerController = null;
        direction = transform.up;
    }

    void Update()
    {
        CheckForTowerInteraction();
        CheckForDrillFiring();
    }

    void FixedUpdate()
    {
        UpdatePlayerMovement();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TowerControlCenter"))
        {
            controlCenter = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("TowerControlCenter"))
        {
            controlCenter = null;
        }
    }

    private void UpdatePlayerMovement()
    {
        if (towerController != null)
        {
            if (rb.velocity != Vector2.zero)
            {
                rb.velocity = Vector2.zero;
            }
            return;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            direction = new Vector2(horizontal, vertical).normalized;
        }
        drill.Rotate(direction);

        if (Input.GetButton("Boost"))
        {
            rb.velocity = direction * Speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void CheckForDrillFiring()
    {
        if (towerController != null)
        {
            if (drill.isFiring)
            {
                drill.StopDrill();
            }
            return;
        }


        if (Input.GetButton("Primary"))
        {
            drill.StartDrill();
        }
        else if (drill.isFiring)
        {
            drill.StopDrill();
        }
    }

    private void CheckForTowerInteraction()
    {
        if (!Input.GetButtonDown("Interact"))
        {
            return;
        }

        if (IsControllingTower)
        {
            towerController.ReleaseControl();
            towerController = null;
        }
        else if (towerController == null && controlCenter != null)
        {
            GameObject towerObject = controlCenter.transform.parent.gameObject;
            TowerController newTowerController = towerObject.GetComponent<TowerController>();
            if (newTowerController != null && !newTowerController.IsControlled)
            {
                towerController = newTowerController;
                towerController.TakeControl();
            }
        }
    }
}
