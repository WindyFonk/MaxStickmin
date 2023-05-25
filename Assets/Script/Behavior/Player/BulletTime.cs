using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BulletTime : MonoBehaviour
{

    private bool isInBulletTime;
    private float time, countdown;

    [SerializeField] GameObject effect;
    [SerializeField] AudioClip enter,exit;
    private PlayerController player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        BulletTimeActive();
    }

    private void BulletTimeActive()
    {
        if (player.energy <= 0)
        {
            isInBulletTime = false;
            return;
        }

        //Bullet time
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isInBulletTime = !isInBulletTime;

            if (isInBulletTime)
            {
                AudioManager.instance.PlaySFX(enter);
                Time.timeScale = 0.3f;

                if (countdown <= 0)
                {
                    time = 0.1f;
                    countdown = time;
                    player.energy -= 1;
                }
                else
                {
                    countdown -= Time.deltaTime;
                }
            }
            else
            {
                AudioManager.instance.PlaySFX(exit);
                Time.timeScale = 1f;
            }

            
        }

        

        effect.GetComponent<Animator>().SetBool("isInBulletTime", isInBulletTime);
    }

}
