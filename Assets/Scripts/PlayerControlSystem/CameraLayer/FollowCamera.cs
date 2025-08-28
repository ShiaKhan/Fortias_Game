using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public static FollowCamera instance;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector2 _offset = new Vector2(0, 0);
    [SerializeField] private float _smoothTime = 0.3f;

    private Vector2 _velocity = Vector2.zero;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        if (_target == null) return;

        Vector2 targetPosition = (Vector2)_target.position + _offset;
        Vector2 smoothPosition = Vector2.SmoothDamp(
            (Vector2)transform.position,
            targetPosition,
            ref _velocity,
            _smoothTime
        );

        // Giữ nguyên trục Z của camera (thường là -10)
        transform.position = new Vector3(smoothPosition.x, smoothPosition.y, transform.position.z);
    }
    public void SetTarget(Transform target)
    {
        this._target = target;
    }
}

