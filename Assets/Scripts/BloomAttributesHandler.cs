using System;
using UnityEngine;
using UnityEngine.Rendering;

public class BloomAttributesHandler : MonoBehaviour
{
    [SerializeField] private Volume volume;
    [SerializeField] private float value;

    public VolumeParameter VolumeParameter;

    private void LateUpdate()
    {
        if (VolumeParameter == null) return;
        VolumeParameter.SetValue(new VolumeParameter<float> {value = value});
    }

    private void OnValidate()
    {
        if (volume == null) volume = GetComponent<Volume>();
        try
        {
            if (VolumeParameter != null) return;
            VolumeParameter = volume.sharedProfile.components[0].parameters[1];
            Debug.Log(VolumeParameter+" Value");
            VolumeParameter.SetValue(new VolumeParameter<float> {value = value});
        }
        catch
        {
            //
        }
        // foreach (var sharedProfileComponent in volume.sharedProfile.components)
        // {
            // Debug.Log("DisplayName: "+sharedProfileComponent.displayName);
            // foreach (var volumeParameter in sharedProfileComponent.parameters)
            // {
                // Debug.Log(volumeParameter+"\t"+volumeParameter.GetValue<float>());
            // }
        // }
    }
}
