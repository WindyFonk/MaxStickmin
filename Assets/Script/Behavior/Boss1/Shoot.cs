using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Rigidbody2D projectile;
    public GameObject barrel;
    private float timeBetweenShot;
    public float startTimeBetweenShot;

    //sfx
    [SerializeField] AudioClip gunshoot;
    void Start()
    {
        timeBetweenShot = startTimeBetweenShot;
    }

    // Update is called once per frame
    void Update()
    {
        GunShoot();
    }

    private void GunShoot()
    {
        if (timeBetweenShot <= 0)
        {
            AudioManager.instance.PlaySFX(gunshoot);
            Rigidbody2D p = Instantiate(projectile, barrel.transform.position, transform.rotation);
            startTimeBetweenShot = NextFloat(1, 3);
            timeBetweenShot = startTimeBetweenShot;
        }
        else
        {
            timeBetweenShot -= Time.deltaTime;
        }
    }

    static float NextFloat(float min, float max)
    {
        System.Random random = new System.Random();
        double val = (random.NextDouble() * (max - min) + min);
        return (float)val;
    }
}
