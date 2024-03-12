using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalBase : Conditional
{

    protected Transform player;
    public override void OnAwake()
    {
        //player = GameObject.FindWithTag("Player").transform;
        StartCoroutine(FindPlayer());
    }

    IEnumerator FindPlayer()
    {
        yield return YieldCache.WaitUntil(() => (SceneManagement.Instance.CurrentScene as GameScene)?.Player != null);
        player = (SceneManagement.Instance.CurrentScene as GameScene)?.Player.transform;
    }
}
