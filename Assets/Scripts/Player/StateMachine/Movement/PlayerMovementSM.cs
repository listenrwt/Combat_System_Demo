using FSM;

public class PlayerMovementSM : StateMachine
{

    public PlayerIdleState IdleState { get; }
    public PlayerWalkingState WalkingState { get; }
    public PlayerRunningState RunningState { get; }
    public PlayerSprintState SprintState { get; }
    public PlayerNormalMovementState NormalMovementState { get; }

    public PlayerMovementSM(PlayerController player)
    {
        NormalMovementState = new PlayerNormalMovementState(player, this);
        IdleState = new PlayerIdleState(player, this);
        WalkingState = new PlayerWalkingState(player, this);
        RunningState = new PlayerRunningState(player, this);
        SprintState = new PlayerSprintState(player, this);

        ChangeState(IdleState);
    }
}
