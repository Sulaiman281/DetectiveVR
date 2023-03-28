using UnityEngine;
using UnityEngine.Rendering;

public class ClueFinderGlass : MonoBehaviour
{
    public void OpaqueTexture(bool value)
    {
        GraphicsSettings.useScriptableRenderPipelineBatching = value;
    }
}
