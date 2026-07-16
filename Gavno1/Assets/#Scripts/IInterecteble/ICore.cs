using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ICore : MonoBehaviour
{
    [HideInInspector] public bool IsActive = true;

    [HideInInspector] public string ButtonText;

    [SerializeField] private string _english;
    [SerializeField] private string _spanish;
    [SerializeField] private string _russian;
    public virtual void PressButton() { }
    virtual public void Start()
    {
        Swap();
        FindAnyObjectByType<ScenLoader>().Swap += Swap;
    }

    public void Swap()
    {
        if (PlayerPrefs.GetInt("Language", 0) == 0)
        {
            ButtonText = _english;
        }
        else if (PlayerPrefs.GetInt("Language", 0) == 1)
        {
            ButtonText = _spanish;
        }
        else if (PlayerPrefs.GetInt("Language", 0) == 2)
        {
            ButtonText = _russian;
        }
    }

    virtual public bool CheckingState()
    {
        return true;
    }
}

