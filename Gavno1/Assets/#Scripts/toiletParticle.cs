using UnityEngine;
using UnityEngine.Audio;

public class toiletParticle : ICore
{
    public ParticleSystem particle;


    [SerializeField] private bool HasKey = false;
    [SerializeField] private Transform spawnPositionKey;
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private AudioClip soundPlunger;
    [SerializeField] private float localVolume = 1f;    
    public bool isUse = false;

    private Animator animator;
    private GameObject key;
    private PlayerPick playerPick;
    private AudioSource audioSource;

    public override void PressButton()
    {
        if (!isUse)
        {
            animator.SetTrigger("Plung");
            isUse = true;
        }
    }
    override public void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        playerPick = FindAnyObjectByType<PlayerPick>();
        audioSource = GetComponent<AudioSource>();
    }
    public void Part()
    {
        particle.Play();
        if (soundPlunger)
        {
            audioSource.volume = localVolume * PlayerPrefs.GetFloat("SFX", 1);
            audioSource.PlayOneShot(soundPlunger);
        }
    }

    public void GiveKey()
    {
        if (HasKey)
        {
            key = Instantiate(keyPrefab, spawnPositionKey.position, Quaternion.identity);
            key.GetComponent<Rigidbody>().AddForce(Vector3.up * 15f + Vector3.forward * 3, ForceMode.Impulse);
            key.GetComponent<Rigidbody>().AddTorque(Vector3.right * 0.1f, ForceMode.Impulse);
            particle.Play();
            playerPick.UseAndDestroyItem();
        }
    }

    public override bool CheckingState()
    {
        if (playerPick.currentSlot != -1 && playerPick.slotItem[playerPick.currentSlot].tag == "Plunger") return true;
        return false;
    }
}
