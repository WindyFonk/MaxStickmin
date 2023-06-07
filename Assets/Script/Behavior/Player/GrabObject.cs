using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;
public class GrabObject : MonoBehaviour

{
    [SerializeField] private Transform holdTransform;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance;
    [SerializeField] private PlayerController player;
    [SerializeField] AudioClip[] hold;
    [SerializeField] AudioClip[] launch;


    private GameObject grabbedObject;
    private int layerIndex;
    public float speed;
    private Rigidbody2D objectRb;
    private SpriteRenderer objectSprite;
    private Collider2D objectCollider;
    public bool isHolding;
    public float range;
    public LayerMask mask;
    public LayerMask grabmask;
    private float ogGravity;
    public float rollSpeed;
    public string ogTag;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(layerIndex);
    }

    // Update is called once per frame
    void Update()
    {
        /*RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.transform.position, Vector2.right, range);
        Debug.DrawRay(rayPoint.transform.position, Vector2.right, UnityEngine.Color.red);*/
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Collider2D targetObject = Physics2D.OverlapCircle(mousePosition, 3, mask);

        //Pick up object
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(1)) && !isHolding && player.energy>10)
        {
            if (targetObject)
            {
                player.energy -= 5;
                grabbedObject = targetObject.transform.gameObject;
            }
            else
            {
                return;
            }
            ogTag = grabbedObject.tag;
            objectRb = grabbedObject.GetComponent<Rigidbody2D>();
            objectSprite = grabbedObject.GetComponent<SpriteRenderer>();
            objectCollider = grabbedObject.GetComponent<Collider2D>();
            ogGravity = objectRb.gravityScale;
            objectRb.gravityScale = 0;
            grabbedObject.transform.SetParent(transform);

            grabbedObject.layer = LayerMask.NameToLayer("GrabbedObject");
            // objectCollider.enabled = false;

            AudioClip clip = hold[UnityEngine.Random.Range(0, hold.Length)];
            AudioManager.instance.PlaySFX(clip);

            isHolding = true;
        }

        if (isHolding && grabbedObject)
        {
            grabbedObject.transform.position = Vector2.MoveTowards(grabbedObject.transform.position,
        holdTransform.position, Time.deltaTime * speed);
            //objectRb.MovePosition(holdTransform.position);

            player.canshoot = false;
            objectRb.velocity = Vector3.zero;

            //Rotate Holding Object
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                grabbedObject.transform.Rotate(new Vector3(0,0,1) * rollSpeed, Space.Self);
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                grabbedObject.transform.Rotate(new Vector3(0, 0, -1) * rollSpeed, Space.Self);
            }
        }
        else
        {
            player.canshoot = true;
        }

        //Drop object
        if (Input.GetKeyDown(KeyCode.F) && grabbedObject)
        {
            grabbedObject.layer = LayerMask.NameToLayer("LaunchObject");

            player.energy += 3;
            grabbedObject.tag = ogTag;

            objectRb = grabbedObject.GetComponent<Rigidbody2D>();
            objectRb.isKinematic = false;

            objectSprite = grabbedObject.GetComponent<SpriteRenderer>();
            objectCollider.enabled = true;

            objectRb.angularVelocity = 1;
            objectRb.gravityScale = ogGravity;

            grabbedObject.transform.SetParent(null);
            grabbedObject = null;
            isHolding = false;
        }

        //Launch object
        if (Input.GetMouseButtonDown(0) && grabbedObject)
        {
            grabbedObject.layer = LayerMask.NameToLayer("LaunchObject");
            grabbedObject.tag = "LaunchObject";
            objectRb = grabbedObject.GetComponent<Rigidbody2D>();
            objectCollider.isTrigger = false;
            objectRb.isKinematic = false;
            objectSprite = grabbedObject.GetComponent<SpriteRenderer>();
            objectCollider.enabled = true;
            objectRb.gravityScale = ogGravity;


            grabbedObject.transform.SetParent(null);
            objectRb.AddForce((mousePosition - grabbedObject.transform.position) * speed * objectRb.mass * 5);

            grabbedObject = null;
            isHolding = false;


            AudioClip clip = launch[UnityEngine.Random.Range(0, launch.Length)];
            AudioManager.instance.PlaySFX(clip);
        }


        if (grabbedObject == null)
        {
            isHolding = false;
            return;
        }



        /*if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == layerIndex)
        {
            grabbedObject = hitInfo.collider.gameObject;
            if (Input.GetKey(KeyCode.E) && !isHolding)
            {
                grabbedObject = hitInfo.collider.gameObject;

                objectRb = grabbedObject.GetComponent<Rigidbody2D>();
                objectRb.isKinematic = true;

                grabbedObject.transform.SetParent(transform);
                

                isHolding= true;
            }
            else if (Input.GetKey(KeyCode.F) && isHolding)
            {
                objectRb = grabbedObject.GetComponent<Rigidbody2D>();
                objectRb.isKinematic = false;

                grabbedObject.transform.SetParent(null);
                grabbedObject = null;

                isHolding = false;

            }

            if (isHolding)
            {
                grabbedObject.transform.position = Vector2.MoveTowards(grabbedObject.transform.position,
            holdTransform.position, Time.deltaTime * speed);
            }*/

    }


}
