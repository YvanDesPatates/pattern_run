using UnityEngine;

public class GravityInvertionStrategy : IMovementStrategy
{
    private Vector3 _initialGravity;
    private float _lastSpeedValue = -1f;

    public GravityInvertionStrategy()
    {
        _initialGravity = Physics.gravity;
    }

    public void OnActionKeyPressed(PlayerController player)
    {
        Physics.gravity = -Physics.gravity;
    }

    public void OnActionKeyReleased(PlayerController player)
    {
    }

    public void ResetBeforeDestroy(PlayerController player)
    {
        Physics.gravity = _initialGravity;
        player.SetAnnimationFloat("Speed_f", 1);
    }

    public void Update(PlayerController player)
    {
        float speedValue = player.IsOnGround ? 1f : 0f;
        SetSpeedAnimation(player, speedValue);
    }

    private void SetSpeedAnimation(PlayerController player, float value)
    {
        if (_lastSpeedValue != value)
        {
            player.SetAnnimationFloat("Speed_f", value);
            _lastSpeedValue = value;
        }
    }
}