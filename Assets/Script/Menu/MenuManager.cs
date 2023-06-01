using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject LevelButtons;
    public GameObject MenuButtons;
    public GameObject OptionButtons;
    public void PlayGame()
    {
        MenuButtons.SetActive(false);
        LevelButtons.SetActive(true);
    }

    public void Option()
    {
        MenuButtons.SetActive(false);
        OptionButtons.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        GameObject current = GameObject.FindGameObjectWithTag("CurrentButtons");
        current.SetActive(false);
        MenuButtons.SetActive(true);
    }

    public void LoadLevel(int lv)
    {
        switch (lv)
        {
            case 0:
                PlayerPrefs.SetInt("first", 1);
                PlayerPrefs.Save();
                break;
        }
        SceneManager.LoadScene(lv+1);
    }
}
