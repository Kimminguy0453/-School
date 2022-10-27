using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DK.FSM;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Object/Enemy Data", order = int.MaxValue)]
public class EnemyData : ScriptableObject
{
    public int EnemyNumber { get => enemyNumber;}
    [SerializeField] private int enemyNumber;
    public string EnemyName { get => enemyName; }
    [SerializeField] private string enemyName;
    public float WalkSpeed { get => walkSpeed; }
    [SerializeField] private float walkSpeed;

    public float RunSpeed { get => runSpeed; }
    [SerializeField] private float runSpeed;

    public float RecognizeRange{ get => recognizeRange; }
    [SerializeField] private float recognizeRange;

    public float RecognizeAngle { get => recognizeAngle; }
    [SerializeField] private float recognizeAngle;

    public float AttackRange { get => attackRange;  }
    [SerializeField] private float attackRange;

    public StateBase<Enemy> StartState { get => startState; }
    [SerializeField] StateBase<Enemy> startState;
}
