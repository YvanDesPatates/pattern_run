using System.Collections;
using UnityEngine;

public class AdjsustableJumpStrategy: IMovementStrategy
{
    private bool _firstJumpHasBeenPerformed;
    private float _firstJumpForce = 400f;
    private float _adjsutableJumpForce = 150f;
    private float _delayBetweenAddingForceInS = 0.15f; 
    private const float GravityModifier = 2f;
    private Coroutine _jumpCoroutine;
    private static readonly Vector3 InitialGravity = Physics.gravity;
    
    public AdjsustableJumpStrategy()
    {
        Physics.gravity = InitialGravity * GravityModifier;
    }
    
    public void OnActionKeyPressed(PlayerController player)
    {
        if ( ! player.IsOnGround) return;
        
        player.IsOnGround = false;
        _jumpCoroutine = player.StartCoroutine(JumpCoroutine(player));
    }

    public void OnActionKeyReleased(PlayerController player)
    {
        if (_jumpCoroutine is null) return;
        
        player.StopCoroutine(_jumpCoroutine);
    }

    public void ResetBeforeDestroy(PlayerController player)
    {
        Physics.gravity = InitialGravity;
    }

    public void Update(PlayerController player){}
    
    private IEnumerator JumpCoroutine(PlayerController player)
    {
        player.PlayerRb.AddForce(Vector3.up * _firstJumpForce, ForceMode.Impulse);
            
        // adjustable jump
        bool playerIsGoingUpwards = true; //add force only until the player has reached the peak of the jump
        while ( ! player.IsOnGround && playerIsGoingUpwards)
        {
            player.PlayerRb.AddForce(Vector3.up * _adjsutableJumpForce, ForceMode.Impulse);
            yield return new WaitForSeconds(_delayBetweenAddingForceInS);
            playerIsGoingUpwards = player.PlayerRb.linearVelocity.y > 0f;
        }
    }
}