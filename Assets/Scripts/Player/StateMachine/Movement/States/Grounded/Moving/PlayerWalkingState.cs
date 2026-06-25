using FSM;

public class PlayerWalkingState : PlayerMovingState
{
    public PlayerWalkingState(PlayerController player, PlayerMovementSM movementSM) : base(player, movementSM) { }

    public override void Update()
    {
        base.Update();

        float inputMagnitude = player.data.movementData.moveInput.magnitude;

        // Only check transitions if we're still moving (base class handles stopping)
        if (inputMagnitude <= 0f)
            return;

        // Transition to sprint if sprinting
        if (player.data.movementData.isSprinting)
        {
            movementSM.ChangeState(movementSM.SprintState);
        }
        // Transition to running if toggle is on or input exceeds walk threshold
        else if (player.data.movementData.isRunningToggled || inputMagnitude > player.SOdata.runMultiplier)
        {
            movementSM.ChangeState(movementSM.RunningState);
        }
    }

    protected override float GetMoveMultiplier()
    {
        bool isGamepad = player.MoveAction.activeControl?.device is UnityEngine.InputSystem.Gamepad;
        return isGamepad ? 1f : player.SOdata.walkMultiplier;
    }
}
