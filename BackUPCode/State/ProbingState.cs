using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DK.FSM;
using SETTING_VALUE;

[CreateAssetMenu(fileName = "ProbingState", menuName = "FSM/ProbingState", order = 0)]
public class ProbingState : StateBase<Enemy>
{
    [SerializeField] private StateBase<Enemy> chaseState;
    private Vector3 target;

    // Start is called before the first frame update
    public override void Enter(Enemy agent)
    {
        target = Vector3.zero;
        agent.navAgent.speed = agent.data.WalkSpeed;
        agent.animePass.SetBool("RockOn", false);
        Move(agent);
    }

    public override void Excute(Enemy agent)
    {
        if (agent.state == (int)STATE_ID.CHASE)
        {
            agent.ChangeState(chaseState);
            return;
        }

        if (agent.GoalCheck())
            Move(agent);
    }

    public override void Exit(Enemy agent)
    {
        agent.animePass.SetBool("RockOn", false);
    }

    public void Move(Enemy agent)
    {
        target = agent.controller.SetSection();
        agent.MoveEnemy(target);
    }
}
