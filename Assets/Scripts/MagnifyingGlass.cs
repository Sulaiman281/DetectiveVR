using System;
using System.Linq;
using HurricaneVR.Framework.Core;
using UnityEngine;
using Random = UnityEngine.Random;

public class MagnifyingGlass : MonoBehaviour
{

    [Header("Ref")] [SerializeField] private HVRGrabbable grabbable;
    [SerializeField] private Transform rayTarget;
    [SerializeField] private Vector3 raySize;
    [SerializeField] private Vector3 posOffset;
    [SerializeField] private AudioClip clueSound;

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

            foreach (var clueObj in clueObjects)
            {
                clueObj.transform.GetComponent<ClueObject>().solved = true;
                AudioSource.PlayClipAtPoint(clueSound, transform.position, .8f);
            }
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

    //
    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireCube(rayTarget.TransformPoint(posOffset), raySize);
    // }
}