using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    private readonly string _horizontalAxis = "Horizontal";
    private readonly string _verticalAxis = "Vertical";

    public Vector3 GetMoveDirection()
    {
        return new Vector3(Input.GetAxis(_horizontalAxis), 0f, Input.GetAxis(_verticalAxis)).normalized;
    }
}
