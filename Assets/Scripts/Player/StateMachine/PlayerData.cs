using UnityEngine;
public class PlayerData
{
    public PlayerMovementData movementData = new PlayerMovementData();
    public PlayerCombatData combatData = new PlayerCombatData();
}

public class PlayerMovementData
{
    public Vector2 moveInput;
    public float verticalVelocity = -0.5f;
    public bool isGround;
    public Quaternion targetRotation;
    public bool isRunning = false;
}

public class PlayerCombatData
{
    public bool isAttacking = false;
    public float attackTimer = 0f;
}
