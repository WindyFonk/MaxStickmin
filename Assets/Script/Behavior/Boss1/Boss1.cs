using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    private Transform player;
    private Vector2 lookDirection;
    private GameObject shield;
    public bool shieldOn;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shield = GameObject.FindGameObjectWithTag("Shield");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FacePlayer();
        animator.SetBool("shieldOn", shieldOn);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LaunchObject"))
        {
            Destroy(collision.gameObject);
        }
    }
    private void FacePlayer()
    {
        lookDirection = player.position - transform.position;
        //flip
        if (lookDirection.x > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            // Player.GetComponent<SpriteRenderer>().flipX = true;

        }

        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            //Player.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (shield == null)
        {
            shieldOn = false;
        }
    }

}
