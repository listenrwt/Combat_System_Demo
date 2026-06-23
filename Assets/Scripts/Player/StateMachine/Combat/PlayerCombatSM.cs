using FSM;

public class PlayerCombatSM : StateMachine
{
    public PlayerCombatNullState NullState { get; }
    public PlayerLightAttackState AttackState { get; }
    public PlayerHeavyAttackState HeavyAttackState { get; }

    public PlayerCombatSM(PlayerController player)
    {
        NullState = new PlayerCombatNullState(player, this);
        AttackState = new PlayerLightAttackState(player, this);
        HeavyAttackState = new PlayerHeavyAttackState(player, this);

        ChangeState(NullState);
    }
}
