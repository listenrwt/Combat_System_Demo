using FSM;

public class PlayerNormalMovementState : PlayerGroundedState
{
    public PlayerNormalMovementState(PlayerController player, PlayerMovementSM movementSM) : base(player, movementSM) { }

    public override void Update()
    {
        base.Update();

        // Check if sprinting should be activated
        if (player.data.movementData.isSprinting && player.data.movementData.moveInput.magnitude > 0f)
        {
            movementSM.ChangeState(movementSM.SprintState);
        }
    }
}
