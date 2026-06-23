public class PlayerWalkingState : PlayerNormalMovementState
{
    public PlayerWalkingState(PlayerController player, PlayerMovementSM movementSM) : base(player, movementSM)
    {
    }

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
        else if (player.data.movementData.enableRunning)
        {
            movementSM.ChangeState(movementSM.RunningState);
        }
        else if (magnitude > player.SOdata.runMultiplier)
        {
            movementSM.ChangeState(movementSM.RunningState);
        }
    }

    protected override float GetMoveMultiplier(UnityEngine.Vector2 moveInput)
    {
        bool isGamepad = player.MoveAction.activeControl?.device is UnityEngine.InputSystem.Gamepad;
        return isGamepad ?
            1f :
            player.SOdata.walkMultiplier;
    }
}
