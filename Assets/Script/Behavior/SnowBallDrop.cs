using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallDrop : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject snowball;
    private Rigidbody2D rb;
    void Start()
    {
        rb = snowball.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.constraints = (RigidbodyConstraints2D)RigidbodyConstraints.None;

        }
    }

}
