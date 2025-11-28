using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bit Motion Data", menuName = " Scriptable Objects/Bit Motion Data", order = 0)]
public class BitMotionData : ScriptableObject
{
    [field : SerializeField] public float MoveSpeed { get; private set; }
    [field : SerializeField] public float JumpSpeed { get; private set; }
}
