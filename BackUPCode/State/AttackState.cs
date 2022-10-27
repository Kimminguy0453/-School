using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DK.FSM;
using SETTING_VALUE;

[CreateAssetMenu(fileName = "AttackState", menuName = "FSM/AttackState", order = 0)]
public class AttackState : StateBase<Enemy>
{
    [SerializeField] private StateBase<Enemy> idelState;

    public override void Enter(Enemy agent)
    {

        agent.navAgent.ResetPath();
        agent.navAgent.velocity = Vector3.zero;
        agent.navAgent.isStopped = true;
        GameManager.Instance.DeathPlayer();
        agent.JumpScare();
        agent.animePass.SetTrigger("Attack");
    }

    public override void Excute(Enemy agent)
    {
        if(GameManager.Instance.gameOver)
            agent.ChangeState(idelState);
    }

    public override void Exit(Enemy agent)
    {
    }
}