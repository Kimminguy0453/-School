using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DK.FSM;
using SETTING_VALUE;

[CreateAssetMenu(fileName = "IdelScript", menuName = "FSM/IdelScript", order = 0)]
public class IdelScript : StateBase<Enemy>
{
    [SerializeField] private StateBase<Enemy> prodingState;
    [SerializeField] private StateBase<Enemy> chaseState;
    public override void Enter(Enemy agent)
    {
    }

    public override void Excute(Enemy agent)
    {
        if (GameManager.Instance.player.Survive)
        {
            if (agent.state == (int)STATE_ID.CHASE)
            {
                agent.ChangeState(chaseState);
                return;
            }
            if (agent.state == (int)STATE_ID.PRODING)
            {
                agent.ChangeState(prodingState);
                return;
            }
        }
    }

    public override void Exit(Enemy agent)
    {
    }
}