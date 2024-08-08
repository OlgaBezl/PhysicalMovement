using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero && _transform.forward != direction)
        {
            _transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(direction, Vector3.up).normalized);
        }
    }
}
