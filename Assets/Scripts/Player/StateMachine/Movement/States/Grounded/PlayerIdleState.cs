using FSM;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerController player, PlayerMovementSM movementSM) : base(player, movementSM) { }

    public override void Enter()
    {
        base.Enter();
        moveMultiplier = 0f;
    }
    public override void Update()
    {
        base.Update();
        if (player.data.movementData.moveInput.magnitude > 0f)
        {
            movementSM.ChangeState(movementSM.WalkingState);
        }
    }
}
