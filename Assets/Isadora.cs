using ReadyPlayerMe.AvatarLoader;
using UnityEngine;

public class Isadora : MonoBehaviour
{
    [SerializeField] private string avatarUrl;
    [SerializeField] private RuntimeAnimatorController isadoraAnimator;
    [SerializeField] private Transform isadoraLocation;

    private GameObject _avatar;
    
    private void Start()
    {
        LoadAvatar();
    }

    private void LoadAvatar()
    {
        var avatarLoader = new AvatarObjectLoader();
        avatarLoader.OnCompleted += (_, args) =>
        {
            _avatar = args.Avatar;
            AvatarAnimatorHelper.SetupAnimator(args.Metadata.BodyType, _avatar);
            
            // set animator controller
            _avatar.GetComponent<Animator>().runtimeAnimatorController = isadoraAnimator;
            
            _avatar.transform.SetParent(isadoraLocation);
            _avatar.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        };
        avatarLoader.LoadAvatar(avatarUrl);
    }
}
