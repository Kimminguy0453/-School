using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Mirror", menuName = "Item/Mirror", order = 0)]
public class Mirror : ItemBase<Player>
{
    [SerializeField] List<Vector3> pos;

    public override void OnAction(Player agent)
    {
        if (agent.CheckItem(Item_code) > 0)
        {
            Vector2 randomPos = Random.insideUnitCircle * 100;
            NavMesh.SamplePosition(agent.GetPlayerPos() + new Vector3(randomPos.x, 0, randomPos.y), out NavMeshHit navHit, 100, NavMesh.AllAreas);
            agent.Teleport(navHit.position);
        }
        else
            UIManager.Instance.TextOn("아이템이 없습니다.", true);
    }
}
