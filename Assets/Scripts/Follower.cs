using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform _target; 
    [SerializeField] private RigidbodyMover _mover;
    [SerializeField] private float _offset = 3f;

    private void OnValidate()
    {
        if( _target == null)
            throw new System.ArgumentNullException(nameof(_target));

        if (_mover == null)
            throw new System.ArgumentNullException(nameof(_mover));

        if (_offset <= 0)
            throw new System.ArgumentOutOfRangeException(nameof(_offset));
    }

    private void FixedUpdate()
    {
        Vector3 direction = _target.transform.position - transform.position;

        if (direction.magnitude > _offset)
            _mover.Move(direction.normalized);
    }
}
