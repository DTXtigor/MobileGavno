using UnityEngine;

public class LanguageSwap : MonoBehaviour
{
    [SerializeField] private string _english;
    private string _spanish;
    [SerializeField] private string _russian;

    private void Start()
    {
        Swap();
        FindAnyObjectByType<ScenLoader>().Swap += Swap;
    }

    public void Swap()
    {
        if (PlayerPrefs.GetInt("Language", 0) == 0)
        {
            GetComponent<TMPro.TMP_Text>().text = _english;
        }
        else if (PlayerPrefs.GetInt("Language", 0) == 1)
        {
            GetComponent<TMPro.TMP_Text>().text = _spanish;
        }
        else if (PlayerPrefs.GetInt("Language", 0) == 2)
        {
            GetComponent<TMPro.TMP_Text>().text = _russian;
        }
    }
}
