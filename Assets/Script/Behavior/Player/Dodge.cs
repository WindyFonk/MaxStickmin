using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    private Animator animator;
    [SerializeField] GameObject effect;
    [SerializeField] AudioClip[] sound;
    private bool canDodge = true;
    public PlayerController player;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DodgeActive();
    }

    private void DodgeActive()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDodge && player.energy>10)
        {
            animator.SetTrigger("Dodge");
            GameObject dodgeEffect = Instantiate(effect);
            Destroy(dodgeEffect, 1f);
            AudioClip clip = sound[UnityEngine.Random.Range(0, sound.Length)];
            AudioManager.instance.PlaySFX(clip);
            canDodge = false;
            Invoke("setDodge", 1f);
            player.energy -= 10;
        }
    }

    private void setDodge()
    {
        canDodge = true;
    }


}
