using UnityEngine;

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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name+" has entered into socket "+other.gameObject.layer +" "+layerNumber);
        if (other.gameObject.layer == layerNumber)
        {
            var trans = other.transform;
            trans.SetParent(transform);
            trans.localPosition = new Vector3(0, 0, 0);
            trans.localRotation = Quaternion.Euler(Vector3.zero);
            trans.localScale = itemScale;
            Debug.Log("Grabbable Object");
        }
    }
}