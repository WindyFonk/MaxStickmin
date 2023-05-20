using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ParticleSystem particle;
    [SerializeField] AudioClip breakSfx;
    private AudioSource audiosource;
    [SerializeField] bool containGold;
    public PlayerController playerController;
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            gameObject.SetActive(false);
            ParticleSystem particlesystem = Instantiate<ParticleSystem>(particle,new Vector2 (transform.position.x,transform.position.y), Quaternion.identity);
            particlesystem.Play();
            Destroy(gameObject,3f);
        }
    }
}
