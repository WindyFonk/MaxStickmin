using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatPlatForm : MonoBehaviour
{
    public bool collide;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (collide)
        {
            transform.Translate(new Vector3(0, Time.deltaTime * 1 * 10, 0));
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Card"))
        {
            collide= true;
        }
        
    }
}
