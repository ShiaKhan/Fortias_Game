using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJoystick : MonoBehaviour
{
    [SerializeField] public RectTransform _joystickBackground;
    [SerializeField] private RectTransform _joystickHandle;
    private Vector2 _joystickOriginalPosition;
    public Vector2 _joystickVec;
    private Vector2 _joystickTouchPos;

    public float _joystickRadius;
    // Start is called before the first frame update
    void Start()
    {
        _joystickOriginalPosition = _joystickHandle.position;
        _joystickRadius = _joystickBackground.sizeDelta.y/4;
    }

    public void PointerDown()
    {
        _joystickHandle.position = Input.mousePosition;
        _joystickBackground.position = Input.mousePosition;
        _joystickTouchPos = Input.mousePosition;
    }

    public void Drag(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        _joystickVec = (dragPos - _joystickTouchPos).normalized;
        float joystickDist = Vector2.Distance(dragPos, _joystickTouchPos);
        if (joystickDist >= _joystickRadius)
        {
            _joystickHandle.position = _joystickTouchPos + _joystickVec * _joystickRadius;
        }
        else
        {
            _joystickHandle.position = _joystickTouchPos + _joystickVec * joystickDist;
        }
    }

    public void PointerUp()
    {
        _joystickVec = Vector2.zero;
        _joystickHandle.position = _joystickOriginalPosition;
        _joystickBackground.position = _joystickOriginalPosition;
    }
}
