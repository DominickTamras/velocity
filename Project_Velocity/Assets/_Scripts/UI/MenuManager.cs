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
    public GameObject flashingWordsObject;
    public TextMeshProUGUI flashingWordText;
    public int showWordsAmount = 3;
    public float showWordsTime = 1f;
    public float closeWordsTime = 0.5f;

    [Header("Log Chat")]
    public GameObject chatUI;
    public TextMeshProUGUI chatBody;
    
    [Header("Death menu")]
    public GameObject deathMenu;

    /*    [Header("Talking Log")]
        public GameObject talkinglog;*/



    [HideInInspector]
    public int wordsCount = 0;
    bool addCount = false;

 
     float timerShow = 0f;
     float timerClose = 0f;
     bool shown = false;
     bool closed = false;
     bool playerDead = false;

    private TypeWriterEffect checkEnd;


    void Start()
    {
        //checkEnd = FindObjectOfType<TypeWriterEffect>();

        //Disables cursor
        pauseMenuUI.SetActive(false);
        deathMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;

        //show flashing words
        timerShow = showWordsTime;
        //talkinglog.SetActive(false);
    }


    void Update()
    {
        /*if(checkEnd.isEnded == false)
        {
            FlashingWordsManager();
        }*/
        //FlashingWordsManager();

        //Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        { if (playerDead != true)
            {
                if (GameIsPaused)
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

    public void DeathScreen()
    {
        StartCoroutine(StartDeathScreen());
      /*  Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        deathMenu.SetActive(true);*/
    }

    IEnumerator StartDeathScreen()
    {
        yield return new WaitForSeconds(0.6f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        deathMenu.SetActive(true);
        playerDead = true;
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

    public void StartChat(ChatLog chatLog)
    {
        chatUI.SetActive(true);
        //Pause();
        //chatBody.text = chatLog.chatLog;
    }

    public void EndChat()
    {
        chatUI.SetActive(false);
        

    }

    public void ShowWords()
    {
        addCount = false;
        flashingWordsObject.SetActive(true);

    }

    void CloseWords()
    {
        if (!addCount)
        {
            wordsCount++;
            addCount = true;
        }
        flashingWordsObject.SetActive(false);
    }



    public void FlashingWordsManager(FlashingWordsInstance flashWords)
    {
        flashingWordText.text = flashWords.flashWords;

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
