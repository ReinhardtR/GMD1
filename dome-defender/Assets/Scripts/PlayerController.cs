using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    private LaserDrillController laserDrill;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Transform laserDrillGameObject = transform.Find("LaserDrill");
        laserDrill = laserDrillGameObject.GetComponent<LaserDrillController>();
    }

    void Update()
    {
        MovePlayer();
        laserDrill.Rotate(movement.normalized);
    }

    public void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    public void OnPrimary(InputValue value)
    {
        if (value.isPressed)
        {
            laserDrill.StartLaser();
        }
        else
        {
            laserDrill.StopLaser();
        }
    }

    private void MovePlayer()
    {
        Vector2 delta = speed * Time.fixedDeltaTime * movement;
        rb.MovePosition((Vector2)transform.position + delta);
    }
}
