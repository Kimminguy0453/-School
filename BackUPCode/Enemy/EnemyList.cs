using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyList", menuName = "FSM/EnemyList", order = 0)]
public class EnemyList : ScriptableObject
{
    [SerializeField]private List<GameObject> eList = new List<GameObject>();
    public List<GameObject> getlist { get => eList; }
}
