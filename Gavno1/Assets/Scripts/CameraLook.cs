using Unity.ProjectAuditor.Editor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Rotation : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Vector3 _InputRotation;
    [SerializeField] private Image _Area;
    [SerializeField] private float SpeedRotation;

    private Transform _Camera;
    [HideInInspector] public bool CanMoveCamera = true;

    private void Start()
    {
        _Camera = Camera.main.transform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (!CanMoveCamera) return;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_Area.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / _Area.rectTransform.sizeDelta.x);
            pos.y = (pos.y / _Area.rectTransform.sizeDelta.y);

            _InputRotation = new Vector2(pos.x, pos.y);

            _InputRotation = (_InputRotation.magnitude > 1.0f) ? _InputRotation.normalized : _InputRotation;
            _Camera.transform.Rotate(Vector3.up, _InputRotation.x * SpeedRotation * Time.deltaTime, Space.World);
            _Camera.transform.Rotate(Vector3.right, -_InputRotation.y * SpeedRotation * Time.deltaTime, Space.Self);
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        _InputRotation = Vector3.zero;
    }
}

