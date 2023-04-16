using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    [SerializeField] private InventoryItem[] items;

    private void OnEnable()
    {
        foreach (var inventoryItem in items)
        {
            inventoryItem.active = true;
        }
    }

    private void OnDisable()
    {
        foreach (var inventoryItem in items)
        {
            inventoryItem.active = false;
        }
    }

    private void OnValidate()
    {
        items = transform.GetComponentsInChildren<InventoryItem>();
    }
}
