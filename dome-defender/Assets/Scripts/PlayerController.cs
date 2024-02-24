using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private Transform drill;

    private Rigidbody2D rb;

    private Vector3 movement;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate() {
        MovePlayer();
        RotateDrill();
    }

    public void OnMovement(InputValue value) {
        movement = value.Get<Vector2>();
    }

    private void MovePlayer() {
        Vector3 delta = speed * Time.fixedDeltaTime * movement;
        rb.MovePosition(transform.position + delta);
    }

    private void RotateDrill() {
        if (movement == Vector3.zero) return;
        Vector3 direction = movement.normalized;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        drill.rotation = Quaternion.Euler(0, 0, -angle);
    }
}
