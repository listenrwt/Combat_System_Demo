using FSM;

public class PlayerRunningState : PlayerGroundedState
{
    public PlayerRunningState(PlayerController player, PlayerMovementSM sm) : base(player, sm) { }

    public override void Update()
    {
        base.Update();

        if (player.data.movementData.moveInput.magnitude <= player.SOdata.walkMultiplier)
            movementSM.ChangeState(movementSM.WalkingState);
        else if (player.data.movementData.moveInput.magnitude <= 0f)
            movementSM.ChangeState(movementSM.IdleState);
    }
}
