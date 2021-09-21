using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
    public GameObject loadscreenBG;
    public Image loadscreenFill;
    public Slider slide;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel (int sceneIndex)
    {
        StartCoroutine(LoadAsynch(sceneIndex));
    }

    IEnumerator LoadAsynch(int sceneIndex)
    {
 
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadscreenBG.SetActive(true);

        while(!operation.isDone)
        {
         
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadscreenFill.color = Color.Lerp(Color.black, Color.white, progress);
            slide.value = progress;
            Debug.Log(progress);
            yield return null;
        }
    }
}
