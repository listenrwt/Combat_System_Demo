using FSM;

public class PlayerSprintState : PlayerMovingState
{
    public PlayerSprintState(PlayerController player, PlayerMovementSM movementSM) : base(player, movementSM) { }

    public override void Update()
    {
        base.Update();

        // Transition to running if no longer sprinting
        if (!player.data.movementData.isSprinting)
        {
            movementSM.ChangeState(movementSM.RunningState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.data.movementData.isSprinting = false;
    }

    protected override float GetMoveMultiplier() => player.SOdata.sprintMultiplier;
    protected override bool ShouldNormalize() => true;
}
