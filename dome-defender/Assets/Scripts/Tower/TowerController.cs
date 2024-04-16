using UnityEngine;

public class TowerController : MonoBehaviour
{
    public bool IsControlled { get; private set; }
    public GameObject ControlCenter;
    public GameObject Weapon;

    private readonly float rotationSpeed = 1f;
    private readonly float weaponLowBoundary = 0f;

    private TowerWeaponController weaponController;
    private SpriteRenderer controlCenterSpriteRenderer;

    void Awake()
    {
        controlCenterSpriteRenderer = ControlCenter.GetComponent<SpriteRenderer>();
        weaponController = Weapon.GetComponent<TowerWeaponController>();
    }

    void Start()
    {
        IsControlled = false;
    }

    public void MoveWeaponLeft()
    {
        if (Weapon.transform.position.x <= 0 && Weapon.transform.position.y <= weaponLowBoundary)
        {
            return;
        }
        RotateWeapon(rotationSpeed);
    }

    public void MoveWeaponRight()
    {
        if (Weapon.transform.position.x >= 0 && Weapon.transform.position.y <= weaponLowBoundary)
        {
            return;
        }
        RotateWeapon(-rotationSpeed);
    }

    private void RotateWeapon(float rotation)
    {
        Weapon.transform.RotateAround(transform.position, Vector3.forward, rotation);
        EnforceWeaponBoundary();
    }

    private void EnforceWeaponBoundary()
    {
        if (Weapon.transform.position.y < weaponLowBoundary)
        {
            Weapon.transform.position = new Vector3(Weapon.transform.position.x, weaponLowBoundary, Weapon.transform.position.z);
        }
    }

    public void FireWeapon()
    {
        weaponController.FireWeapon();
    }

    public void TakeControl()
    {
        Debug.Log("Taking control of tower");
        IsControlled = true;
        controlCenterSpriteRenderer.color = Color.red;
    }

    public void ReleaseControl()
    {
        Debug.Log("Releasing control of tower");
        IsControlled = false;
        controlCenterSpriteRenderer.color = Color.green;
    }
}
