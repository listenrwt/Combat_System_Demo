using FSM;

public class PlayerIdleState : PlayerNormalMovementState
{
    public PlayerIdleState(PlayerController player, PlayerMovementSM movementSM) : base(player, movementSM) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (player.data.movementData.moveInput.magnitude > 0f)
        {
            if (player.data.movementData.enableRunning)
                movementSM.ChangeState(movementSM.RunningState);
            else if (player.data.movementData.moveInput.magnitude > player.SOdata.runMultiplier)
                movementSM.ChangeState(movementSM.RunningState);
            else
                movementSM.ChangeState(movementSM.WalkingState);
        }
    }

    protected override float GetMoveMultiplier() => 0f;
}
