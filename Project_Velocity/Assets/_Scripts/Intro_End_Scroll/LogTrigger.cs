using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogTrigger: MonoBehaviour
{
    public ChatLog chatLog;
    public MenuManager manager;


    public void StartChat()
    {
        if (chatLog != null)
        {
            manager.StartChat(chatLog);
        }

        else
        {
            Debug.LogError("This terminal is missing a log scriptable object!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartChat();
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
