using FSM;

public class PlayerSprintState : PlayerGroundedState
{
    public PlayerSprintState(PlayerController player, PlayerMovementSM movementSM) : base(player, movementSM) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        player.data.movementData.isSprinting = false;
    }

    public override void Update()
    {
        base.Update();
        // Transition back to normal movement state when sprint ends
        if (!player.data.movementData.isSprinting || player.data.movementData.moveInput.magnitude <= 0f)
        {
            movementSM.ChangeState(movementSM.IdleState);
        }
    }
    protected override float GetMoveMultiplier() => player.SOdata.sprintMultiplier;

    protected override bool ShouldNormalize() => player.data.movementData.moveInput.magnitude > 0f;

}
