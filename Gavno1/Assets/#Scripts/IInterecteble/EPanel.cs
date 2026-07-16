using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class EPanel : MonoBehaviour
{
    [SerializeField] private Renderer LightSwitch;
    [SerializeField] private Material On, Off;
    [SerializeField] private EPanel[] Switches;
    [SerializeField] private EPanel main;

    public int id;

    public bool _isOn = false;
    private StaffOnly _staffOnly;
    private Animator _animation;
    private void Awake()
    {
        _staffOnly = FindAnyObjectByType<StaffOnly>();
        _animation = GetComponentInChildren<Animator>();
        _isOn = intToBool(PlayerPrefs.GetInt("StateSwitchers" + id, boolToInt(_isOn)));
        if (_isOn)
        {
            LightSwitch.material = On;
            _animation.SetBool("_isOn", true);
        }
        else
        {
            LightSwitch.material = Off;
            _animation.SetBool("_isOn", false);
        }
    }

    private bool intToBool(int value)
    {
        if (value == 0) return false;
        return true;
    }

    private int boolToInt(bool value)
    {
        if (value) return 1;
        return 0;
    }

    public void ResetAll()
    {
        foreach(var item in Switches)
        {
            item._isOn = false;
            item.LightSwitch.material = Off;
            item._animation.SetBool("_isOn", false);
        }
        ChangeState(true);
        _staffOnly.CheckPass();
    }
    public void InterectAll()
    {

        Interect();
        main.ChangeState(false);
        foreach (var item in Switches)
        {
            item.Interect();
        }
        _staffOnly.CheckPass();
    }
    public void Interect()
    {
        _isOn = !_isOn;
        if (_isOn)
        {
            LightSwitch.material = On;
            _animation.SetBool("_isOn", true);
        }
        else
        {
            LightSwitch.material = Off;
            _animation.SetBool("_isOn", false);
        }
    }

    public void ChangeState(bool state)
    {
        if (state)
        {
            LightSwitch.material = On;
            _animation.SetBool("_isOn", true);
        }
        else
        {
            LightSwitch.material = Off;
            _animation.SetBool("_isOn", false);
        }
    }
}
