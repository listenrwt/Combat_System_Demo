using FSM;

public class PlayerRunningState : PlayerNormalMovementState
{
    public PlayerRunningState(PlayerController player, PlayerMovementSM sm) : base(player, sm) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        float magnitude = player.data.movementData.moveInput.magnitude;

        if (magnitude <= 0f)
        {
            movementSM.ChangeState(movementSM.IdleState);
        }
        else if (!player.data.movementData.enableRunning)
        {
            movementSM.ChangeState(movementSM.WalkingState);
        }
        else if (magnitude <= player.SOdata.walkMultiplier)
        {
            movementSM.ChangeState(movementSM.WalkingState);
        }
    }

    protected override float GetMoveMultiplier(UnityEngine.Vector2 moveInput)
    {
        bool isGamepad = player.MoveAction.activeControl?.device is UnityEngine.InputSystem.Gamepad;
        return isGamepad ?
            1f : player.SOdata.runMultiplier;
    }
}
