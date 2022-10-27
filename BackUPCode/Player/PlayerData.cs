using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "CharacterData/Player Data", order = int.MaxValue)]
public class PlayerData : ScriptableObject
{
    public float WalkSpeed { get => PlayerWalkSpeed; }
    [SerializeField] private float PlayerWalkSpeed;

    public float RunSpeed { get => PlayerRunSpeed; }
    [SerializeField] private float PlayerRunSpeed;

    public float rotateSpeed { get => PlayerRatateSpeed; }
    [SerializeField] private float PlayerRatateSpeed;
}
