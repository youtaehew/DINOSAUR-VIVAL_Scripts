using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class EnemyGrowl : ActionBase
{
	public override void OnStart()
	{
        //navMeshAgent.isStopped = true;
        navMeshAgent.Speed = 0;
        navMeshAgent.Velocity = Vector3.zero;
        ani.SetTrigger("isGrowl");
        detecPlayer.tree.SetVariableValue("Growl", false);

    }

	public override TaskStatus OnUpdate()
	{
		return IsFinishAnimation("Growl", 0.9f);
	}
}