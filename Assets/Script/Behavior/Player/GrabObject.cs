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
    public bool isHolding;
    public float range;


    // Start is called before the first frame update
    void Start()
    {
        layerIndex = LayerMask.NameToLayer("LaunchObject");
        Debug.Log(layerIndex);
    }

    // Update is called once per frame
    void Update()
    {
        /*RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.transform.position, Vector2.right, range);
        Debug.DrawRay(rayPoint.transform.position, Vector2.right, UnityEngine.Color.red);*/
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Collider2D targetObject = Physics2D.OverlapCircle(mousePosition, 5, layerIndex,-1,1);


        //Pick up object
        if (Input.GetKeyDown(KeyCode.E) && !isHolding)
        {
            if (targetObject)
            {
                grabbedObject = targetObject.transform.gameObject;
            }
            else
            {
                return;
            }

            objectRb = grabbedObject.GetComponent<Rigidbody2D>();
            objectSprite = grabbedObject.GetComponent<SpriteRenderer>();
            objectRb.isKinematic = true;
            objectSprite.color= UnityEngine.Color.red;
            grabbedObject.transform.SetParent(transform);

            AudioClip clip = hold[UnityEngine.Random.Range(0, hold.Length)];
            AudioManager.instance.PlaySFX(clip);

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

            objectSprite = grabbedObject.GetComponent<SpriteRenderer>();
            objectSprite.color = UnityEngine.Color.black;

            grabbedObject.transform.SetParent(null);
            grabbedObject = null;
            isHolding = false;
        }

        //Launch object
        if (Input.GetMouseButtonDown(0) && grabbedObject)
        {
            grabbedObject.tag = "LaunchObject";
            objectRb = grabbedObject.GetComponent<Rigidbody2D>();
            objectRb.isKinematic = false;
            objectSprite = grabbedObject.GetComponent<SpriteRenderer>();
            objectSprite.color = UnityEngine.Color.black;

            grabbedObject.transform.SetParent(null);
            objectRb.AddForce((mousePosition - grabbedObject.transform.position) * speed * objectRb.mass * 5);

            grabbedObject = null;
            isHolding = false;


            AudioClip clip = launch[UnityEngine.Random.Range(0, launch.Length)];
            AudioManager.instance.PlaySFX(clip);
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
