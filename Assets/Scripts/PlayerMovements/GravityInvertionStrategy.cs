
using UnityEngine;

public class GravityInvertionStrategy : IMovementStrategy
{
   private Vector3 _initialGravity;
   
   public GravityInvertionStrategy()
   {
      _initialGravity = Physics.gravity;
   }

   public void OnActionKeyPressed(PlayerController player)
   {
      Physics.gravity = -Physics.gravity;
   }

   public void OnActionKeyReleased(PlayerController player){}

   public void ResetBeforeDestroy()
   {
      Physics.gravity = _initialGravity;
   }

   public void Update(PlayerController player){}
}
