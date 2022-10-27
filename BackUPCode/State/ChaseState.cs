using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DK.FSM;
using SETTING_VALUE;

[CreateAssetMenu(fileName = "ChaseState", menuName = "FSM/ChaseState", order = 0)]
public class ChaseState : StateBase<Enemy>
{
    [SerializeField] private StateBase<Enemy> attackState;
    [SerializeField] private StateBase<Enemy> prodingState;
    private Vector3 target;

    public override void Enter(Enemy agent)
    {
        if (agent.CheckAttack())
        {
            agent.ChangeState(attackState);
            return;
        }
        agent.navAgent.speed = agent.data.RunSpeed;
        agent.animePass.SetBool("RockOn", true);
    }

    public override void Excute(Enemy agent)
    {
        if (agent.CheckAttack())
        {
            agent.ChangeState(attackState);
            return;
        }
        if (agent.state == (int)STATE_ID.PRODING)
        {
            agent.ChangeState(prodingState);
            return;
        }
        target = GameManager.Instance.player.GetPlayerPos();
        agent.MoveEnemy(target);
    }

    public override void Exit(Enemy agent)
    {
        agent.animePass.SetBool("RockOn", false);
    }
}
