using UnityEngine;

public class JumpStrategy: IMovementStrategy
{
    private const float JumpForce = 650f;
    private const float GravityModifier = 2f;

    private static readonly Vector3 InitialGravity = Physics.gravity;
    
    public JumpStrategy()
    {
        Physics.gravity = InitialGravity * GravityModifier;
    }

    public void OnActionKeyPressed(PlayerController player)
    {
        if ( ! player.IsOnGround) return;
        
        player.PlayerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        player.SetAnimationTrigger("Jump_trig"); 
        player.PlayJumpSound();
    }

    public void OnActionKeyReleased(PlayerController player){}
    
    public void Update(PlayerController player){}
    
    public void ResetBeforeDestroy()
    {
        Physics.gravity = InitialGravity;
    }
    
}