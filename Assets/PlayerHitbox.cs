using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            player.health -= 1;
        }
    }
}
