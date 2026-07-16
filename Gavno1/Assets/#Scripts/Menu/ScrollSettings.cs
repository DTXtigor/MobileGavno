using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollSettings : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private float SpeedScroll;
    [SerializeField] private RectTransform _Scroll;
    [SerializeField] private float ScrollLimit = 20f;

    private float _InputRotation;
    private float ScrollValue;

    public void OnDrag(PointerEventData eventData)
    {
        _InputRotation = eventData.delta.y * SpeedScroll;

        ScrollValue = Mathf.Clamp(ScrollValue + _InputRotation, 0, ScrollLimit);
        _Scroll.anchoredPosition = new Vector2(_Scroll.anchoredPosition.x, ScrollValue);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        _InputRotation = 0;
    }
}
