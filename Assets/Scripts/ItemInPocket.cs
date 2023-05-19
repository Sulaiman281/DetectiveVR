using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

public class ItemInPocket : MonoBehaviour
{
    [SerializeField] private HVRGrabbable grabbable;
    [SerializeField] private HVRSocket inventorySocket;
    private void LateUpdate()
    {
        if (!grabbable.IsHandGrabbed && !grabbable.IsSocketed)
        {
            Debug.Log("Item Needs to Grabbed"+inventorySocket.TryGrab(grabbable));
        }
    }
}
