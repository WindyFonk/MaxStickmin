using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialLevel : MonoBehaviour
{
    [SerializeField] GameObject Loadingscreen;
    private Animator loadAnimator;

    private void Start()
    {
        loadAnimator= Loadingscreen.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            Reload();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            loadAnimator.SetTrigger("Load");
            StartCoroutine(LoadLV1());
        }
    }


    public void Reload()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    IEnumerator LoadLV1()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }
}
