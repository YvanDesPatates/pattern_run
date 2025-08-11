using UnityEngine;

public class DoubleJumpStrategy: IMovementStrategy
{
    private const float JumpForce = 450f;
    private const float GravityModifier = 2f;

    private static readonly Vector3 InitialGravity = Physics.gravity;
    
    private bool _keyIsBeingPressed = false;
    private bool _secondJumpUsed = false;
    
    public DoubleJumpStrategy()
    {
        Physics.gravity = InitialGravity * GravityModifier;
    }

    public void OnActionKeyPressed(PlayerController player)
    {
        if (!player.IsOnGround && !_secondJumpUsed)
        {
            _secondJumpUsed = true;
            Jump(player);
        }
        
        _keyIsBeingPressed = true;
    }

    public void OnActionKeyReleased(PlayerController player)
    {
        _keyIsBeingPressed = false;
    }

    public void Update(PlayerController player)
    {
        //do not trigger the second jump here, or it will trigger instantly after the first jump
        if ( !_keyIsBeingPressed || ! player.IsOnGround) return;
        
        Jump(player);
        _secondJumpUsed = false;
    }
    
    public void ResetBeforeDestroy(PlayerController player)
    {
        Physics.gravity = InitialGravity;
    }
    
    private void Jump(PlayerController player)
    {
        player.IsOnGround = false;
        player.PlayerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        player.SetAnimationTrigger("Jump_trig");
        player.PlayJumpSound();
    }
    
}