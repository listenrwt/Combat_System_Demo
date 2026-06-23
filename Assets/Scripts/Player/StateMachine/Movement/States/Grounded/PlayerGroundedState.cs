using FSM;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerGroundedState : IState
{
    protected PlayerController player;
    protected PlayerMovementSM movementSM;

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
            player.Animator.SetFloat("moveAmount", 0f, 0.2f, Time.deltaTime);
            return;
        }

        UpdateToggles();

        Vector2 moveInput = player.data.movementData.moveInput = player.MoveAction.ReadValue<Vector2>();

        float multiplier = GetMoveMultiplier(moveInput);

        Vector3 moveDir = player.CameraController.PlanarRotation * new Vector3(moveInput.x, 0f, moveInput.y);
        if (ShouldNormalize(moveInput))
            moveDir = moveDir.normalized;

        Vector3 velocity = (moveDir * player.SOdata.moveSpeed * multiplier + new Vector3(0f, player.data.movementData.verticalVelocity, 0f)) * Time.deltaTime;
        player.CharacterController.Move(velocity);

        if (moveInput.magnitude > 0f)
            player.data.movementData.targetRotation = Quaternion.LookRotation(moveDir);
        player.transform.rotation = Quaternion.RotateTowards(
            player.transform.rotation, player.data.movementData.targetRotation, player.SOdata.rotateSpeed * Time.deltaTime);

        float animAmount = ShouldNormalize(moveInput) ? multiplier : moveInput.magnitude * multiplier;
        player.Animator.SetFloat("moveAmount", animAmount, 0.2f, Time.deltaTime);
    }

    protected virtual float GetMoveMultiplier(Vector2 moveInput) => 1f;

    protected virtual bool ShouldNormalize(Vector2 moveInput) => false;

    protected void UpdateToggles()
    {
        // Check if current input is from gamepad by examining the last used device
        bool isGamepad = player.MoveAction.activeControl?.device is Gamepad;

        if (isGamepad)
        {
            // Gamepad: analog stick magnitude drives walk/run dynamically — no toggle needed.
            player.data.movementData.enableRunning = false;
        }
        else
        {
            // Keyboard: toggle walk/run on button press.
            if (player.WalkToggleAction.triggered)
            {
                player.data.movementData.enableRunning = !player.data.movementData.enableRunning;
            }

            if (player.SprintAction.triggered)
            {
                player.data.movementData.isSprinting = !player.data.movementData.isSprinting;
            }
        }
    }
}
