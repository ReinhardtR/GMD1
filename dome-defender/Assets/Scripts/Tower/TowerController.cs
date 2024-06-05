using UnityEngine;

public class TowerController : MonoBehaviour
{
    public bool IsControlled { get; private set; }
    public GameObject ControlCenter;
    public GameObject Weapon;

    private readonly float rotationSpeed = 2f;
    private readonly float weaponLowBoundary = 0f;

    private Health health;
    private TowerWeaponController weaponController;
    private SpriteRenderer controlCenterSpriteRenderer;

    void Awake()
    {
        health = GetComponent<Health>();
        controlCenterSpriteRenderer = ControlCenter.GetComponent<SpriteRenderer>();
        weaponController = Weapon.GetComponent<TowerWeaponController>();
    }

    void Start()
    {
        IsControlled = false;
    }

    void OnEnable()
    {
        health.OnDamageEvent += OnDamage;
        health.OnDeathEvent += OnDeath;
    }

    void OnDisable()
    {
        health.OnDamageEvent -= OnDamage;
        health.OnDeathEvent -= OnDeath;
    }

    public void MoveWeaponLeft()
    {
        if (Weapon.transform.position.x <= 0 && Weapon.transform.position.y <= weaponLowBoundary)
        {
            Debug.Log("Weapon is at boundary");
            return;
        }
        RotateWeapon(rotationSpeed);
    }

    public void MoveWeaponRight()
    {
        if (Weapon.transform.position.x >= 0 && Weapon.transform.position.y <= weaponLowBoundary)
        {
            Debug.Log("Weapon is at boundary");
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
            Debug.Log("Enforcing nerdge");
            Weapon.transform.position = new Vector3(Weapon.transform.position.x, weaponLowBoundary, Weapon.transform.position.z);
            Weapon.transform.rotation = Quaternion.Euler(0, 0, Weapon.transform.position.x < 0 ? 90 : -90);
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

    private void OnDamage(int damage)
    {
        Debug.Log("Tower taking damage: " + damage);
    }

    private void OnDeath()
    {
        Debug.Log("Tower destroyed");
        GameManager.Instance.GameOver();
    }
}
