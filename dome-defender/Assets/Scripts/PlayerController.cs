using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    private LaserDrillController drill;
    private Rigidbody2D rb;
    private Vector2 direction;
    private bool isBoosting;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        drill = GetComponentInChildren<LaserDrillController>();
    }

    void Start()
    {
        isBoosting = false;
        direction = transform.up;
    }

    void Update()
    {
        if (Input.GetButton("Primary")) drill.StartDrill();
        else drill.StopDrill();
    }

    void FixedUpdate()
    {
        isBoosting = Input.GetButton("Boost");

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            direction = new Vector2(horizontal, vertical).normalized;
        }

        drill.Rotate(direction);

        if (isBoosting) rb.velocity = direction * speed;
        else rb.velocity = Vector2.zero;
    }
}
