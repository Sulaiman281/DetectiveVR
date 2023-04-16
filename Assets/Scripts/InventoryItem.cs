using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor socketInteractor;
    [SerializeField] private XRGrabInteractable startingItem;

    private IXRSelectInteractable _itemSelected;

    private bool _isInventoryActive;

    public bool active
    {
        set
        {
            _isInventoryActive = value;
            
            if (_itemSelected == null)
            {
                if (value)
                {
                    startingItem.transform.position = transform.position;
                }
                return;
            }

            _itemSelected.transform.gameObject.SetActive(value);
        }
    }

    private void OnValidate()
    {
        if (socketInteractor == null) socketInteractor = transform.GetComponent<XRSocketInteractor>();
    }

    public void OnItemAdded()
    {
        _itemSelected = socketInteractor.GetOldestInteractableSelected();
    }

    public void OnItemRemoved()
    {
        if (!transform.parent.gameObject.activeSelf) return;
        _itemSelected = null;
        // StartCoroutine(RemoveItem());
    }

    private IEnumerator RemoveItem()
    {
        yield return new WaitForSeconds(.5f);
        if (_isInventoryActive)
            _itemSelected = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " has triggered with the socket");
    }
}