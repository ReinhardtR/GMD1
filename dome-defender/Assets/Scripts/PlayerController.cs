using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    private LaserDrillController drill;
    private Rigidbody2D rb;
    private Vector2 direction;
    private bool isBoosting;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        drill = GetComponentInChildren<LaserDrillController>();
        isBoosting = false;
        direction = transform.up;
    }

    void Update()
    {
        HandleInput();
        MovePlayer();
        drill.Rotate(direction);
    }

    private void HandleInput()
    {
        // Drilling
        if (Input.GetButton("Primary")) drill.StartDrill();
        else drill.StopDrill();

        // Boosting
        isBoosting = Input.GetButton("Boost");

        // Movement/Direction Change
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            direction = new Vector2(horizontal, vertical).normalized;
        }
    }

    private void MovePlayer()
    {
        if (!isBoosting) return;
        Vector2 delta = speed * Time.fixedDeltaTime * direction;
        rb.MovePosition((Vector2)transform.position + delta);
    }
}
