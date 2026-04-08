using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    private Dialogue _dialogue;
    [SerializeField] private string[] _russian;
    [SerializeField] private string[] _english;
    private string[] _currentLanguage;
    private void Start()
    {
        _dialogue = FindAnyObjectByType<Dialogue>();
        FindAnyObjectByType<ScenLoader>().Swap += Swap;
        Swap();
    }

    public void Swap()
    {
        PlayerPrefs.GetInt("Language", 0);
         if (PlayerPrefs.GetInt("Language", 0) == 0)
        {
            _currentLanguage = _english;
        }
        else
        {
            _currentLanguage = _russian;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _dialogue._currentLanguage = _currentLanguage;
            _dialogue.StartDialogue();
        }
    }
}
