using UnityEngine;

public class FloorPanel : MonoBehaviour
{
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material unactiveMaterial;

    [SerializeField] private MeshRenderer[] allPiece;

    [SerializeField] private MeshRenderer[] Floor0;
    [SerializeField] private MeshRenderer[] Floor1;
    [SerializeField] private MeshRenderer[] Floor2;
    [SerializeField] private MeshRenderer[] Floor3;

    private MeshRenderer[] Target;
    public void ChangeFloor(int floor)
    {
        foreach (var piece in allPiece) { piece.material = unactiveMaterial; }
        switch (floor)
        {
            case 0:
                Target = Floor0; break;
            case 1:
                Target = Floor1; break;
            case 2:
                Target = Floor2; break;
            case 3:
                Target = Floor3; break;
            default:
                Target = null; break;
        }
        if (Target != null) foreach (MeshRenderer piece in Target) { piece.material = activeMaterial; }
    }
}
