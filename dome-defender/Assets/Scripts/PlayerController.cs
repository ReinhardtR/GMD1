using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;

    private Rigidbody2D _rigidbody;
    private Transform _drill;

    private Vector3 _movement;
    private Vector2 _direction;

    void Start() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _drill = transform.Find("Drill");
    }

    public void FixedUpdate() {
        Vector3 delta = _speed * Time.fixedDeltaTime * _movement;
        _rigidbody.MovePosition(transform.position + delta);

        if (!_direction.Equals(Vector2.zero)) { 
            float radian = Mathf.Atan2(_direction.y, _direction.x);
            float degree = radian * Mathf.Rad2Deg;
            float z = degree - 90;
            _drill.rotation = Quaternion.Euler(0, 0, z);
        }
    }

    public void OnMovement(InputValue value) {
        _movement = value.Get<Vector2>();
        _direction = _movement.normalized;
    }
}
