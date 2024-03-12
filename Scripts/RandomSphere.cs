using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomSphere : MonoBehaviour
{
    float angle;
    [SerializeField] float speed;
    [SerializeField] float radius;

    [SerializeField] GameObject Player;

    Vector3 spherePos;
    NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {
        angle += Time.deltaTime * speed;
        transform.position = Player.transform.position + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Player.transform.position, radius);

    }
}
