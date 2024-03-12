using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class ActionBase : Action
{
    protected Animator ani;
    //protected NavMeshAgent navMeshAgent;
    protected CustomNavMeshAgent navMeshAgent;
    protected DetecPlayer detecPlayer;


    public override void OnAwake()
    {
        detecPlayer = Owner.GetComponent<DetecPlayer>();
        ani = Owner.GetComponent<Animator>();
        navMeshAgent = Owner.GetComponent<CustomNavMeshAgent>();
    }

    protected TaskStatus IsFinishAnimation(string animationName, float time) => ani.GetCurrentAnimatorStateInfo(0).IsName(animationName) 
        && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= .8f ? TaskStatus.Success : TaskStatus.Running;

    protected TaskStatus IsFinishMove() => Vector3.Distance(navMeshAgent.Destination, transform.position) <= .2f ? TaskStatus.Success : TaskStatus.Running;
}
