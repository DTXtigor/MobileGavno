using UnityEngine;

public class IInteractable : MonoBehaviour
{
    [HideInInspector] public bool IsActive = true;
    [SerializeField] private GameObject _button;
    [SerializeField] private GameObject[] _hiddes;
    [SerializeField] private GameObject[] _nededs;
    public virtual void PressButton() { }
    public void ShowButton() { _button?.SetActive(true); }
    public void HideButton() { _button?.SetActive(false); }
    public void SwitchAllOther(bool state)
    {
        foreach (var item in _hiddes)
        {
            item.SetActive(state);
        }
        foreach (var item in FindAnyObjectByType<PlayerMove>().UIPlayer)
        {
            item.SetActive(state);
        }
    }
    public void SwitchAllNeeded(bool state)
    {
        foreach (var item in _nededs)
        {
            item.SetActive(state);
        }
    }
    private void Start()
    {
        _button.SetActive(false);
    }
}

