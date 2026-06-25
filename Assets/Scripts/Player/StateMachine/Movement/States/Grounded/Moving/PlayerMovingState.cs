using FSM;

// Base class for all moving states (walking, running, sprinting)
// Provides shared Enter/Exit logic and movement speed determination
public abstract class PlayerMovingState : PlayerGroundedState
{
    public PlayerMovingState(PlayerController player, PlayerMovementSM movementSM) : base(player, movementSM) { }

    public override void Enter()
    {
        base.Enter();
        player.Animator.SetBool("isStopping", false);
    }

    public override void Update()
    {
        base.Update();

        float magnitude = player.data.movementData.moveInput.magnitude;

        // Check for stopping - transition to idle directly if we were just attacking
        if (magnitude <= 0f)
        {
            if (player.data.combatData.isAttacking)
                return; // Stay in current state while attacking

            // If we just finished attacking, go to idle instead of stopping
            float moveAmount = player.Animator.GetFloat("moveAmount");
            if (moveAmount <= 0.1f)
            {
                movementSM.ChangeState(movementSM.IdleState);
            }
            else
            {
                TransitionToStoppingState();
            }
        }
    }

    private void TransitionToStoppingState()
    {
        // Capture current movement speed and determine which stopping state to use
        float moveAmountBeforeStopping = player.Animator.GetFloat("moveAmount");

        if (moveAmountBeforeStopping <= player.SOdata.walkMultiplier * 0.5f)
        {
            movementSM.WalkStopState.SetStopAmount(moveAmountBeforeStopping);
            movementSM.ChangeState(movementSM.WalkStopState);
        }
        else
        {
            movementSM.RunStopState.SetStopAmount(moveAmountBeforeStopping);
            movementSM.ChangeState(movementSM.RunStopState);
        }
    }
}
