using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public GameObject Tower;

    private PlayerController playerController;

    private float leaveTowerControlTimer;
    private readonly float smoothTransitionDuration = 0.2f;

    private readonly float towerCameraSize = 10f;
    private readonly float playerCameraSize = 4f;

    void Awake()
    {
        playerController = Player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerController.IsControllingTower)
        {
            FollowTower();
        }
        else
        {
            FollowPlayer();
        }

        if (playerController.IsControllingTower)
        {
            leaveTowerControlTimer = 0f;
        }
        else
        {
            leaveTowerControlTimer += Time.deltaTime;
        }
    }

    private void FollowPlayer()
    {
        ChangeSize(playerCameraSize);
        ChangePosition(Player.transform.position, leaveTowerControlTimer <= smoothTransitionDuration);
    }

    private void FollowTower()
    {
        ChangeSize(towerCameraSize);

        Vector2 position = Tower.transform.position + new Vector3(0, 4);
        ChangePosition(position, true);
    }

    private void ChangeSize(float size)
    {
        if (Camera.main.orthographicSize == size)
        {
            return;
        }

        Camera.main.orthographicSize = Mathf.Lerp(
            Camera.main.orthographicSize,
            size,
            0.1f
        );
    }

    private void ChangePosition(Vector2 position, bool smooth)
    {
        if (position == (Vector2)transform.position)
        {
            return;
        }

        if (smooth)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                new Vector3(
                    position.x,
                    position.y,
                    transform.position.z
                ),
                0.1f
            );
        }
        else
        {
            transform.position = new Vector3(
                position.x,
                position.y,
                transform.position.z
            );
        }
    }
}
