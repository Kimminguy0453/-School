using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrystalBall", menuName = "Item/CrystalBall", order = 0)]
public class CrystalBall : ItemBase<Player>
{
    [SerializeField] private List<Enemy> enemys;

    public override void OnAction(Player agent)
    {
        if(agent.CheckItem(Item_code) > 0)
        {
            for (int i = 0; i <= enemys.Count; i++)
                enemys[i].Timestop();
        }
        else
            UIManager.Instance.TextOn("아이템이 없습니다.", true);
    }
}
