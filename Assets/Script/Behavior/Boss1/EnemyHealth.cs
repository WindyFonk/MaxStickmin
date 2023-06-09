using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class EnemyHealth : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animator;
    [SerializeField] BoxCollider2D boxCollider;
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
            Instantiate(card,transform.position, Quaternion.identity);
            Die();
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

    private void Ragdoll()
    {
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
        this.enabled = false;
        Destroy(gameObject, 15f);
    }

    private void Die()
    {
        Ragdoll();
        animator.enabled = false;
        boxCollider.enabled = false;
        GetComponent<Shoot>().enabled = false;
        GetComponent<Boss1>().enabled = false;
        gun.GetComponent<Rigidbody2D>().simulated = true;
        body.simulated = false;
        gun.transform.SetParent(null);
    }
}
