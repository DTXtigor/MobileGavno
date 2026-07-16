using UnityEngine;
using System.Collections;

public class StartElevatorD : StartDialogueCore
{
    override public void Start()
    {
        base.Start();
        if (PlayerPrefs.GetInt("GameStage", 0) == 0)
        {
            StartCoroutine(Starts());   
        }
    }
    public override void StartsDialogue(int i)
    {
        _dialogue._currentLanguage = _currentLanguage[i].texts;
        _dialogue.StartDialogue();
        _dialogue._startDialogue = this;
    }

    IEnumerator Starts()
    {
        yield return new WaitForSeconds(0.5f);
        StartsDialogue(0);
    }
}
