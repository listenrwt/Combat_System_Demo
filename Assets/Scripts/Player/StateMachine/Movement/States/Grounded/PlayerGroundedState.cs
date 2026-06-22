using FSM;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : IState
{
    protected PlayerController player;
    protected PlayerMovementSM movementSM;
    protected float moveMultiplier = 0f;

    public PlayerGroundedState(PlayerController player, PlayerMovementSM movementSM)
    {
        this.player = player;
        this.movementSM = movementSM;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }


    public virtual void Update()
    {
        if (player.data.combatData.isAttacking)
        {
            player.data.movementData.moveInput = Vector2.zero;
            player.Animator.SetFloat("moveAmount", 0f);
            return;
        }
        UpdateWalkToggle();

        Vector2 moveInput = player.data.movementData.moveInput = player.MoveAction.ReadValue<Vector2>();

        Vector3 moveDir = player.CameraController.PlanarRotation * new Vector3(moveInput.x, 0f, moveInput.y);
        Vector3 velocity = (moveDir * player.SOdata.moveSpeed + new Vector3(0f, player.data.movementData.verticalVelocity, 0f)) * Time.deltaTime;
        player.CharacterController.Move(velocity);

        if (moveInput.magnitude > 0f)
            player.data.movementData.targetRotation = Quaternion.LookRotation(moveDir);
        player.transform.rotation = Quaternion.RotateTowards(
            player.transform.rotation, player.data.movementData.targetRotation, player.SOdata.rotateSpeed * Time.deltaTime);

        player.Animator.SetFloat("moveAmount", moveInput.magnitude, 0.2f, Time.deltaTime);
    }

    private void UpdateWalkToggle()
    {
        if (player.WalkToggleAction.triggered)
        {
            player.data.movementData.isRunning = !player.data.movementData.isRunning;
        }
    }
}
