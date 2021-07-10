using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Pause")]
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    [Header("Terminal")]
    public GameObject terminalUI;
    public TextMeshProUGUI logTitle;
    public TextMeshProUGUI logBody;


    [Header("Flashing Words")]
    public GameObject flashingWords;
    public int showWordsAmount = 3;
    public float showWordsTime = 1f;
    public float closeWordsTime = 0.5f;

    int wordsCount = 0;
    bool addCount = false;

    float timerShow = 0f;
    float timerClose = 0f;
    bool shown = false;
    bool closed = false;

    private TypeWriterEffect checkEnd;


    void Start()
    {
        checkEnd = FindObjectOfType<TypeWriterEffect>();

        //Disables cursor
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;

        //show flashing words
        timerShow = showWordsTime;
    }


    void Update()
    {
        if(checkEnd.isEnded == false)
        {
            FlashingWordsManager();
        }
       

        //Pause
        if (Input.GetKeyDown(KeyCode.Escape))
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

    public void Pause()
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
        EnemyDeath.enemyCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    void ShowWords()
    {
        addCount = false;
        flashingWords.SetActive(true);
    }

    void CloseWords()
    {
        if (!addCount)
        {
            wordsCount++;
            addCount = true;
        }
        flashingWords.SetActive(false);
    }



    void FlashingWordsManager()
    {
        if (wordsCount < showWordsAmount)
        {
            if (timerShow > 0)
            {
                ShowWords();
                timerShow -= Time.deltaTime;
                closed = false;
            }
            else
            {
                if (!closed)
                {
                    timerClose = closeWordsTime;
                    closed = true;
                }
            }

            if (timerClose > 0)
            {
                CloseWords();
                timerClose -= Time.deltaTime;
                shown = false;
            }
            else
            {
                if (!shown)
                {
                    timerShow = showWordsTime;
                    shown = true;
                }
            }
        }
    }
}
