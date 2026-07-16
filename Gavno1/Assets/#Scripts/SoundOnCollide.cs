using UnityEngine;

public class SoundOnCollide : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private float localVolume = 1f;    
    [SerializeField] private AudioClip[] clip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnCollisionEnter(Collision hit)
    {
        audioSource.volume = localVolume * PlayerPrefs.GetFloat("SFX", 1);
        audioSource.PlayOneShot(clip[Random.Range(0, clip.Length)]);      
    } 
}

