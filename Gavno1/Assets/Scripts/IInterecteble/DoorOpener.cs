using UnityEngine;

public class DoorOpener : IInteractable
{
    private Interectively _interectively;
    public bool IsLocked = false;
    public bool IsOpen = false;
    private void Start()
    {
        _interectively = FindAnyObjectByType<Interectively>();
    }
    override public void PressButton()
    {
        if (IsLocked)
        {
            _interectively.Interect.GetComponent<Animator>().SetTrigger("Locked");
            return;
        }
        IsOpen = !IsOpen;
        _interectively.Interect.GetComponent<Animator>().SetBool("Open", IsOpen);
    }
}
