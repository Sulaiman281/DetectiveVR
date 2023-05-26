using System;
using System.Linq;
using HurricaneVR.Framework.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class MagnifyingGlass : MonoBehaviour
{
    [Header("Events")] [SerializeField] private PlayerInput.ActionEvent onClueFound;
    
    [Header("Ref")] [SerializeField] private HVRGrabbable grabbable;
    [SerializeField] private Transform rayTarget;
    [SerializeField] private Vector3 raySize;
    [SerializeField] private Vector3 posOffset;
    [SerializeField] private AudioClip clueSound;
    [SerializeField] private AudioClip triggerSound;

    [Header("Tease Dialogs")] [SerializeField]
    private AudioClip[] teaseDialogs;

    // public InputAction actionInput;

    // private void Start()
    // {
        // actionInput.Enable();
        // actionInput.performed += _ => { CheckClue(); };
    // }

    public void CheckClue()
    {
        if (!grabbable.IsHandGrabbed) return;
        AudioSource.PlayClipAtPoint(triggerSound, transform.position, .8f);
        try
        {
            var results = new Collider[10];
            Physics.OverlapBoxNonAlloc(rayTarget.TransformPoint(posOffset), raySize, results, rayTarget.rotation);
            var clueObjects = results.Where(obj => obj != null && obj.CompareTag("Clue") && !obj.GetComponent<ClueObject>().solved).ToList();
            if (clueObjects.Count == 0)
            {
                TeaseDialog();
                return;
            }

            clueObjects[0].GetComponent<ClueObject>().solved = true;
            AudioSource.PlayClipAtPoint(clueSound, transform.position, .8f);
            onClueFound.Invoke(default);

            // foreach (var clueObj in clueObjects)
            // {
                // clueObj.transform.GetComponent<ClueObject>().solved = true;
            // }
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    private void TeaseDialog()
    {
        try
        {
            PlayerSfxManager.instance.PlayClip(teaseDialogs[Random.Range(0, teaseDialogs.Length)]);
        }
        catch
        {
            // ignore
        }
    }

    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(rayTarget.TransformPoint(posOffset), raySize);
    }
}