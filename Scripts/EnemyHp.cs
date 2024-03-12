using BehaviorDesigner.Runtime;
using FIMSpace.FProceduralAnimation;
using FIMSpace.FTail;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Parkjung2016;
using UnityEngine.AI;
using UnityEditor;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;
public class EnemyHp : MonoBehaviour, IApplyDamage
{
    public float maxHp;
    public float currentHp;
    BehaviorTree tree;
    Animator ani;
    DetecPlayer detecPlayer;
    NavMeshAgent navMeshAgent;

    TailAnimator2 tail;
    LegsAnimator leg;

    SkinnedMeshRenderer mesh;
    Material mat;
    int a;
    public event Action DeathAction;
    int spawnCount;

    EnemyReturnToPool enemyReturnToPool;

    private void Awake()
    {
        enemyReturnToPool = GetComponent<EnemyReturnToPool>();
           tree = GetComponent<BehaviorTree>();
        tail = transform.GetChild(1).GetComponent<TailAnimator2>();
        leg = transform.GetChild(1).GetComponent<LegsAnimator>();
        ani = GetComponent<Animator>();
        detecPlayer = GetComponent<DetecPlayer>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        //mesh = transform.GetChild(0).GetComponentInChildren<SkinnedMeshRenderer>();
        //mat = mesh.material;
    }

    private void meatCount()
    {
       if(detecPlayer.dinoControll.DinoAgeProp <= 5)
        {
            spawnCount = 1;
        }
        else
        {
            spawnCount = (int)detecPlayer.dinoControll.DinoAgeProp / 5 + 1;
        }
    }

    private void Start()
    {
        meatCount();

        //maxHp = detecPlayer.so.Hp;
        //currentHp = maxHp;
    }




    private void Update()
    {
        if (!detecPlayer.isMeat)
        {
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("Hit" + a) && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                if (detecPlayer.firstAttack)
                {
                    detecPlayer.StartGrowl();
                    detecPlayer.firstAttack = false;
                }

            }
        }


    }





    private void Die()
    {
        detecPlayer.dinoControll.SwitchEyeShape(6);
        navMeshAgent.angularSpeed = 0;
        tree.enabled = false;
        navMeshAgent.speed = 0;
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.enabled = false;
        leg.enabled = false;
        tail.enabled = false;
        detecPlayer.Attacking = false;
        DeathAction?.Invoke();
        dropMeat();
    }

    private void dropMeat()
    {
        print("Drop");
        for (int i = 0; i < spawnCount; ++i)
            PoolManagerq.SpawnFromPool("Meat", transform.position, Quaternion.identity);
       

        GameObject[] gm = GameObject.FindGameObjectsWithTag("Meat");
        foreach (var dd in gm)
        {
            RetuenPool pool = dd.GetComponent<RetuenPool>();
            pool.Spawn();
        }

        DG.Tweening.Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMoveY(-50, 4)).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    public void hitEnd()
    {
        detecPlayer.AttackCollider(0);
        tree.enabled = true;
        detecPlayer.dinoControll.SwitchEyeShape(2);
        //navMeshAgent.isStopped = false;
    }

    public void ApplyDamage(float damage)
    {
        if (currentHp < 0) return;
        detecPlayer.dinoControll.SwitchEyeShape(5);
        currentHp -= damage;
        tree.enabled = false;
        navMeshAgent.isStopped = true;
        navMeshAgent.velocity = Vector3.zero;
        detecPlayer.Attacking = false;


        if (currentHp <= 0)
        {
            ani.CrossFadeInFixedTime("Die", 0.2f);
            Die();
            return;
        }

        detecPlayer.AttackCollider(0);
        a = Random.Range(1, 3);

        ani.CrossFadeInFixedTime("Hit" + a, 0.2f);
    }



}
