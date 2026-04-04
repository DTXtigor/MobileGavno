using System.Collections.Generic;
using UnityEngine;

public class LightSwitcher : IInteractable
{
    [SerializeField] private GameObject[] Torches;
    private List<Light> _lights = new List<Light>();
    private void Start()
    {
        foreach (var item in Torches) 
        { 
            _lights.Add(item.GetComponentInChildren<Light>());
            item.GetComponentInChildren<Light>().enabled = false;
        }
    }
    override public void PressButton()
    {
        Debug.Log("Pressed");
        foreach (var item in _lights)
        {
            item.enabled = !item.enabled;
        }
    }
}
