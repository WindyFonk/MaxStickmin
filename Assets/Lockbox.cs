using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockbox : MonoBehaviour
{
    [SerializeField] Animator lockedDoor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LaunchObject"))
        {
            lockedDoor.SetTrigger("Unlock");
        }
    }
}
