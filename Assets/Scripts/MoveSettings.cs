using UnityEngine;

public class MoveSettings : MonoBehaviour
{
    [field: SerializeField] public float SloapLimit { get; private set; } = 35f;
    [field: SerializeField] public float MaxStepOffset { get; private set; } = 0.6f;
    [field: SerializeField] public float PlayerSpeed { get; private set; } = 3f;
    [field: SerializeField] public float FollowerSpeed { get; private set; } = 2f;

    private void OnValidate()
    {
        if (SloapLimit <= 0)
            throw new System.ArgumentOutOfRangeException(nameof(SloapLimit));

        if (MaxStepOffset <= 0)
            throw new System.ArgumentOutOfRangeException(nameof(MaxStepOffset));

        if (PlayerSpeed <= 0)
            throw new System.ArgumentOutOfRangeException(nameof(PlayerSpeed));

        if (FollowerSpeed <= 0 || PlayerSpeed <= FollowerSpeed)
            throw new System.ArgumentOutOfRangeException(nameof(FollowerSpeed));
    }
}
