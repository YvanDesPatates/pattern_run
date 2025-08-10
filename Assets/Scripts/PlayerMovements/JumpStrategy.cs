using UnityEngine;

public class JumpStrategy: IMovementStrategy
{
    private const float JumpForce = 650f;
    private const float GravityModifier = 2f;

    private static readonly Vector3 InitialGravity = Physics.gravity;
    
    private bool _keyIsBeingPressed = false;
    
    public JumpStrategy()
    {
        Physics.gravity = InitialGravity * GravityModifier;
    }

    public void OnActionKeyPressed(PlayerController player)
    {
        _keyIsBeingPressed = true;
    }

    public void OnActionKeyReleased(PlayerController player)
    {
        _keyIsBeingPressed = false;
    }

    public void Update(PlayerController player)
    {
        if ( !_keyIsBeingPressed || ! player.IsOnGround) return;
        
        player.IsOnGround = false;
        player.PlayerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        player.SetAnimationTrigger("Jump_trig"); 
        player.PlayJumpSound();
    }
    
    public void ResetBeforeDestroy(PlayerController player)
    {
        Physics.gravity = InitialGravity;
    }
    
}