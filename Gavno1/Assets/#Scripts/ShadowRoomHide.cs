using System.Collections;
using UnityEngine;

public class ShadowRoomHide : MonoBehaviour
{
    private Vector3 place;

    [SerializeField] private CheckStatueDoor statue;
    [SerializeField] private Transform positionWall;
    [SerializeField] private Transform wall;
    [SerializeField] private DoorOpener staffOnlyDoor;
    [SerializeField] private GameObject shadowRoom;

    private void Start()
    {
        place = positionWall.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && statue.IsFinished)
        {
            staffOnlyDoor.ChangeState(false);
            StartCoroutine(timeForCloseDoor());
        }
    }

    private IEnumerator timeForCloseDoor()
    {
        yield return new WaitForSeconds(2f);
        shadowRoom.SetActive(false);
        wall.position = place;

    }
}
