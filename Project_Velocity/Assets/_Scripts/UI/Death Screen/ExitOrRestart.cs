using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitOrRestart : MonoBehaviour
{
  
    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

   public void OnClickEnd()
    {
        SceneManager.LoadScene("LevelSelect_Menu");
    }
}
