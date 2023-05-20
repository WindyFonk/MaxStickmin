using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private Transform player;
    private Vector3 target;
    private Rigidbody2D rb;
    public float speed=2;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Torso").transform;
        rb= GetComponent<Rigidbody2D>();

        target = player.transform.position;
        AimAtPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        target = player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }      
    }

    private void AimAtPlayer()
    {
        Vector3 direction = target - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;

    }


}
