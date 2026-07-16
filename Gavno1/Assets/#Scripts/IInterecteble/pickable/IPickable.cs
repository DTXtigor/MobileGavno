using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class IPickable : ICore
{
    [SerializeField] public int id;
    [SerializeField] private Sprite icon;
    [SerializeField] private AudioClip clip;
    [SerializeField] private StartDialogueCore pickD;

    private AudioSource audioSource;
    private bool dPick = true;
    public PlayerPick pickable;
    [HideInInspector] public PlayerMove playerMove;
    [HideInInspector] public Vector3 backpack = new Vector3(0, -10, 10);

    [SerializeField] float localvolume = 1f;
    override public void Start()
    {
        base.Start();
        playerMove = FindAnyObjectByType<PlayerMove>();
        pickable = playerMove.GetComponent<PlayerPick>();      
        audioSource = GetComponent<AudioSource>();  
    }
    public override void PressButton()
    {
        for (int i = 0; i < pickable.slotItem.Length; i++) 
        {
            if (!pickable.slotItem[i])
            {
                pickable.slotItem[i] = gameObject;
                if (icon) pickable.slotImage[i].GetComponent<UnityEngine.UI.Image>().sprite = icon;
                transform.position = backpack;
                PlayerPrefs.SetInt("Inventory " + i, id);
                if (clip)
                {
                    audioSource.volume = localvolume * PlayerPrefs.GetFloat("SFX", 1);
                    audioSource.PlayOneShot(clip);
                }
                if (pickD && dPick)
                {
                    pickD.StartsDialogue(0);
                    dPick = false;
                }
                return;
            }
        }
    }

    virtual public void inInventory() { }
    virtual public void outInventory() { }

}
