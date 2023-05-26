using System.Linq;
using HurricaneVR.Framework.Core.UI;
using UnityEngine;
using UnityEngine.UI;

public class AllSceneCanvas : MonoBehaviour
{
    [SerializeField] private HVRInputModule hvrInputModule;
    [SerializeField] private float testValue;
    private void OnValidate()
    {
        if (hvrInputModule == null) hvrInputModule = GetComponent<HVRInputModule>();
        hvrInputModule.UICanvases = FindObjectsByType<Canvas>(FindObjectsSortMode.None).Where(canvas => canvas.GetComponent<GraphicRaycaster>() != null).ToList();
    }
}
