using UnityEngine;

public class ReturnRoom : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindAnyObjectByType<StaffOnly>().Return();
        }
    }
}
