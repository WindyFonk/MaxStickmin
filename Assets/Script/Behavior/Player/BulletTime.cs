using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class BulletTime : MonoBehaviour
{

    public bool isInBulletTime;
    private float time, countdown;

    [SerializeField] GameObject effect;
    [SerializeField] AudioClip enter,exit;
    public PlayerController player;
    private void Start()
    {
        time = 1;
        countdown = time;
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
                EnergyDecrease(0.5f);
                AudioManager.instance.PlaySFX(enter);
                Time.timeScale = 0.3f;

                if (countdown <= 0)
                {
                    countdown = time;
                    player.energy -= 15;
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

    private void EnergyDecrease(float time)
    {
        if (countdown <= 0)
        {
            countdown = time;
            player.energy -= 1;
        }
        else
        {
            countdown -= Time.deltaTime;
        }
    }

}
