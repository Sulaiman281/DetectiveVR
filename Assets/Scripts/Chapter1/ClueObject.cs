using TMPro;
using UnityEngine;

public class ClueObject : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameObject popUpText;

    private bool _solved = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("MagnifyingGlass")) return;
        if (!Chapter1Manager.instance.findClueStart) return;
        if (_solved) return;
        _solved = true;
        AudioSource.PlayClipAtPoint(clip, transform.position);
        var popUp = Instantiate(popUpText, transform.position, Quaternion.identity);
        var text = popUp.transform.GetChild(0).GetComponent<TMP_Text>();
        text.text = "XP10+";
        Chapter1Manager.instance.ClueFound();
        DestroyImmediate(gameObject);
    }
}
