using UnityEngine;

public class DropsItem : MonoBehaviour
{
    public Item Item;
    public int Amount;

    void OnDestroy()
    {
        if (Item == null || gameObject.activeSelf)
        {
            return;
        }

        ItemFactory.Instance.CreateItem(Item, transform.position, Amount);
    }
}
