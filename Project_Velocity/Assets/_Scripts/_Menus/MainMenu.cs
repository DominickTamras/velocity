using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject mainmenu;
    public GameObject levelSelect;
    public GameObject options;
    public void OnClickStartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnclickExit()
    {

    }


    public void OnClickLevelSelect()
    {
        mainmenu.SetActive(false);
        levelSelect.SetActive(true);
    }

    public void OnClickOptions()
    {
        mainmenu.SetActive(false);
        options.SetActive(true);
    }

    public void OnClickReturnOptions()
    {
        options.SetActive(false);
        mainmenu.SetActive(true);
    }

    public void OnClickReturnLevelSelect()
    {
        levelSelect.SetActive(false);
        mainmenu.SetActive(true);
    }
}
