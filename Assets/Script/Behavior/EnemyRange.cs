using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    public EnemyBehavior enemy;
    void Start()
    {
        enemy= gameObject.transform.parent.gameObject.GetComponent<EnemyBehavior>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.shoot = true;
            enemy.isAiming = true;
        };
    }

}
