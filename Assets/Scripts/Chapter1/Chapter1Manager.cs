using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Chapter1Manager : Singleton<Chapter1Manager>
{
    [SerializeField] private TMP_Text subtitlesTMP;
    [SerializeField] private string[] dialogsSubtitles;
    [SerializeField] private PlayableDirector playableDirector;

    [SerializeField] private AudioClip correctSound, wrongSound;
    [SerializeField] private Transform tip2Obj, tip3Obj;
    [SerializeField] private TMP_Text msg, tip1, clueMsg;

    public PlayerManager playerManager;

    private int _index = -1;

    public bool findClueStart { get; set; }
    
    public int totalClues { set; get; }
    public int clueFound { set; get; }

    public void FindCluesStart()
    {
        findClueStart = true;
        playableDirector.Stop();
    }

    public void ClueFound()
    {
        clueFound++;
        clueMsg.text = $"Find Clues ({clueFound}/3)";
        if (clueFound < totalClues) return;
        AudioSource.PlayClipAtPoint(correctSound ,Camera.main != null ? Camera.main.transform.position: transform.position, 1);
        playerManager.ShowCanvas(true);
    }

    public void ChangeText(int x)
    {
        _index += x;
        if (_index >= dialogsSubtitles.Length) _index = 0;
        StartCoroutine(WriteText(dialogsSubtitles[_index]));
    }

    public void SuspectIsQuality(bool value)
    {
        if (value)
        {
            if (clueFound < 2)
            {
                AudioSource.PlayClipAtPoint(wrongSound ,Camera.main != null ? Camera.main.transform.position: transform.position, 1);
                tip2Obj.gameObject.SetActive(false);
                tip3Obj.gameObject.SetActive(true);
                msg.text = "You Haven't Found Enough Clues";
                Invoke(nameof(OneMoreChance), 2.5f);
            }
            else
            {
                AudioSource.PlayClipAtPoint(correctSound ,Camera.main != null ? Camera.main.transform.position: transform.position, 1);
                tip2Obj.gameObject.SetActive(false);
                tip3Obj.gameObject.SetActive(true);
                msg.text = "Great Detective! You Solved the Case.";
            }
        }
        else
        {
            AudioSource.PlayClipAtPoint(wrongSound ,Camera.main != null ? Camera.main.transform.position: transform.position, 1);
            tip2Obj.gameObject.SetActive(false);
            tip3Obj.gameObject.SetActive(true);
            msg.text = "You Might Have Miss a Clue Detective! You Lost.";
        }
    }

    private void OneMoreChance()
    {
        tip2Obj.gameObject.SetActive(true);
        tip3Obj.gameObject.SetActive(false);
        msg.text = "";
    }

    private IEnumerator WriteText(string text)
    {
        subtitlesTMP.text = "";
        foreach (var t in text)
        {
            subtitlesTMP.text += t;
            yield return new WaitForSeconds(0.03f); // Change the duration to set the speed of the typing effect
        }
    }

    public void Clear()
    {
        subtitlesTMP.text = "";
    }

    public void Menu()
    {
        instance = null;
        SceneManager.LoadScene("Menu");
    }
}