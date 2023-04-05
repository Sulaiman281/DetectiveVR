using UnityEngine;

public class FootSfx : MonoBehaviour
{
    [Header("SFX")] [SerializeField] private AudioClip[] footSfx;

    public void OnFootStep()
    {
        AudioSource.PlayClipAtPoint(footSfx[Random.Range(0, footSfx.Length)], transform.position);
    }
}
