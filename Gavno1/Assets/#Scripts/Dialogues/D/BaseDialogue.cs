using UnityEngine;
using System.Collections;

public class BaseDialogue : StartDialogueCore
{
    override public void Start()
    {
        base.Start();
        if (PlayerPrefs.GetInt("GameStage", 0) == 0)
        {
            StartCoroutine(Starts());   
        }
    }

    IEnumerator Starts()
    {
        yield return new WaitForSeconds(0.5f);
        StartsDialogue(0);
    }
}
