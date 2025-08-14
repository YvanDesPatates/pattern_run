using UnityEngine;

public class JetpackStrategy: IMovementStrategy
{
    private float _upwardForce = 660f;
    private float _gravityModifier = 2f;
    private bool _keyIsPressed;
    private bool _jetpackIsActive;
    
    private static readonly Vector3 InitialGravity = Physics.gravity;
    
    public JetpackStrategy()
    {
        Physics.gravity *= _gravityModifier;
    }
    
    public void OnActionKeyPressed(PlayerController player)
    {
        _keyIsPressed = true;
    }

    public void OnActionKeyReleased(PlayerController player)
    {
        _keyIsPressed = false;
    }

    public void ResetBeforeDestroy(PlayerController player)
    {
        Physics.gravity = InitialGravity;
        player.SetJetpackVisibility(false);
    }

    public void Update(PlayerController player)
    {
        if (_keyIsPressed)
        {
            player.PlayerRb.AddForce(Vector3.up*_upwardForce);
        }
        
        if (_jetpackIsActive) return;
        _jetpackIsActive = true;
        player.SetJetpackVisibility(true);
    }
}