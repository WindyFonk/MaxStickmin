using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialLevel : MonoBehaviour
{
    public int level;
    public GameObject loadScreen;
    public GameObject UI;
    private Animator animatorLoad;

    private void Start()
    {
        loadScreen.SetActive(true);
        animatorLoad = loadScreen.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            Reload();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(LoadNextLevel(0));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LoadNextLevel(level));
        }
    }


    public void Reload()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    IEnumerator LoadNextLevel(int level)
    {
        UI.SetActive(false);
        animatorLoad.SetTrigger("Load");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(level);
    }

    public void PlayGame()
    {
        StartCoroutine(LoadNextLevel(level));
    }
}
