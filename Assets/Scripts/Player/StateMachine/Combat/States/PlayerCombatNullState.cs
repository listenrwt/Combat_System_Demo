using FSM;

public class PlayerCombatNullState : IState
{
    private PlayerController player;
    private PlayerCombatSM combatSM;

    public PlayerCombatNullState(PlayerController player, PlayerCombatSM combatSM)
    {
        this.player = player;
        this.combatSM = combatSM;
    }

    public void Enter() { }
    public void Exit() { }

    public void Update()
    {
        if (player.AttackAction.WasPressedThisFrame() && !player.data.combatData.isAttacking)
            combatSM.ChangeState(combatSM.AttackState);
    }
}
