using System.Collections.Generic;
using ReadyPlayerMe.AvatarLoader;
using UnityEngine;

public class ChapterThreeManager : Singleton<ChapterThreeManager>
{
    [SerializeField] private string[] actorsAvatarUrl;
    [SerializeField] private RuntimeAnimatorController actorAnimatorController;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private Transform actorsParent;

    private Queue<Transform> _spawns;
    
    private void Start()
    {
        _spawns = new Queue<Transform>(spawnPoints);
        foreach (var s in actorsAvatarUrl)
        {
            LoadAvatar(s);
        }
    }

    private void LoadAvatar(string url)
    {
        var avatarLoader = new AvatarObjectLoader();
        avatarLoader.OnCompleted += (_, args) =>
        {
            try
            {
                var avatar = args.Avatar;
                AvatarAnimatorHelper.SetupAnimator(args.Metadata.BodyType, avatar);

                // set animator controller
                avatar.GetComponent<Animator>().runtimeAnimatorController = actorAnimatorController;

                var trans = _spawns.Dequeue();
                avatar.transform.SetPositionAndRotation(trans.position, trans.rotation);
                avatar.transform.SetParent(actorsParent);
                avatar.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }
            catch
            {
                //
            }
        };
        avatarLoader.LoadAvatar(url);
    }
}
