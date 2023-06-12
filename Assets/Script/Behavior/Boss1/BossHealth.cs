using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class BossHealth : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animator;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] CapsuleCollider2D capCollider;
    [SerializeField] CircleCollider2D cirCollider;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject card;
    [SerializeField] private List<Collider2D> colliders;
    [SerializeField] private List<HingeJoint2D> hingeJoints;
    [SerializeField] private List<Rigidbody2D> rigidbodies;
    [SerializeField] private List<LimbSolver2D> solvers;

    public float health;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 1)
        {
            animator.enabled = false;

            Ragdoll();
            Instantiate(card,transform.transform.position, Quaternion.identity);
            this.enabled = false;
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
            health -= 100;
        };
    }

    private void Ragdoll()
    {
        Die();
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
            rb.isKinematic = false;
        }
        this.enabled = false;
        Destroy(gameObject, 5f);
    }

    private void Die()
    {
        GetComponent<Shoot>().enabled = false;
        GetComponent<Boss1>().enabled = false;
        gun.GetComponent<Rigidbody2D>().simulated = true;
        boxCollider.enabled = false;
        cirCollider.enabled = false;
        body.simulated = false;
        gun.transform.SetParent(null);
        gameObject.GetComponent<AudioSource>().Stop();
    }
}
