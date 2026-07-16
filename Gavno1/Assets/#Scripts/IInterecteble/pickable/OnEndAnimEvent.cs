using UnityEngine;

public class OnEndAnimEvent : MonoBehaviour
{
    private PlayerMove playerMove;
    private PlayerPick playerPick;

    private int currentItemIdInHand;
    private void Start()
    {
        playerMove = GetComponentInParent<PlayerMove>();
        playerPick = GetComponentInParent<PlayerPick>();    
    }

    public void onEndAnimation()
    {
        playerMove._InInterface = false;
        playerMove.BackToBody();

        playerPick.slotItem[currentItemIdInHand].transform.SetParent(null);

        playerPick.slotItem[currentItemIdInHand].transform.position = playerPick.slotItem[currentItemIdInHand].GetComponent<IList>().backpack;
        playerPick.slotItem[currentItemIdInHand].transform.rotation = Quaternion.identity;

        playerPick.slotImage[currentItemIdInHand].color = playerPick.defaultColor;
    }

    public void toStartAnimation()
    {
        playerMove._InInterface = true;
        playerPick.rotatingCamera = true;

        if (playerPick.slotItem[playerPick.currentSlot].GetComponent<IList>().nameTargetInHand != "")
        {
            Transform positionInHand = GameObject.Find(playerPick.slotItem[playerPick.currentSlot].GetComponent<IList>().nameTargetInHand).transform;
            playerPick.slotItem[playerPick.currentSlot].transform.position = positionInHand.position;
            playerPick.slotItem[playerPick.currentSlot].transform.rotation = positionInHand.rotation;
        }
        else
        {
            playerPick.slotItem[playerPick.currentSlot].transform.position = playerPick.slotItem[playerPick.currentSlot].GetComponent<IList>().finger.position;
            playerPick.slotItem[playerPick.currentSlot].transform.rotation = playerPick.slotItem[playerPick.currentSlot].GetComponent<IList>().finger.rotation;
        }

        currentItemIdInHand = playerPick.currentSlot;
        playerPick.slotItem[playerPick.currentSlot].transform.SetParent(playerPick.slotItem[playerPick.currentSlot].GetComponent<IList>().finger);

    }
}
