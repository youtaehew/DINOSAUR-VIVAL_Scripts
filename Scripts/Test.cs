using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YTest : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 10);
    }
}
