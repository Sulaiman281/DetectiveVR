using UnityEngine;
using Random = UnityEngine.Random;

public class CluesOnBody : MonoBehaviour
{
    [SerializeField] private ClueObject[] clues;
    [SerializeField] private int maxCluesShow = 3;

    private void OnValidate()
    {
        clues = transform.GetComponentsInChildren<ClueObject>(true);
        foreach (var clue in clues)
        {
            clue.gameObject.SetActive(false);
        }
    }

    public void ShowClues(int count = 0)
    {
        foreach (var t in clues)
        {
            if (Random.Range(0, 2) != 1) continue;
            t.gameObject.SetActive(true);
            if (count >= maxCluesShow) break;
            count++;
        }

        if(count < maxCluesShow) ShowClues(count);
    }
}
