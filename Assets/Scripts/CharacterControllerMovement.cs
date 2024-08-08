using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerMovement : MonoBehaviour
{
    [SerializeField] private KeyboardInput _input;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private MoveSettings _settings;

    private CharacterController _characterController;
    private Vector3 _verticalVelocity;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        _characterController.slopeLimit = _settings.SloapLimit;
        _characterController.stepOffset = _settings.MaxStepOffset;
    }

    private void OnValidate()
    {
        if (_input == null)
            throw new System.ArgumentNullException(nameof(_input));

        if (_rotator == null)
            throw new System.ArgumentNullException(nameof(_rotator));
    }

    private void FixedUpdate()
    {
        Move();        
    }

    private void Move()
    {
        Vector3 direction = _input.GetMoveDirection();

        if (_characterController.isGrounded)
        {
            _verticalVelocity = Vector3.down;
        }
        else
        {
            _verticalVelocity += Physics.gravity * Time.fixedDeltaTime;
        }

        _rotator.Rotate(direction);
        _characterController.Move((direction * _settings.PlayerSpeed + _verticalVelocity) * Time.fixedDeltaTime);
    }
}
