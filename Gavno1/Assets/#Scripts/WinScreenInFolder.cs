using UnityEngine;

public class WinScreenInFolder : MonoBehaviour
{
    private Folder f;

    private void Start()
    {
        f = FindAnyObjectByType<Folder>();
    }
}
