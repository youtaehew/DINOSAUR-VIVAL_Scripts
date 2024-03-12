using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Enemy/Meet")]
public class EnemySO : ScriptableObject
{
    public float AttackPower;
    public float detectDis;
    public float maxChaseDis;
    //public float minDis;
    public float maxViewAngle;
    public float minViewAngle;
    public float attackRotationSpeed;
    public float rotationSpeed;
    public float runSpeed;
    public float walkSpeed;
    public int Hp;
}
