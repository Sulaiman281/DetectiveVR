using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private PlayerInput.ActionEvent onSelectInteraction;
    [SerializeField] private PlayerInput.ActionEvent onCancelInteraction;
    [SerializeField] private XRGrabInteractable grabInteractable;

    public ItemType itemType;
    
    [Header("Attach Trans Offset")]
    [SerializeField] private Transform attachTrans;
    [SerializeField] private Quaternion socketRotation;
    [SerializeField] private Vector3 offSetPosition;

    private Quaternion _orgAttachRot;
    private Vector3 _orgAttachPos;

    private void OnValidate()
    {
        if (grabInteractable == null) grabInteractable = transform.GetComponent<XRGrabInteractable>();
    }

    private void Start()
    {
        _orgAttachRot = attachTrans.localRotation;
        _orgAttachPos = attachTrans.localPosition;
        StartCoroutine(DelayTask(1.5f, () =>
        {
            onCancelInteraction.Invoke(default);
        }));
    }

    private IEnumerator DelayTask(float delayTime, Action task)
    {
        yield return new WaitForSeconds(delayTime);
        task.Invoke();
    }

    public void OnInteraction(bool start = true)
    {
        IXRSelectInteractor interactor = null;
        foreach (var xrSelectInteractor in grabInteractable.interactorsSelecting)
        {
            if (xrSelectInteractor.hasSelection)
            {
                // Debug.Log(xrSelectInteractor.transform.name+" has selection");
                interactor = xrSelectInteractor;
                break;
            }
        }

        if (interactor == null)
        {
            onCancelInteraction.Invoke(default);
            // attachTrans.localScale = Vector3.one;
            return;
        }
        // Debug.Log(interactor.transform.name+" Interactor");
        if (!interactor.transform.TryGetComponent<XRDirectInteractor>(out var comp))
        {
            attachTrans.localRotation = socketRotation;
            attachTrans.localPosition = offSetPosition;
            // attachTrans.localScale = Vector3.one / 2;
            onCancelInteraction.Invoke(default);
            return;
        }

        if (start)
        {
            onSelectInteraction.Invoke(default);
            attachTrans.localRotation = _orgAttachRot;
            attachTrans.localPosition = _orgAttachPos;
        }
        else
            onCancelInteraction.Invoke(default);
    }
}
