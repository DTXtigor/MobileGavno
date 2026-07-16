using UnityEngine;

public class TrashList : ICore
{
    private PlayerPick playerPick;

    override public void Start()
    {
        base.Start();
        playerPick = FindAnyObjectByType<PlayerPick>();
    }

    public override void PressButton()
    {
        for (int i = 0; i < playerPick.slotItem.Length; i++)
        {
            if (playerPick.slotItem[i] && playerPick.slotItem[i].GetComponent<PrinterList>())
            {
                Destroy(playerPick.slotItem[i]);
                playerPick.CleanItem(i);
            }
        }
    }
    public override bool CheckingState()
    {
        foreach (GameObject g in playerPick.slotItem) if (g && g.GetComponent<PrinterList>()) return true;
        return false;
    }
}
