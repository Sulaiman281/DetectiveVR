using System;
using Chapter1;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public void SetChapter(Chapter chapter)
    {
        levelName = chapter.chapterName;
        levelDifficulty = chapter.levelDifficulty;
        levelThumbImg.texture = chapter.coverPic;
        
        playLvlBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(chapter.sceneBuildName);
        });
    }

    private void Start()
    {
        if (levelLocked)
        {
            lockMsg.text = "Level Locked";
            lockPanel.gameObject.SetActive(true);
            playLvlBtn.gameObject.SetActive(false);
        }
        if (underDevelopment)
        {
            lockMsg.text = "Under Development";
            lockPanel.gameObject.SetActive(true);
            playLvlBtn.gameObject.SetActive(false);
        }
    }
}