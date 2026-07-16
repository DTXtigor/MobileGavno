using UnityEngine;

public class InElevatorDetecter : MonoBehaviour
{
    private Elevator _Elevator;
    private void Start()
    {
        _Elevator = transform.parent.GetComponent<Elevator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) _Elevator._objects.Add(other.transform);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_Elevator._objects.Contains(other.transform) && other.gameObject.layer == LayerMask.NameToLayer("Player")) _Elevator._objects.Add(other.transform);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) _Elevator._objects.Remove(other.transform);
    }
}
