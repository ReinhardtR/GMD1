using UnityEngine;

public class ShopOpener : MonoBehaviour
{
    public GameObject Shop;

    private bool playerInTrigger;

    void Update()
    {
        if (playerInTrigger && Input.GetButtonDown("Interact"))
        {
            ToggleShop();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        playerInTrigger = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        playerInTrigger = false;
    }

    public void ToggleShop()
    {
        bool shopActive = Shop.activeSelf;
        Shop.SetActive(!shopActive);
        GUIManager.Instance.SetPopupActive(!shopActive);
    }
}
