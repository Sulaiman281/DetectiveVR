using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DoorLevelUI : MonoBehaviour
{
    [Header("Reference")] [SerializeField] private string lvlName;
    [SerializeField] private Texture lvlImage;
    [SerializeField] private Sprite[] doorDifficulty;
    [SerializeField] private Image doorPanelUI;
    [Range(0, 4)] [SerializeField] private int difficulty;

    [Header("UI Element")]
    [SerializeField] private TMP_Text levelName;
    [SerializeField] private TMP_Text playerNameRank1;
    [SerializeField] private TMP_Text playerNameRank2;
    [SerializeField] private TMP_Text playerNameRank3;
    
    [Header("Event")]
    public PlayerInput.ActionEvent onPlayerEnterToDoor;

    private void OnValidate()
    {
        doorPanelUI.sprite = doorDifficulty[difficulty];
    }

    private void OnTriggerEnter(Collider other)
    {
        // move the player to the next level
    }
}
