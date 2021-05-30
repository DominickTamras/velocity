using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    public GameObject terminalUI;
    public TextMeshProUGUI logTitle;
    public TextMeshProUGUI logBody;

    void Start()
    {
        //Disables cursor
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                OpenPauseMenu();
                Pause();
            }
        }
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameIsPaused = true;
        Time.timeScale = 0f;
    }

    public void EndPause()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameIsPaused = false;
        Time.timeScale = 1f;
    }

    void OpenPauseMenu()
    {
        pauseMenuUI.SetActive(true);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        terminalUI.SetActive(false);
        EndPause();
    }

    public void Restart()
    {
        EndPause();
        SceneManager.LoadScene("Movement_Playtest_Map");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenTerminal(TerminalLog log)
    {
        //Game paused
        terminalUI.SetActive(true);
        Pause();

        //Open termal menu
        logTitle.text = log.title;
        logBody.text = log.log;
    }
}
