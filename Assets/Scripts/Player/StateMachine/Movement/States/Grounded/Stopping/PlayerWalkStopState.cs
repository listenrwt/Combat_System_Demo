using FSM;

public class PlayerWalkStopState : PlayerStoppingState
{
    public PlayerWalkStopState(PlayerController player, PlayerMovementSM movementSM) : base(player, movementSM) { }

    public override void SetStopAmount(float amount)
    {
        // Walk stop always uses 0
        stopAmount = 0f;
    }
}
