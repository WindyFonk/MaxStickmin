using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.U2D.IK;
using static UnityEngine.ParticleSystem;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] float speed;
    public float health;
    public GameObject Arm;
    private GameObject player;
    private bool isAlive;
    public bool shoot, isAiming;
    private Vector2 lookDirection;


    public Rigidbody2D projectile;
    public GameObject barrel;
    private float timeBetweenShot;
    public float startTimeBetweenShot;
    public bool isFacingRight;
    public float range;

    private Rigidbody2D body;
    private Animator animator;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] GameObject gun;
    [SerializeField] CircleCollider2D cirCollider;
    [SerializeField] private List<Collider2D> colliders;
    [SerializeField] private List<HingeJoint2D> hingeJoints;
    [SerializeField] private List<Rigidbody2D> rigidbodies;
    [SerializeField] private List<LimbSolver2D> solvers;

    //sfx
    [SerializeField] AudioClip gunshoot;


    public static class EnemyBehaviorStatic
    {
        public static EnemyBehavior enemyBehavior;
    }

    void Start()
    {
        timeBetweenShot = startTimeBetweenShot;
        EnemyBehaviorStatic.enemyBehavior = this;
        player = GameObject.FindGameObjectWithTag("Torso");
        isAlive = true;
        isFacingRight = false;
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        shoot = Range();
        isAiming = Range();

        if (isAlive && isAiming)
        {
            Aim();

            if (shoot)
            {
                Shoot();
            }
        }
        else if (!isAlive)
        {
            Ragdoll();
        }

        if (health<=0)
        {
            isAlive = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            health -= 60;
        };

        if (collision.gameObject.CompareTag("LaunchObject"))
        {
            health -= 120;
        };
    }




    private void Aim()
    {
        //Arm rotate
        Arm.transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
        lookDirection = player.transform.position - transform.position;

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
    }

    private void Ragdoll()
    {
        Invoke("DisableCollider", 0.5f);
        DisableCollider();
        GetComponent<IKManager2D>().enabled = false;
        foreach (var col in colliders)
        {
            col.enabled = true;
        }

        foreach (var joints in hingeJoints)
        {
            joints.enabled = true;
        }

        foreach (var rb in rigidbodies)
        {
            rb.simulated = true;
        }
        this.enabled= false;
        Destroy(gameObject, 30f);
    }

    private void DisableCollider()
    {
        gun.GetComponent<Rigidbody2D>().simulated = true;
        boxCollider.enabled = false;
        cirCollider.enabled = false;
        body.simulated= false;
        animator.enabled= false;
        gun.transform.SetParent(null);
    }

    private void Shoot()
    {
        if (timeBetweenShot <= 0)
        {
            AudioManager.instance.PlaySFX(gunshoot);
            Rigidbody2D p = Instantiate(projectile, barrel.transform.position, transform.rotation);
            startTimeBetweenShot = NextFloat(1,3);
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

    private bool Range()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log(distance);
        if (distance < range)
        {
            return true;
        }
        return false;
    }


}
