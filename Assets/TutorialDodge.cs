using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDodge : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] BulletTime bulletTime;
    [SerializeField] GrabObject grabObject;
    [SerializeField] GameObject textDodge;
    float time;
    void Start()
    {
        time = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.enabled)
        {
            time -= Time.deltaTime;
        }

        if (textDodge.activeInHierarchy&& Input.GetKeyUp(KeyCode.LeftShift) || time<0) 
        {
            Time.timeScale = 1;
            player.enabled = true;
            bulletTime.enabled = true;
            grabObject.enabled = true;
            textDodge.SetActive(false);
            Destroy(gameObject,0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Time.timeScale = 0.1f;
            player.enabled= false;
            bulletTime.enabled= false;
            grabObject.enabled= false;
            textDodge.SetActive(true);
        }
    }
}
