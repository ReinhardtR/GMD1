using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private GameObject laser;

    private LaserController laserController;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        laserController = laser.GetComponent<LaserController>();
    }

    public void FixedUpdate()
    {
        MovePlayer();
        laserController.Rotate(movement.normalized);
    }

    public void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    public void OnPrimary(InputValue value)
    {
        if (value.isPressed)
        {
            laserController.StartLaser();
        }
        else
        {
            laserController.StopLaser();
        }
    }

    private void MovePlayer()
    {
        Vector2 delta = speed * Time.fixedDeltaTime * movement;
        rb.MovePosition((Vector2)transform.position + delta);
    }
}
