using UnityEngine;

public class IList : IPickable
{
    private Animator animatorPlayer;
    public Transform finger;
    public string nameTargetInHand;
    public int idList;

    override public void Start()
    {
        base.Start();
        playerMove = FindAnyObjectByType<PlayerMove>();
        animatorPlayer = FindAnyObjectByType<PlayerMove>().GetComponentInChildren<Animator>();
        finger = GameObject.Find("mixamorig:RightHandMiddle1").transform; 
    }

    public override void PressButton()
    {
        base.PressButton();
        foreach (Transform t in GetComponentsInChildren<Transform>()) t.gameObject.layer = LayerMask.NameToLayer("Default");

    }
    
    override public void inInventory()
    {
        animatorPlayer.SetTrigger("ListIn");
    }
    public override void outInventory() 
    {
        animatorPlayer.SetTrigger("ListOut");
    }
}
