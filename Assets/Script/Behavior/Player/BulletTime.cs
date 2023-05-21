using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BulletTime : MonoBehaviour
{

    private bool isInBulletTime;
    private float time, countdown;
    [SerializeField] GameObject effect;
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
        //Bullet time
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isInBulletTime = !isInBulletTime;
        }
        if (isInBulletTime)
        {
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
            Time.timeScale = 1f;
        }

        if (player.energy<=0)
        {
            isInBulletTime= false;
        }

        effect.GetComponent<Animator>().SetBool("isInBulletTime", isInBulletTime);
    }

}
