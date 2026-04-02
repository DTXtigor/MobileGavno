using UnityEngine;
using UnityEngine.EventSystems;

public class Rotation : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private float SpeedRotation;

    [SerializeField] private Vector3 _InputRotation;
    private float cameraPitch = 0.0f;
    private Transform _Camera;
    private Transform _player;

    private void Start()
    {
        _Camera = Camera.main.transform;
        _player = FindAnyObjectByType<PlayerMove>().transform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _InputRotation = new Vector3(eventData.delta.x, eventData.delta.y, 0) * SpeedRotation;

        cameraPitch = Mathf.Clamp(cameraPitch - _InputRotation.y, -90f, 90f);
        _Camera.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
        _player.Rotate(Vector3.up * _InputRotation.x);      
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        _InputRotation = Vector3.zero;
    }
}

