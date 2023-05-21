using System;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

public class ItemInPocket : MonoBehaviour
{
    [SerializeField] private HVRGrabbable grabbable;
    [SerializeField] private HVRSocket inventorySocket;

    private void LateUpdate()
    {
        try
        {
            if (!grabbable.IsHandGrabbed && !grabbable.IsSocketed)
            {
                Debug.Log("Item Needs to Grabbed" + inventorySocket.TryGrab(grabbable));
            }
        }
        catch (Exception)
        {
            // ignore
        }
    }
}