using UnityEngine;

public class PrinterList : IList
{
    private Printer printer;

    override  public void Start()
    {
        base.Start();
        printer = FindAnyObjectByType<Printer>();   
    }
    public override void PressButton()
    {
        base.PressButton();
        printer.newList = null;
    }

    public override bool CheckingState()
    {
        if (printer.isPrinting) return false;
        return true;
    }
}
