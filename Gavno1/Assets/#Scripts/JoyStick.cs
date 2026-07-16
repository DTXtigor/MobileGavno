using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class JoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image _Background;
    [SerializeField] private Image _Joystick;
    [SerializeField] private Image _Area;

    private Vector2 _BackGroundStartPos;
    public Vector2 _InputVector;

    private bool _IsPressed;

    [SerializeField] private Color _PressedColor;
    [SerializeField] private Color _NormalColor;
    private void Start()
    {
        ClickEffect();
        _BackGroundStartPos = _Background.rectTransform.anchoredPosition;
    }
    private void ClickEffect()
    {
        _IsPressed = !_IsPressed;
        _Joystick.color = _IsPressed ? _PressedColor : _NormalColor;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_Area.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / _Area.rectTransform.sizeDelta.x);
            pos.y = (pos.y / _Area.rectTransform.sizeDelta.y);

            _InputVector = new Vector2(pos.x * 2, pos.y * 2);

            _InputVector = (_InputVector.magnitude > 1.0f) ? _InputVector.normalized : _InputVector;
            
            _Joystick.rectTransform.anchoredPosition = new Vector2(_InputVector.x * (_Area.rectTransform.sizeDelta.x / 3), _InputVector.y * (_Area.rectTransform.sizeDelta.y / 3));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ClickEffect();
        Vector2 pos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_Area.rectTransform, eventData.position, null, out pos))
        {
            _Background.rectTransform.anchoredPosition = pos;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _Background.rectTransform.anchoredPosition = _BackGroundStartPos;
        ClickEffect();
        _InputVector = Vector2.zero;
        _Joystick.rectTransform.anchoredPosition = Vector2.zero;
    }
}
