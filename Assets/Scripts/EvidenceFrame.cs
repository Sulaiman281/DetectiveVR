using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EvidenceFrame : MonoBehaviour
{
    [Header("UI Component")] [SerializeField]
    private TMP_Text headingTmp;

    [SerializeField] private Image evidenceImage;

    [Header("Values")] [SerializeField] private string evidenceTitle;
    [SerializeField] private Sprite evidenceTexture;

    private void OnValidate()
    {
        if (headingTmp == null) headingTmp = GetComponentInChildren<TMP_Text>();
        if (evidenceImage == null) evidenceImage = GetComponentInChildren<Image>();

        if (!string.IsNullOrEmpty(evidenceTitle)) headingTmp.text = evidenceTitle;
        if (evidenceTexture != null) evidenceImage.sprite = evidenceTexture;
    }
}
