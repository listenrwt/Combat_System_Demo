using FSM;
using UnityEngine;

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

        // State transitions
        Vector2 moveInput = player.data.movementData.moveInput;
        if (!player.data.movementData.isSprinting || moveInput.magnitude <= 0f)
        {
            if (moveInput.magnitude <= 0f)
                movementSM.ChangeState(movementSM.IdleState);
            else if (player.data.movementData.enableRunning || moveInput.magnitude > player.SOdata.runMultiplier)
                movementSM.ChangeState(movementSM.RunningState);
            else
                movementSM.ChangeState(movementSM.WalkingState);
        }
    }

    protected override float GetMoveMultiplier(Vector2 moveInput) => player.SOdata.sprintMultiplier;


    protected override bool ShouldNormalize(Vector2 moveInput) => moveInput.magnitude > 0f;

}
