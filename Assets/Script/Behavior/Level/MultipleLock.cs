using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleLock : MonoBehaviour
{
    public bool unlocked;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(unlocked);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Key1"))
        {
            unlocked = true;
        }
    }
}
