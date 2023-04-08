using UnityEngine;

public class SuspectCh1 : MonoBehaviour
{
    [SerializeField] private GameObject[] neckPart;
    [SerializeField] private GameObject[] handsPart;
    [SerializeField] private GameObject[] shoesPart;

    private void Awake()
    {
        foreach (var o in neckPart)
        {
            o.SetActive(false);
        }
        foreach (var o in handsPart)
        {
            o.SetActive(false);
        }
        foreach (var o in shoesPart)
        {
            o.SetActive(false);
        }
    }

    private void Start()
    {
        neckPart[Random.Range(0,  neckPart.Length)].SetActive(true);
        handsPart[Random.Range(0,  neckPart.Length)].SetActive(true);
        shoesPart[Random.Range(0,  neckPart.Length)].SetActive(true);
    }
}
