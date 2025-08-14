using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0, 0, -8);
    [SerializeField] private float _smoothTime = 0.3f;

    private Vector3 _velocity = Vector3.zero;

    private void LateUpdate()
    {
        Vector3 targetPosition = _target.position + _offset;
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref _velocity,
            _smoothTime
        );

        transform.LookAt(_target);
    }
    public void SetTarget(Transform target)
    {
        this._target = target;
    }
}

