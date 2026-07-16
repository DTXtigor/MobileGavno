using Unity.VisualScripting;
using UnityEngine;

public class Folder : ICore
{
    private PlayerPick playerPick;

    [SerializeField] private int idList;
    [SerializeField] private int MaxCountList = 10;
    private int currentCountList = 0;
    [SerializeField] private float offsetY = 0.1f;
    [SerializeField] private Transform startPos;

    [SerializeField] private GameObject winTable;
    [SerializeField] private GameObject[] otherWithWinTable;
    public bool win = false;

    override public void Start()
    {
        base.Start();
        playerPick = FindAnyObjectByType<PlayerPick>();

        win = intToBool(PlayerPrefs.GetInt("IsWin", 0));
        if (win)
        {
            winTable.SetActive(true);
            foreach (GameObject obj in otherWithWinTable) obj.SetActive(false);
        }
    }
    private bool intToBool(int value)
    {
        if (value == 0) return false;
        return true;
    }

    public override void PressButton()
    {
        for (int i = 0; i < playerPick.slotItem.Length; i++)
        {
            if (playerPick.slotItem[i] && playerPick.slotItem[i].GetComponent<PrinterList>())
            {
                if (playerPick.slotItem[i].GetComponent<PrinterList>().idList == idList)
                {
                    winTable.SetActive(true);
                    foreach (GameObject obj in otherWithWinTable) obj.SetActive(false);
                }
                playerPick.slotItem[i].transform.position = startPos.position + new Vector3(0, offsetY * currentCountList, 0);
                playerPick.slotItem[i].transform.rotation = startPos.rotation;
                playerPick.slotItem[i].transform.SetParent(startPos);
                currentCountList++;
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
