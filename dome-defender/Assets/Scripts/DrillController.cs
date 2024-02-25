using UnityEngine;

public class DrillController : MonoBehaviour
{
    [SerializeField]
    private float range = 5f;
    [SerializeField]
    private LineRenderer laser;

    void Start() {
        laser.enabled = false;
    }

    void Update() {
        UpdateLaser();
    }

    // It's a laser drill, so it should be able to fire
    public void StartLaser() {
        laser.enabled = true;
    }

    public void UpdateLaser() {
        if (!laser.enabled) return;

        // shoot at rotation of drill
        RaycastHit2D hit = Physics2D.Raycast(laser.transform.position, transform.up, range);

        if (hit.collider != null) {
            laser.SetPosition(1, new Vector3(0, hit.distance, 0));
        } else {
            laser.SetPosition(1, new Vector3(0, range, 0));
        }
    }

    public void StopLaser() {
        laser.enabled = false;
    }

    public void Rotate(Vector3 direction) {
        if (direction == Vector3.zero) return;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, -angle);
    }
}
