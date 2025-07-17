using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] public RectTransform _joystickBackground;
    [SerializeField] private RectTransform _joystickHandle;
    [SerializeField] private float _handleRange = 1f;
    [SerializeField] private float _deadZone = 0.2f;

    public Vector2 Direction
    {
        get
        {
            if (_inputVector.magnitude < _deadZone) return Vector2.zero;
            return _inputVector;
        }
        
    }

    private Vector2 _inputVector = Vector2.zero;
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - (Vector2)_joystickBackground.position;
        _inputVector = (direction.magnitude > _joystickBackground.sizeDelta.x / 2f) 
            ? direction.normalized 
            : direction / (_joystickBackground.sizeDelta.x / 2f);
        
        _joystickHandle.anchoredPosition = _inputVector * _joystickBackground.sizeDelta.x / 2f * _handleRange;
    }
    
    public virtual void OnPointerDown(PointerEventData eventData) => OnDrag(eventData);
    
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        _inputVector = Vector2.zero;
        _joystickHandle.anchoredPosition = Vector2.zero;
    }
    
    //public Vector2 Direction => new Vector2(_inputVector.x, _inputVector.y);
}