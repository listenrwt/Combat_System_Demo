using FSM;

public class PlayerCombatSM : StateMachine
{
    public PlayerCombatNullState NullState { get; }
    public PlayerAttackState AttackState { get; }

    public PlayerCombatSM(PlayerController player)
    {
        NullState = new PlayerCombatNullState(player, this);
        AttackState = new PlayerAttackState(player, this);

        ChangeState(NullState);
    }
}
