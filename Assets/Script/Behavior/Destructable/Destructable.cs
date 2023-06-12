using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{

    public List<GameObject> fragments;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && gameObject.CompareTag("LaunchObject"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        foreach (GameObject frag in fragments)
        {
            frag.transform.parent = null;
            frag.SetActive(true);
        }

        ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();
        ef.doExplosion(transform.position);
        Destroy(gameObject);
    }
}
