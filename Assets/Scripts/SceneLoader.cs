using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private TMP_Text quoteTmp;
    [SerializeField] private TMP_Text authorTmp;
    [SerializeField] private Slider loadingSlider;
    public string[] quotes;

    [Header("Value")]
    [Range(0, 100)] [SerializeField] private float loadingOperationValue;
    [Range(0, 25)] [SerializeField] private int quoteIndex;
    
    private void OnValidate()
    {
        loadingSlider.value = loadingOperationValue;
        var quote = quotes[quoteIndex].Split('-');
        quoteTmp.text = quote[0];
        authorTmp.text = quote[1];
    }

    public async Task NextScene(string sceneName)
    {
        var operation = SceneManager.LoadSceneAsync(sceneName);
        var quote = quotes[Random.Range(0, quotes.Length)].Split('-');
        quoteTmp.text = quote[0];
        authorTmp.text = quote[1];
        while (!operation.isDone)
        {
            var progress = operation.progress / 0.9f * 100;
            loadingSlider.value = progress;
            await Task.Yield();
        }
    }  
}
