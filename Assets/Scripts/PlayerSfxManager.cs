using UnityEngine;

public class PlayerSfxManager : Singleton<PlayerSfxManager>
{
    [SerializeField] private AudioSource audioSource;

    private bool _isPlaying;
    
    private void OnValidate()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClip clip)
    {
        if (_isPlaying) return;
        audioSource.clip = clip;
        audioSource.Play();
        _isPlaying = true;
        Invoke(nameof(ExpireRestriction), clip.length+.2f);
    }

    private void ExpireRestriction()
    {
        _isPlaying = false;
    }
}
