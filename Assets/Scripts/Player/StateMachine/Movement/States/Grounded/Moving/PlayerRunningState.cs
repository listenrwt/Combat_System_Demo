using FSM;

public class PlayerRunningState : PlayerMovingState
{
    public PlayerRunningState(PlayerController player, PlayerMovementSM movementSM) : base(player, movementSM) { }

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
        // Transition to walking if not toggled and input is below walk threshold
        else if (!player.data.movementData.isRunningToggled || inputMagnitude <= player.SOdata.walkMultiplier)
        {
            movementSM.ChangeState(movementSM.WalkingState);
        }
    }

    protected override float GetMoveMultiplier()
    {
        bool isGamepad = player.MoveAction.activeControl?.device is UnityEngine.InputSystem.Gamepad;
        return isGamepad ? 1f : player.SOdata.runMultiplier;
    }
}
