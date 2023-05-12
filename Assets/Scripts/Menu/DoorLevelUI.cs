using Menu;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DoorLevelUI : MonoBehaviour
{
    [SerializeField] private Level doorLevel;
    
    [Header("Reference")]
    [SerializeField] private Image nextLvlImage;
    [SerializeField] private Sprite[] doorDifficulty;
    [SerializeField] private Image difficultyImageUI;
    // [Range(0, 4)] [SerializeField] private int difficulty;

    [Header("UI Element")] [SerializeField]
    private TMP_Text lvlNoTmp;
    [SerializeField] private TMP_Text levelName;
    
    [SerializeField] private TMP_Text[] leaderboard;
    
    [Header("Event")]
    public PlayerInput.ActionEvent onPlayerEnterToDoor;

    private void OnValidate()
    { 
        if(lvlNoTmp != null) lvlNoTmp.text = doorLevel.lvlNo + "";
        levelName.text = doorLevel.levelName;
        difficultyImageUI.sprite = doorDifficulty[doorLevel.difficulty];
        var index = 0;
        foreach (var playerName in doorLevel.top3Players)
        {
            if (string.IsNullOrEmpty(playerName)) continue;
            leaderboard[index++].text = playerName;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // move the player to the next level
        if (!other.CompareTag("Player")) return;
        onPlayerEnterToDoor.Invoke(default);
    }
}
