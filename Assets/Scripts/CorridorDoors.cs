using HurricaneVR.Framework.Components;
using UnityEngine;

public class CorridorDoors : MonoBehaviour
{
    [SerializeField] private HVRPhysicsDoor[] hvrDoors;
    [SerializeField] private HVRRotationTracker[] hvrDoorsRotation;
    [SerializeField] private Rigidbody[] doorsRigidbodies;

    private void OnValidate()
    {
        hvrDoors = transform.GetComponentsInChildren<HVRPhysicsDoor>();
        hvrDoorsRotation = transform.GetComponentsInChildren<HVRRotationTracker>();
        doorsRigidbodies = transform.GetComponentsInChildren<Rigidbody>();
    }

    public void DisableDoors(bool value = false)
    {
        foreach (var hvrPhysicsDoor in hvrDoors)
        {
            hvrPhysicsDoor.enabled = value;
        }

        foreach (var hvrRotationTracker in hvrDoorsRotation)
        {
            hvrRotationTracker.enabled = value;
        }
        
        foreach (var doorsRigidbody in doorsRigidbodies)
        {
            doorsRigidbody.isKinematic = !value;
        }
    }
}
