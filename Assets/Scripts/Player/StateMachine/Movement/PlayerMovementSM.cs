using FSM;

public class PlayerMovementSM : StateMachine
{
    public PlayerIdleState IdleState { get; }
    public PlayerWalkingState WalkingState { get; }
    public PlayerRunningState RunningState { get; }
    public PlayerSprintState SprintState { get; }
    public PlayerWalkStopState WalkStopState { get; }
    public PlayerRunStopState RunStopState { get; }

    public PlayerMovementSM(PlayerController player)
    {
        IdleState = new PlayerIdleState(player, this);
        WalkingState = new PlayerWalkingState(player, this);
        RunningState = new PlayerRunningState(player, this);
        SprintState = new PlayerSprintState(player, this);
        WalkStopState = new PlayerWalkStopState(player, this);
        RunStopState = new PlayerRunStopState(player, this);

        ChangeState(IdleState);
    }
}
