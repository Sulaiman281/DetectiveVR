using System;
using System.Collections;
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
                StartCoroutine(PutItBack(2));
            }
        }
        catch (Exception)
        {
            // ignore
        }
    }

    private IEnumerator PutItBack(float delay)
    {
        yield return new WaitForSeconds(delay);
        inventorySocket.TryGrab(grabbable);
    }
}