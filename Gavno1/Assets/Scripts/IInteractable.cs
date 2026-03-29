using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class IInteractable : MonoBehaviour
{
    [HideInInspector] public bool IsActive = true;
    [SerializeField] private GameObject _button;
    [SerializeField] private GameObject[] _hiddes;
    [SerializeField] private GameObject[] _nededs;

    private GameObject Button;
    public virtual void PressButton() { }
    public void ShowButton() { Button?.SetActive(true); }
    public void HideButton() { Button?.SetActive(false); }
    public void SwitchAllOther(bool state)
    {
        foreach (var item in _hiddes)
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

