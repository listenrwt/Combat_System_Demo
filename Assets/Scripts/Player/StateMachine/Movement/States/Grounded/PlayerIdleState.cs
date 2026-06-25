using FSM;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerController player, PlayerMovementSM movementSM) : base(player, movementSM) { }

    public override void Enter()
    {
        base.Enter();
        player.Animator.SetBool("isStopping", false);
    }

    public override void Update()
    {
        base.Update();

        if (player.data.movementData.moveInput.magnitude > 0f)
        {
            // Determine which moving state to enter based on current input
            if (player.data.movementData.isSprinting)
                movementSM.ChangeState(movementSM.SprintState);
            else if (player.data.movementData.isRunningToggled)
                movementSM.ChangeState(movementSM.RunningState);
            else if (player.data.movementData.moveInput.magnitude > player.SOdata.runMultiplier)
                movementSM.ChangeState(movementSM.RunningState);
            else
                movementSM.ChangeState(movementSM.WalkingState);
        }
    }

    protected override float GetMoveMultiplier() => 0f;
}
