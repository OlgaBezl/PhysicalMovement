using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(CapsuleCollider))]
public class RigidbodyMover : MonoBehaviour
{
    [SerializeField] private Rotator _rotator;
    [SerializeField] private float _stepFactor = 5f;
    [SerializeField] private Transform _bottomPoint;
    [SerializeField] private Transform _upperForwarPoint;
    [SerializeField] private MoveSettings _settings;
        
    private Rigidbody _rigidbody;
    private float _colliderHeight;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _colliderHeight = GetComponent<CapsuleCollider>().height;
    }

    private void OnValidate()
    {
        if (_settings == null)
            throw new System.ArgumentNullException(nameof(_settings));

        if (_rotator == null)
            throw new System.ArgumentNullException(nameof(_rotator));

        if (_bottomPoint == null)
            throw new System.ArgumentNullException(nameof(_bottomPoint));

        if (_upperForwarPoint == null)
            throw new System.ArgumentNullException(nameof(_upperForwarPoint));

        if (_stepFactor < 1)
            throw new System.ArgumentOutOfRangeException(nameof(_stepFactor));
    }

    public void Move(Vector3 direction)
    {
        if (direction == Vector3.zero)
            return;

        _rotator.Rotate(direction);

        if (Physics.Raycast(_upperForwarPoint.position, Vector3.down, out RaycastHit upperRaycastHit, _colliderHeight))
        {
            if (upperRaycastHit.point.y > _bottomPoint.position.y)
            {
                float slopeAngle = Vector3.Angle(Vector3.up, upperRaycastHit.normal);

                if (slopeAngle > 0)
                {
                    if (slopeAngle <= _settings.SloapLimit)
                    {
                        Vector3 upperPoint = new Vector3(upperRaycastHit.point.x, upperRaycastHit.point.y + _colliderHeight / 2f, upperRaycastHit.point.z);
                        direction = (upperPoint - _rigidbody.position).normalized;
                    }
                    else
                    {
                        direction = Vector3.zero;
                    }
                }
                else
                {
                    float stepHeight = upperRaycastHit.point.y - _bottomPoint.position.y;

                    if (stepHeight < _settings.MaxStepOffset)
                    {
                        Vector3 upperPoint = new Vector3(_rigidbody.position.x, upperRaycastHit.point.y + _colliderHeight / 2f, _rigidbody.position.z);
                        Vector3 stepDirection = upperPoint - _rigidbody.position;
                        _rigidbody.AddForce(stepDirection.normalized * _stepFactor * _settings.FollowerSpeed, ForceMode.Force);
                        direction = stepDirection;
                    }
                }
            }
        }

        var _forwardSpeed = direction * _settings.FollowerSpeed;
        _rigidbody.velocity = new Vector3(_forwardSpeed.x, _rigidbody.velocity.y, _forwardSpeed.z);
    }
}
