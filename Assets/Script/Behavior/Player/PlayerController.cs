using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.IK;

public class PlayerController : MonoBehaviour
{


    //Projectile
    public GameObject ProjectilePrefab, playerArm;
    public Transform LaunchOffset;

    //ability
    public bool launchUnlocked;
    public bool dodgeUnlocked;
    public float energy;
    public int health;
    public int resource;

    //Look angle
    private Vector2 lookDirection;

    //Control
    public bool isGrounded, doubleJump;
    [SerializeField] float speed, jumpForce;
    public Animator animator;
    private float horizontal,vertical;
    [SerializeField] private Rigidbody2D rb;
    private bool isFacingRight;
    private bool isDucking;
    public bool canshoot;


    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] Transform torso;
    [SerializeField] private List<Collider2D> colliders;
    [SerializeField] private List<HingeJoint2D> hingeJoints;
    [SerializeField] private List<Rigidbody2D> rigidbodies;
    [SerializeField] private List<LimbSolver2D> solvers;



    void Start()
    {
        Cursor.visible = false;
        isDucking = false;
        Time.timeScale = 1;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        Shoot();

        if (health < 1)
        {
            Ragdoll();
        }

        AnimationController();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Projectile")
        {
            health-=1;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Deathzone")
        {
            Time.timeScale = 0;
            Destroy(gameObject);
        }

    }

    private void AnimationController()
    {
        //Moving character
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            isDucking = !isDucking;
        }
        else if (Input.anyKeyDown)
        {
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
            {
                return;
            }
            else
            {
                isDucking = false;
            }
        }
        animator.SetBool("isDucking", isDucking);



        if (rb.velocity.x > 0)
        {
            isFacingRight = true;
        }
        else
        {
            isFacingRight = false;
        }

        if ((isFacingRight && lookDirection.x > 0) || (!isFacingRight && lookDirection.x < 0))
        {
            animator.SetBool("isGoingFront", true);
        }
        else
        {
            animator.SetBool("isGoingFront", false);
        }

        //double jump
        if (isGrounded && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }
        if (isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                animator.SetTrigger("Jump");
                rb.AddForce(Vector2.up * jumpForce);
                isGrounded = false;
            }
            
        }

        animator.SetFloat("Health", health);

         

    }

    private void Flip()
    {
        //Get mouse direction
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - torso.transform.position;

        //Debug.Log(lookDirection);
        //Flip character
        if (lookDirection.x > 0)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = 1;
            transform.localScale = theScale;
            isFacingRight = true;
            //transform.localRotation = Quaternion.Euler(0, 180, 0);

        }

        else
        {
            Vector3 theScale = transform.localScale;
            theScale.x = -1;
            transform.localScale = theScale;
            isFacingRight = false;
            //transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Shoot()
    {

        
        //LaunchOffset.rotation = Quaternion.Euler(0, 0, lookAngle);

        //Arm rotate
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerArm.transform.position = new Vector2(mousepos.x, mousepos.y);

        if (!canshoot)
        {
            return;
        }

        //Shoot
        if (Input.GetMouseButtonDown(0) && canshoot)
        {
            Instantiate(ProjectilePrefab, LaunchOffset.position, LaunchOffset.rotation);
        }
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
        animator.enabled = false;
        rb.simulated = false;


        boxCollider.enabled = false;
    }


}