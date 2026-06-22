using FSM;

public class PlayerMovementSM : StateMachine
{

    public PlayerIdleState IdleState { get; }
    public PlayerWalkingState WalkingState { get; }
    public PlayerRunningState RunningState { get; }

    public PlayerMovementSM(PlayerController player)
    {
        IdleState = new PlayerIdleState(player, this);
        WalkingState = new PlayerWalkingState(player, this);
        RunningState = new PlayerRunningState(player, this);

        ChangeState(IdleState);
    }
}
