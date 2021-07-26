using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New ChatLog", menuName = "Chat Log")]
public class ChatLog : ScriptableObject
{
    [TextArea]
    public string chatLog;
}

