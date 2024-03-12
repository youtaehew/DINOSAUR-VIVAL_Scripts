using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class LepterAttack : ActionBase
{

    public override void OnStart()
	{
		ani.SetFloat("Speed", 0);
        //navMeshAgent.isStopped = true;
        navMeshAgent.Speed = 0;
        navMeshAgent.Velocity = Vector3.zero;
        navMeshAgent.AngularSpeed = 0;
        ani.CrossFadeInFixedTime("Attack", 0.2f);
	}



    public override TaskStatus OnUpdate()
	{
		return IsFinishAnimation("Attack", .8f);
    }
}