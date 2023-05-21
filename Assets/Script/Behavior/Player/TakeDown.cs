using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDown : MonoBehaviour
{
    public GameObject key;
    public GameObject player;
    public GameObject Enemy;
    private Animator playerAnimator, enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = player.GetComponent<Animator>();
        enemyAnimator= Enemy.GetComponent<Animator>();
    }
    private void Takedown()
    {

        if (Input.GetKeyDown(KeyCode.F)) 
        {
            playerAnimator.SetTrigger("TakeDown");
            enemyAnimator.SetTrigger("TakeDown");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        key.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        key.SetActive(false);
    }

    
}
