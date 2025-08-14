/*using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicJoystick : VirtualJoystick
{
    private Vector2 _defaultPosition;
    
    
    private void Start()
    {
        _defaultPosition = _joystickBackground.anchoredPosition;
    }
    
    public override void OnPointerDown(PointerEventData eventData)
    {
        _joystickBackground.anchoredPosition = eventData.position;
        base.OnPointerDown(eventData);
    }
    
    public override void OnPointerUp(PointerEventData eventData)
    {
        _joystickBackground.anchoredPosition = _defaultPosition;
        base.OnPointerUp(eventData);
    }
}*/