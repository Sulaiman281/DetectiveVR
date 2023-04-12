using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClueFinderGlass : MonoBehaviour
{
    public bool canFindClue;
    public PlayerInput.ActionEvent onClueFind;

    [SerializeField] private Camera cam;
    
    [Header("Award PopUp")]
    [SerializeField] private AudioClip clip;
    [SerializeField] private TMP_Text awardText;
    [SerializeField] private Animator awardObject;


    public bool IsObjectVisible()
    {
        var viewPortPos = cam.WorldToViewportPoint(transform.position);
        return viewPortPos.x >= 0 && viewPortPos.x <= .8 &&
               viewPortPos.y >= 0 && viewPortPos.y <= .8 &&
               viewPortPos.z > 0;
    }

    public void ShowAwardText(string awardPoint)
    {
        awardObject.gameObject.SetActive(true);
        awardObject.SetTrigger("Award");
        awardText.text = awardPoint;
        AudioSource.PlayClipAtPoint(clip, transform.position);
        Invoke(nameof(HideAwardObj), 2f);
    }

    public void HideAwardObj()
    {
        awardObject.gameObject.SetActive(false);
    }

    // public bool IsObjectAtCenterOfView()
    // {
    //     var viewPortPos = cam.WorldToViewportPoint(transform.position);
    //     Debug.Log(viewPortPos);
    //     return Mathf.Abs(viewPortPos.x - 0.5f) < 0.05f && Mathf.Abs(viewPortPos.y - 0.5f) < 0.05f;
    // }
}
