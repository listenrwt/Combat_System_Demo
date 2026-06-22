using FSM;
using UnityEngine;

public class PlayerAttackState : IState
{
    private PlayerController player;
    private PlayerCombatSM combatSM;

    public PlayerAttackState(PlayerController player, PlayerCombatSM combatSM)
    {
        this.player = player;
        this.combatSM = combatSM;
    }

    public void Enter()
    {
        player.data.combatData.isAttacking = true;
        player.Animator.SetFloat("moveAmount", 0f);
        player.Animator.CrossFade("Slash", 0.2f);
        player.data.combatData.attackTimer = -1f; // sentinel: wait one frame for transition
    }

    public void Exit()
    {
        player.data.combatData.isAttacking = false;
    }

    public void Update()
    {
        if (player.data.combatData.attackTimer < 0f)
        {
            player.data.combatData.attackTimer = 0f;
            return;
        }

        if (player.data.combatData.attackTimer == 0f)
        {
            player.data.combatData.attackTimer = player.Animator.GetNextAnimatorStateInfo(1).length;
            return;
        }

        player.data.combatData.attackTimer -= Time.deltaTime;
        if (player.data.combatData.attackTimer <= 0f)
            combatSM.ChangeState(combatSM.NullState);
    }
}
