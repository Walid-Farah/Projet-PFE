using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static bool gamePaused=false;
    public GameObject pauseMenuUI;
    //public GameObject LoadSce

    void Start()
    {

    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused) {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void Quitter()
    {
        Resume();
        DataBase.nomPersonne = "Test";
        DataBase.prenomPersonne = "Test";
        DataBase.niveauPersonne = 0;
        SceneManager.LoadScene("MainMenu");
    }


    public void Retourner()
    {
        Resume();
        SceneManager.LoadScene("SampleScene");
    }

    public void RetournerMoteur()
    {
        Resume();
        SceneManager.LoadScene("Moteur");
    }

    
    
}
