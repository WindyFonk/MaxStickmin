using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public GameObject ButtonWidget;
    private bool inRange;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                LoadNextLevel();
            }
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ButtonWidget.gameObject.SetActive(true);
        inRange= true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ButtonWidget.gameObject.SetActive(false);
        inRange = false;
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelIndex);

    }
}
