using UnityEngine;

public class EPanel : MonoBehaviour
{
    [SerializeField] private Renderer LightSwitch;
    [SerializeField] private Material On, Off;
    [SerializeField] private bool _isOn = false;
    [SerializeField] private EPanel[] Switches;

    private Animator _animation;
    private void Start()
    {
        _animation = GetComponentInChildren<Animator>();
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

    public void ResetAll()
    {
        foreach(var item in Switches)
        {
            item._isOn = false;
            item.LightSwitch.material = Off;
            item._animation.SetBool("_isOn", false);
        }
    }
    public void InterectAll()
    {
        Interect();
        foreach (var item in Switches)
        {
            item.Interect();
        }
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
}
