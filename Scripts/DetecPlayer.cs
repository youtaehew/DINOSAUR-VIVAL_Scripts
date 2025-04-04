using BehaviorDesigner.Runtime;
using System;
using MeatStat;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using System.Collections;
using Invector.vCharacterController;
public class DetecPlayer : MonoBehaviour //Enemy
{
    public bool isMeat;
    public EnemySO so;

    public bool canChase;
    public BehaviorTree tree;
    public bool firstAttack = true;
    Animator ani;

    public GameObject Player;

    //DetecEnemy detecEnemy;
    public Vector3 direction;
    public float playerDis;
    CustomNavMeshAgent navMeshAgent;
    float viewable;
    private WeaponCollider weaponCollider;
    private LayerMask layer;
    public DinoControll dinoControll;

    public bool Attacking = false;
    private EnemyHp enemyHP;


    public float maxSpeed;

    private int age; public int Age => age;

    private bool start;
    private PoolableMono poolable;

    private void Awake()
    {
        poolable = GetComponent<PoolableMono>();
        enemyHP = GetComponent<EnemyHp>();
        ani = GetComponent<Animator>();
        tree = GetComponent<BehaviorTree>();
        navMeshAgent = GetComponent<CustomNavMeshAgent>();
        weaponCollider = transform.GetChild(0).GetComponent<WeaponCollider>();
        dinoControll = GetComponentInChildren<DinoControll>();
        start = true;
        var a = LayerMask.NameToLayer("Player");
        layer = 1 << a;
    }

    private IEnumerator Start()
    {
        yield return YieldCache.WaitUntil(() => ((GameScene)SceneManagement.Instance.CurrentScene).Player != null);

        if (Vector3.Distance(transform.position,
                ((GameScene)SceneManagement.Instance.CurrentScene).Player.transform.position) <= 500)
        {
            PoolManager.Instance.Push(poolable);
        }
        int currentPlayerLv = ((GameScene)SceneManagement.Instance.CurrentScene).Player.CurrentLevel;
        int maxAge = Mathf.Min(currentPlayerLv + 5, Data.DataMap.Count);
        int minAge = Mathf.Max(currentPlayerLv - 5, 1);

        age = Random.Range(minAge, maxAge);
        if (isMeat)
        {
            Data data = Data.DataMap[age];
            weaponCollider.Power = data.Damage;
            enemyHP.maxHp = data.Helth;
        }
        else
        {
            VegeatableStat.Data data = VegeatableStat.Data.DataMap[age];
            weaponCollider.Power = data.Damage;
            enemyHP.maxHp = data.Helth;
        }
        enemyHP.currentHp = enemyHP.maxHp;

        if (start)
            Invoke("SetAge", 1);
        else
            SetAge();
    }

    void SetAge()
    {
        dinoControll.SetAge(age);
        AttackCollider(0);

        start = false;
    }

    private void Update()
    {
        ani.SetBool("IsMoving", navMeshAgent.Velocity.sqrMagnitude > 0);
        ani.SetFloat("Speed", Mathf.Min(navMeshAgent.Velocity.sqrMagnitude, maxSpeed), 0.1f, Time.deltaTime);
        if (isMeat)
        {
            if (!(bool)tree.GetVariable("isDetect").GetValue())
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, so.detectDis, layer);
                if (colliders != null)
                {
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        Vector3 targetDir = colliders[i].transform.position - transform.position;
                        viewable = Vector3.Angle(targetDir, transform.forward);
                        if (viewable > so.minViewAngle && viewable < so.maxViewAngle)
                        {
                            tree.SetVariableValue("isDetect", true);
                            tree.SetVariableValue("Growl", true);
                            Player = colliders[i].gameObject;
                            dinoControll.SwitchEyeShape(2);
                        }
                    }
                }
            }
            else
            {
                HandleRotate();
                playerDis = Vector3.Distance(Player.transform.position, transform.position);

                if (playerDis >= so.maxChaseDis)
                {
                    canChase = false;
                    tree.SetVariableValue("isDetect", false);
                    tree.SetVariableValue("Growl", false);
                }
            }
        }
        else
        {
            if ((bool)tree.GetVariable("isDetect").GetValue())
            {
                if (Player != null)
                {
                    HandleRotate();
                    playerDis = Vector3.Distance(Player.transform.position, transform.position);

                    if (playerDis >= so.maxChaseDis)
                    {
                        canChase = false;
                        firstAttack = true;
                        tree.SetVariableValue("isDetect", false);
                        tree.SetVariableValue("Growl", false);
                    }
                }
            }
        }
    }

    public void HitGrawl()
    {
        tree.SetVariableValue("isDetect", true);
        canChase = true;
    }

    public void StartGrowl()
    {
        tree.SetVariableValue("isDetect", true);
        tree.SetVariableValue("Growl", true);
        Player = GameObject.FindWithTag("Player");
        dinoControll.SwitchEyeShape(2);
    }


    //public void EnemeyGrowlCheck()
    //{
    //    //detecEnemy.isGrowl = true;
    //}

    private void HandleRotate()
    {
        if (!Attacking)
        {
            direction = Player.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                so.attackRotationSpeed * Time.deltaTime);
        }
    }

    public void AttackCollider(int a)
    {
        weaponCollider.EnableCollider(Convert.ToBoolean(a));
    }

    public void AttackManage(int a)
    {
        Attacking = Convert.ToBoolean(a);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, so.detectDis);
    }
}
