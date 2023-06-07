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
    private float horizontal, vertical;
    private Rigidbody2D rb;
    private bool isFacingRight;
    private bool isDucking;
    public bool canshoot = false;
    private float countdown;

    //sfx
    [SerializeField] AudioClip gunshoot;
    [SerializeField] AudioClip death;

    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] Transform torso;
    [SerializeField] private List<Collider2D> colliders;
    [SerializeField] private List<HingeJoint2D> hingeJoints;
    [SerializeField] private List<Rigidbody2D> rigidbodies;
    [SerializeField] private List<LimbSolver2D> solvers;

    //UI
    public HealthBar healthBar;
    public EnergyBar energyBar;

    [SerializeField] GameObject cursor;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject UI;




    void Start()
    {
        Cursor.visible = false;
        isDucking = false;
        Time.timeScale = 1;
        animator = GetComponent<Animator>();
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
     void Update()
    {
        Flip();
       // Shoot();

        if (health < 1)
        {
            Ragdoll();
            energy -= 100;
            StartCoroutine(Die());  
        }

        healthBar.setHealth(health);
        energyBar.setHealth(energy);
        EnergyRecover(1);

        AnimationController();

        Debug.Log(rb.velocity.y);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Lock" || collision.gameObject.tag == "Corpse")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Shield")
        {
            health -= 100;
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
        if (Input.GetButtonDown("Jump"))
        {
            //animator.ResetTrigger("Jump");
            if (isGrounded || doubleJump)
            {
                isGrounded = true;
                isGrounded = false;
                //animator.SetTrigger("Jump");
                //rb.AddForce(Vector2.up * jumpForce);
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                doubleJump = !doubleJump;
            }
        }

        animator.SetBool("Jump",!isGrounded);
        animator.SetFloat("Health", health);



    }

    private void Flip()
    {
        //Get mouse direction
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - torso.transform.position;

        //Debug.Log(lookDirection);
        //Flip character


        //Arm rotate
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerArm.transform.position = new Vector2(mousepos.x, mousepos.y);
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
        

        if (!canshoot)
        {
            return;
        }

        //Shoot
        if (Input.GetMouseButtonDown(0) && canshoot)
        {
            AudioManager.instance.PlaySFX(gunshoot);
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

    private void EnergyRecover(float time)
    {
        if (countdown <= 0)
        {
            countdown = time;
            energy += 1;
        }
        else
        {
            countdown -= Time.deltaTime;
        }

        if (energy >= 100)
        {
            energy = 100;
        }
    }

    IEnumerator Die()
    {
        AudioManager.instance.PlaySFX(death);
        Time.timeScale = 0.3f;
        cursor.SetActive(false);
        yield return new WaitForSeconds(1);
        Time.timeScale = 1f;
        UI.SetActive(false);
        gameOver.SetActive(true);
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;


    }
}
