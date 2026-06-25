using FSM;
using UnityEngine;

public abstract class PlayerGroundedState : IState
{
    protected PlayerController player;
    protected PlayerMovementSM movementSM;

    public PlayerGroundedState(PlayerController player, PlayerMovementSM movementSM)
    {
        this.player = player;
        this.movementSM = movementSM;
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void Update()
    {
        player.UpdateMovementToggles();
        player.Move(GetMoveMultiplier(), ShouldNormalize());
    }

    protected virtual float GetMoveMultiplier() => 1f;

    protected virtual bool ShouldNormalize() => false;
}
