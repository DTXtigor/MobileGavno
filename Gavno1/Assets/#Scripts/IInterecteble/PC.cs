using UnityEngine;
public class PC : ICore
{
    const int PassCount = 8;
    [SerializeField] private bool[] Pass = new bool[PassCount];
    [SerializeField] private EPanel[] Switchers;

    private bool isOn = false;
    private bool canInteract = false;

    [SerializeField] private bool chekPass = false;

    [SerializeField] private StartDialogueCore turnOffD;
    [SerializeField] private StartDialogueCore turnOnD;

    private bool tOn = true;
    private bool tOff = true;
    override public void PressButton()
    {
        EnergyPC();
        if (canInteract) 
        { 
            isOn = !isOn; 
            if (tOn) 
            { 
                turnOnD.StartsDialogue(0); 
                tOn = false; 
            } 
        }
        else if (tOff)
        {
            turnOffD.StartsDialogue(0);
            tOff =false;
        }
            FindAnyObjectByType<Monitor>().TurningOnOff(isOn);
    }

    public void EnergyPC()
    {
        if (chekPass) canInteract = CheckPass();
        else canInteract = true;
    }
    private bool CheckPass()
    {
        for (int i = 0; i < PassCount; i++)
        {
            if (Switchers[i]._isOn != Pass[i]) return false;
        }
        return true;
    }
    public override void Start()
    {
        base.Start();      
    }
}
