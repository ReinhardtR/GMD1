using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private GameObject drill;
    private DrillController drillController;

    private Rigidbody2D rb;

    private Vector2 movement;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        drillController = drill.GetComponent<DrillController>();
    }

    public void FixedUpdate() {
        MovePlayer();
        drillController.Rotate(movement.normalized);
    }

    public void OnMovement(InputValue value) {
        movement = value.Get<Vector2>();
    }

    public void OnPrimary(InputValue value) {
        if (value.isPressed) {
            drillController.StartLaser();
        } else {
            drillController.StopLaser();
        }
    }

    private void MovePlayer() {
        Vector2 delta = speed * Time.fixedDeltaTime * movement;
        rb.MovePosition((Vector2) transform.position + delta);
    }
}
