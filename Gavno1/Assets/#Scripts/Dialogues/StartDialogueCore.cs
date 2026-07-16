using System.Collections;
using UnityEngine;

public class StartDialogueCore : MonoBehaviour
{
    [HideInInspector] public Dialogue _dialogue;
    public RowData[] _russian;
    public RowData[] _english;
    [HideInInspector] public RowData[] _currentLanguage;
    [SerializeField] private bool ChangeGameStageOnEndDialogue;
    virtual public void Start()
    {
        _dialogue = FindAnyObjectByType<Dialogue>();
        FindAnyObjectByType<ScenLoader>().Swap += Swap;
        Swap();
    }

    public virtual void Swap()
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

    public virtual void StartsDialogue(int i)
    {
        _dialogue._currentLanguage = _currentLanguage[i].texts;
        _dialogue.StartDialogue();
        _dialogue._startDialogue = this;
    }

    public virtual void OnEndDialogue()
    {
        if (ChangeGameStageOnEndDialogue) FindAnyObjectByType<ScenLoader>().ChangeGameStage(true);
    }
}
