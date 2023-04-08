using UnityEngine;

public class ClueObject : MonoBehaviour
{
    [SerializeField] private Texture2D scratchTexture;
    [SerializeField] private SkinnedMeshRenderer mesh;
    [SerializeField] private ClueVisibility visibility;

    private bool _solved = false;

    private void OnValidate()
    {
        if (mesh == null) mesh = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        var referenceMat = Resources.Load<Material>("ScratchReferenceMat");
        var newMat = new Material(referenceMat);
        mesh.material = newMat;
        if (scratchTexture == null) return;
        newMat.mainTexture = scratchTexture;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_solved) return;
        if (!other.CompareTag("MagnifyingGlass")) return;
        if (!visibility.isVisibleToEye) return;
        var script = other.transform.root.GetComponent<ClueFinderGlass>();
        script.ShowAwardText("XP10+");
        script.onClueFind.Invoke(default);
        _solved = true;
        DestroyImmediate(gameObject);
    }
}