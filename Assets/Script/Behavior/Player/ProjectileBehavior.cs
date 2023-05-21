using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileBehavior : MonoBehaviour
{
    public float speed = 15f;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    [SerializeField] GameObject gun;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        Destroy(gameObject, 2f); 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        sprite.enabled = false;
        Destroy(gameObject,0.1f);
    }
}
