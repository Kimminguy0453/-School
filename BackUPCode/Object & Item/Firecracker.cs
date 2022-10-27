using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Firecracker", menuName = "Item/Firecracker", order = 0)]
public class Firecracker : ItemBase<Player>
{
    [SerializeField] private GameObject player;
    [SerializeField] List<Vector3> pos;

    public override void OnAction(Player agent)
    {
        if (agent.CheckItem(Item_code) > 0)
        {
            
        }
        else
            UIManager.Instance.TextOn("아이템이 없습니다.", true);
    }
}