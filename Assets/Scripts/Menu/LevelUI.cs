using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text levelNameTMP;
    [SerializeField] private RawImage levelThumbImg;
    [SerializeField] private TMP_Text levelDifficultyTMP;

    [SerializeField] private Transform lockPanel;
    [SerializeField] private TMP_Text lockMsg;
    [SerializeField] private Button playLvlBtn;

    public bool levelLocked;
    public bool underDevelopment;


    public string levelName
    {
        set => levelNameTMP.text = value;
    }

    public string levelDifficulty
    {
        set => levelDifficultyTMP.text = value;
    }

    private void Start()
    {
        if (levelLocked) lockMsg.text = "Level Locked";
        if (underDevelopment) lockMsg.text = "Under Development";
        lockPanel.gameObject.SetActive(levelLocked || underDevelopment);
        playLvlBtn.gameObject.SetActive(!(levelLocked || underDevelopment));
    }
}