using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;

    private Rigidbody2D _rigidbody;

    private Vector3 _movement;

    void Start() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate() {
        Vector3 delta = _speed * Time.fixedDeltaTime * _movement;
        _rigidbody.MovePosition(transform.position + delta);
    }

    public void OnMovement(InputValue value) {
        _movement = value.Get<Vector2>();
    }
}
