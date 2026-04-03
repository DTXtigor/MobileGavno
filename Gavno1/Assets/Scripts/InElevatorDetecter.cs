using UnityEngine;

public class InElevatorDetecter : MonoBehaviour
{
    private Elevator _Elevator;
    [SerializeField]private LayerMask _ForElevator;
    private void Start()
    {
        _Elevator = transform.parent.GetComponent<Elevator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        _Elevator._objects.Add(other.transform);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_Elevator._objects.Contains(other.transform)) _Elevator._objects.Add(other.transform);
    }
    private void OnTriggerExit(Collider other)
    {
        _Elevator._objects.Remove(other.transform);
    }
}
