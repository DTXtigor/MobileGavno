using UnityEngine;

public class EPanelSheet : ICorePanel
{
    [SerializeField] private Transform ListPos;
    private PlayerPick playerPick;

    public override void Start()
    {
        base.Start();
        playerPick = FindAnyObjectByType<PlayerPick>();
    }

    public override void PressButton()
    {
        base.PressButton();
        if (playerPick.currentSlot != -1 && playerPick.slotItem[playerPick.currentSlot].GetComponent<IList>() && playerPick.slotItem[playerPick.currentSlot].GetComponent<IList>().id == 15)
        {
            Transform list = playerPick.slotItem[playerPick.currentSlot].GetComponent<IList>().transform;

            list.position = ListPos.position;
            list.SetParent(transform);

            playerPick.CleanItem(playerPick.currentSlot);
        }
    }
}
