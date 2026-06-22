public class PlayerWalkingState : PlayerGroundedState
{
    public PlayerWalkingState(PlayerController player, PlayerMovementSM movementSM) : base(player, movementSM) { }

    public override void Update()
    {
        base.Update();

        float magnitude = player.data.movementData.moveInput.magnitude;
        if (magnitude <= 0f)
            movementSM.ChangeState(movementSM.IdleState);
        else if (magnitude > player.SOdata.walkMultiplier)
            movementSM.ChangeState(movementSM.RunningState);
    }
}
