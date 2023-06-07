using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Explodable))]

public class DestructCollide : MonoBehaviour
{
    private Explodable _explodable;
    public bool ExplodeByLaunchObject;
    public bool ExplodeOnEnemyTough;

    // Start is called before the first frame update
    void Start()
    {
        _explodable = GetComponent<Explodable>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Corpse") && ExplodeOnEnemyTough)
        {
            Explode();
        }
        
        else if (collision.gameObject.CompareTag("Enemy") && gameObject.CompareTag("LaunchObject"))
        {
            Explode();
        }

        if (collision.gameObject.CompareTag("LaunchObject") && ExplodeByLaunchObject)
        {
            Explode();
        }
    }

    private void Explode()
    {
        _explodable.explode();
        ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();
        ef.doExplosion(transform.position);
    }
}
