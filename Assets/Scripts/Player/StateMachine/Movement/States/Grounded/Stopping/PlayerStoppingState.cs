using FSM;

// Base class for all stopping states (walk stop, run stop)
// Provides shared stopping logic and handles transitions back to moving or idle
public abstract class PlayerStoppingState : PlayerGroundedState
{
    protected float stopAmount;

    public PlayerStoppingState(PlayerController player, PlayerMovementSM movementSM) : base(player, movementSM) { }

    public virtual void SetStopAmount(float amount)
    {
        stopAmount = amount;
    }

    public override void Enter()
    {
        base.Enter();
        player.Animator.SetBool("isStopping", true);
        player.Animator.SetFloat("stopAmount", stopAmount);
        player.data.movementData.stoppingTimer = -1f; // sentinel: wait one frame for transition
    }

    public override void Update()
    {
        base.Update();

        // Handle stopping animation timer
        if (player.data.movementData.stoppingTimer < 0f)
        {
            player.data.movementData.stoppingTimer = 0f;
            return;
        }

        if (player.data.movementData.stoppingTimer == 0f)
        {
            player.data.movementData.stoppingTimer = player.Animator.GetNextAnimatorStateInfo(0).length;
            return;
        }

        player.data.movementData.stoppingTimer -= UnityEngine.Time.deltaTime;

        // If stop animation is finished, transition to idle
        if (player.data.movementData.stoppingTimer <= 0f)
        {
            movementSM.ChangeState(movementSM.IdleState);
            return;
        }

        // If player starts moving again, transition to appropriate moving state
        if (player.data.movementData.moveInput.magnitude > 0f)
        {
            TransitionToMovingState();
        }
    }

    private void TransitionToMovingState()
    {
        // Determine which moving state to enter based on current input
        if (player.data.movementData.isSprinting)
            movementSM.ChangeState(movementSM.SprintState);
        else if (player.data.movementData.isRunningToggled)
            movementSM.ChangeState(movementSM.RunningState);
        else if (player.data.movementData.moveInput.magnitude > player.SOdata.runMultiplier)
            movementSM.ChangeState(movementSM.RunningState);
        else
            movementSM.ChangeState(movementSM.WalkingState);
    }
}
