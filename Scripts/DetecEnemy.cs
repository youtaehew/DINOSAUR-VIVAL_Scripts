using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetecEnemy : MonoBehaviour
{
    public float detectDis;
    public LayerMask enemyLayer;

    public bool isGrowl = false;

    DetecPlayer player;

    private void Awake()
    {
        player = GetComponent<DetecPlayer>();
        
    }

    private void Update()
    {
        if (isGrowl)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectDis, enemyLayer);
            if (colliders != null)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    DetecPlayer detecPlayer = colliders[i].GetComponent<DetecPlayer>();      
                    if (detecPlayer.gameObject == gameObject) continue;   
                    
                    //detecPlayer.CheckEnemy(player.Player);
                    //detecPlayer.growlCount = 0;
                }
            }
        }
        
    }
}
