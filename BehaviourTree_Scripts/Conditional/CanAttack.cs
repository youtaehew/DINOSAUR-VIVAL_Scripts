using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanAttack : ConditionalBase
{
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private Color gizmoColor;

    public override TaskStatus OnUpdate()
    {

        float dis = Vector3.Distance(player.position, Owner.transform.position);

        
        if (dis <= attackRange)
        {
            return TaskStatus.Success;
        }
        else return TaskStatus.Failure;

    }
    public override void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(Owner.transform.position, attackRange);

    }
}