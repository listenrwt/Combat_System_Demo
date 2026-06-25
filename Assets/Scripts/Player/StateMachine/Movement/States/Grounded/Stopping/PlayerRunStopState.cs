using FSM;

public class PlayerRunStopState : PlayerStoppingState
{
    public PlayerRunStopState(PlayerController player, PlayerMovementSM movementSM) : base(player, movementSM) { }

    public override void SetStopAmount(float amount)
    {
        // Normalize the stop amount based on sprint multiplier
        stopAmount = amount / player.SOdata.sprintMultiplier;
    }
}
