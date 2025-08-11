using System;
using Random = UnityEngine.Random;

public enum MovementStrategyEnum
{
    Jump,
    DoubleJump,
    GravityInvertion,
}
public class MovementStrategyEnumUtil
{
    public static IMovementStrategy GetMovementStrategy(MovementStrategyEnum movementStrategyEnum)
    {
        return movementStrategyEnum switch
        {
            MovementStrategyEnum.Jump => new JumpStrategy(),
            MovementStrategyEnum.DoubleJump => new DoubleJumpStrategy(),
            MovementStrategyEnum.GravityInvertion => new GravityInvertionStrategy(),
            _ => throw new System.ArgumentOutOfRangeException(nameof(movementStrategyEnum), movementStrategyEnum, null)
        };
    }

    public static MovementStrategyEnum GetRandomMovementStrategyExceptOne(MovementStrategyEnum strategyToExclude)
    {
        var values = (MovementStrategyEnum[]) Enum.GetValues(typeof(MovementStrategyEnum));
        var filteredValues = Array.FindAll(values, value => value != strategyToExclude);
        
        return filteredValues[Random.Range(0, filteredValues.Length)];
    }
}