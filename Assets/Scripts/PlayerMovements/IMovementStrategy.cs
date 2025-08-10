public interface IMovementStrategy
{
    /// <summary>
    /// called by PlayerController when the action key is pressed. Should implement the specific movement behavior.
    /// </summary>
    public void OnActionKeyPressed(PlayerController player);
    /// <summary>
    /// called by PlayerController when the action key is released. Should implement the specific behavior on key release.
    /// </summary> 
    public void OnActionKeyReleased(PlayerController player);
    /// <summary>
    /// Should reset any state or properties before the PlayerController destroy this strategy (probably to change for another).
    /// </summary>
    public void ResetBeforeDestroy();
    /// <summary>
    /// called by PlayerController every frame to update the movement strategy.
    /// </summary>
    public void Update(PlayerController player);
}