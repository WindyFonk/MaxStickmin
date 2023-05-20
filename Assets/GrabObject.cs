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

    private GameObject grabbedObject;
    private int layerIndex;
    public float speed;
    private Rigidbody2D objectRb;
    public bool isHolding;
    public float range;


    // Start is called before the first frame update
    void Start()
    {
        layerIndex = LayerMask.NameToLayer("LaunchObjects");
    }

    // Update is called once per frame
    void Update()
    {
        /*RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.transform.position, Vector2.right, range);
        Debug.DrawRay(rayPoint.transform.position, Vector2.right, UnityEngine.Color.red);*/
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Pick up object
        if (Input.GetKeyDown(KeyCode.E) && !isHolding)
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition,7);
            if (targetObject)
            {
                grabbedObject = targetObject.transform.gameObject;
                Debug.Log("OnObject");
            }
            else
            {
                return;
            }

            objectRb = grabbedObject.GetComponent<Rigidbody2D>();
            objectRb.isKinematic = true;

            grabbedObject.transform.SetParent(transform);


            isHolding = true;
        }

        if (isHolding)
        {
            grabbedObject.transform.position = Vector2.MoveTowards(grabbedObject.transform.position,
        holdTransform.position, Time.deltaTime * speed);
            player.canshoot = false;
        }
        else
        {
            player.canshoot = true;
        }

        //Drop object
        if (Input.GetKeyDown(KeyCode.F) && grabbedObject)
        {
            objectRb = grabbedObject.GetComponent<Rigidbody2D>();
            objectRb.isKinematic = false;

            grabbedObject.transform.SetParent(null);
            grabbedObject = null;
            isHolding= false;
        }

        //Launch object
        if (Input.GetMouseButtonDown(0) && grabbedObject)
        {
            objectRb = grabbedObject.GetComponent<Rigidbody2D>();
            objectRb.isKinematic = false;

            grabbedObject.transform.SetParent(null);
            objectRb.AddForce((mousePosition- grabbedObject.transform.position)*speed*objectRb.mass*3);

            grabbedObject = null;
            isHolding = false;
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
