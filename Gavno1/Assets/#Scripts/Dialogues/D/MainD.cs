using UnityEngine;

public class MainD : StartDialogueCore
{
    [SerializeField] private int ToStage = 1;
    public override void OnEndDialogue()
    {
        if (PlayerPrefs.GetInt("GameStage", 0) < ToStage)FindAnyObjectByType<ScenLoader>().ChangeGameStage(true);
    }

    public override void StartsDialogue(int i)
    {
        _dialogue._currentLanguage = _currentLanguage[i].texts;
        _dialogue.StartDialogue();
        _dialogue._startDialogue = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (PlayerPrefs.GetInt("GameStage", 0) < ToStage)
            StartsDialogue(0);
        else if (_currentLanguage.Length > 1) StartsDialogue(Random.Range(1, _currentLanguage.Length));
    }
}
