using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Lockbox lockbox;
    // Update is called once per frame
    void Update()
    {
        if (lockbox.unlocked)
        {
            animator.SetTrigger("Unlock");
        }
    }
}
