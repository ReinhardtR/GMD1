using System;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public bool IsControlled { get; private set; }
    public GameObject ControlCenter;

    private SpriteRenderer controlCenterSpriteRenderer;

    void Awake()
    {
        controlCenterSpriteRenderer = ControlCenter.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        IsControlled = false;
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
