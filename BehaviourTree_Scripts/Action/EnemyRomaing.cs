using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;


public class EnemyRomaing : ActionBase
{
    public float speed;
    public float minRange;
    public float maxRange;
    Vector3 point;
    float time = 0;
    bool canRotate;
   

    


    public override void OnStart()
    {
        detecPlayer.dinoControll.SwitchEyeShape(1);
        navMeshAgent.AngularSpeed = detecPlayer.so.rotationSpeed ;
        navMeshAgent.Speed = detecPlayer.so.walkSpeed;
        detecPlayer.Attacking = false;
        //navMeshAgent.isStopped = false;

        detecPlayer.maxSpeed = .5f;
        //navMeshAgent.speed = speed;

    }

    public Vector3 RandomPointInAnnulus(Vector3 origin, float minRadius, float maxRadius)
    {

        var randomDirection = (Random.insideUnitSphere * 10).normalized;

        var randomDistance = Random.Range(minRadius, maxRadius);

        point = origin + randomDirection * randomDistance;

        return point;
    }
    public override TaskStatus OnUpdate()
    {
        float playerDis = Vector3.Distance(navMeshAgent.Destination, transform.position);
                                                                                                       
        if (playerDis <= 1)
        {
            time += Time.deltaTime;
            canRotate = false;
            if (time >= 1)
            {
                time = 0;
                canRotate = true;
                navMeshAgent.SetDestination(RandomPointInAnnulus(transform.position, minRange, maxRange));
            }


        }
        HandleRotate();
        return TaskStatus.Success;
    }

    private void HandleRotate()
    {
        if (canRotate)
        {
            Vector3 direction;
            direction = point - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
               4 * Time.deltaTime);
        }
        

    }
}