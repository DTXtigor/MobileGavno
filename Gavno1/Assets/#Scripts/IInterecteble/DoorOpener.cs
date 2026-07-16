using System.Collections;
using UnityEngine;

public class DoorOpener : ICore
{
    private IPlayer iPlayer;
    private Animator animator;
    private PlayerPick playerPick;

    public bool IsLocked = false;
    public bool IsOpen = false;

    public int idKey = 0;
    public int id = 0;

    private AudioSource audioSource;
    [SerializeField] private AudioClip openCloseSound;
    [SerializeField] private AudioClip unlockedSound;

    [SerializeField] private float localVolume = 1f;

    [SerializeField] private StartDialogueCore lockedD;
    private bool dLocked = true;
    override public void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        IsLocked = intToBool(PlayerPrefs.GetInt("IsLockedDoor" + id, boolToInt(IsLocked)));
        IsOpen = intToBool(PlayerPrefs.GetInt("IsOpenDoor" + id, boolToInt(IsOpen)));
        animator.SetBool("Open", IsOpen);
        
        playerPick = FindAnyObjectByType<PlayerPick>();
        iPlayer = FindAnyObjectByType<IPlayer>();
        audioSource = GetComponent<AudioSource>();
    }

    private bool intToBool(int value)
    {
        if(value == 0) return false;
        return true;
    }

    private int boolToInt(bool value)
    {
        if (value) return 1;
        return 0;
    }

    override public void PressButton()
    {
        if (openCloseSound)
        {
            audioSource.volume = localVolume * PlayerPrefs.GetFloat("SFX", 1);
            audioSource.PlayOneShot(openCloseSound);         
        }

        if (IsLocked)
        {
            if (playerPick.currentSlot == -1)
            {
                iPlayer.Interect.GetComponent<Animator>().SetTrigger("Locked");
                if (lockedD && dLocked)
                {
                    lockedD.StartsDialogue(0);
                    dLocked = false;
                }

                return;
            }

            var key = playerPick.slotItem[playerPick.currentSlot];
            if (key && key.CompareTag("Key") && key.GetComponent<IPickable>().id == idKey)
            {
                IsLocked = false;
                IsOpen = !IsOpen;
                iPlayer.Interect.GetComponent<Animator>().SetBool("Open", IsOpen);
                FindAnyObjectByType<PlayerPick>().UseAndDestroyItem();
                audioSource.PlayOneShot(unlockedSound);
            }
            else
            {
                iPlayer.Interect.GetComponent<Animator>().SetTrigger("Locked");
            }
            return;
        }
            IsOpen = !IsOpen;
            iPlayer.Interect.GetComponent<Animator>().SetBool("Open", IsOpen);
    }

    public void ChangeState(bool State)
    {
        animator.SetBool("Open", State);
        IsOpen = State;
    }
}
