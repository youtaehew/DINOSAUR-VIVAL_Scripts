using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Runtime.InteropServices;

public class CanAttackJump : ConditionalBase
{
    [SerializeField]
    private float jumpAttackRange;
    [SerializeField]
    private float minjumpAttackRange;

    [SerializeField]
    private Color gizmoColor;

    public override TaskStatus OnUpdate()
    {

        float dis = Vector3.Distance(player.position, Owner.transform.position);

        if (minjumpAttackRange <=dis && dis <= jumpAttackRange)
        {
            return TaskStatus.Success;
        }
        else return TaskStatus.Failure;

    }
    public override void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(Owner.transform.position, jumpAttackRange);
        Gizmos.DrawWireSphere(Owner.transform.position, minjumpAttackRange);


    }
}