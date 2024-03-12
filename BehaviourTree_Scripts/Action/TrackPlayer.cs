using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class TrackPlayer : ActionBase
{
    public override void OnStart()
    {
        //navMeshAgent.isStopped = false;
        //navMeshAgent.Speed = detecPlayer.so.moveSpeed;
        detecPlayer.maxSpeed = 1;
        navMeshAgent.Speed = detecPlayer.so.runSpeed;
        detecPlayer.canChase = true;
        navMeshAgent.AngularSpeed = detecPlayer.so.rotationSpeed;
    }

    public override TaskStatus OnUpdate()
    {
        if (detecPlayer.Player == null) return TaskStatus.Running;
        if (detecPlayer.canChase)
        {
            navMeshAgent.SetDestination(detecPlayer.Player.transform.position);
        }
        return IsFinishMove();
    }


}