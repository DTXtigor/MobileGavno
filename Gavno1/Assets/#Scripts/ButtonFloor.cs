using UnityEngine;

public class ButtonFloor : MonoBehaviour
{
    private Animator animator;
    private Transform anchor;
    [HideInInspector] public bool isPressed = false;
    

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPressed = true;
            animator.SetBool("isPressed", true);
        }
    }
}
