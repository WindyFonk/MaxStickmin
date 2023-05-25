using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPattern : MonoBehaviour
{
    public int attackDmg = 1;
    public float range = 1;
    public LayerMask mask;

    public void Attack()
    {
        Collider2D colInfo = Physics2D.OverlapCircle(transform.position, range, mask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerController>().health -= attackDmg;
            Debug.Log("Hit");
        }
    }
}
