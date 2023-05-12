using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private int layerNumber;
    [SerializeField] private ItemType itemType;
    
    [SerializeField] private Vector3 itemScale;
    
    private bool _isInventoryActive;

    public bool active
    {
        set
        {
            _isInventoryActive = value;
        }
    }

    private void OnValidate()
    {
    }

    public void OnItemAdded()
    {
    }

    public void OnItemRemoved()
    {
        
    }

    // private IEnumerator RemoveItem()
    // {
    //     
    // }

    private XRGrabInteractable _lastInteraction;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<InteractableObject>(out var obj)) return;
        if (!obj.itemType.Equals(itemType)) return;
        _lastInteraction = obj.GetComponent<XRGrabInteractable>();
        // _lastInteraction.enabled = false;
        // Debug.Log(other.name+" has entered into socket "+other.gameObject.layer +" "+layerNumber);
        // if (other.gameObject.layer == layerNumber)
        // {
        //     var trans = other.transform;
        //     trans.SetParent(transform);
        //     trans.localPosition = new Vector3(0, 0, 0);
        //     trans.localRotation = Quaternion.Euler(Vector3.zero);
        //     trans.localScale = itemScale;
        //     Debug.Log("Grabbable Object");
        // }
    }

    private void OnTriggerStay(Collider other)
    {
    }

    private void OnTriggerExit(Collider other)
    {
        if (InputManager.instance.leftController.gripPressedHold)
        {
            
        }
    }
}