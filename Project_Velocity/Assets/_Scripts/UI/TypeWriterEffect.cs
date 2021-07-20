﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class TypeWriterEffect : MonoBehaviour {

	public float delay = 0.1f;
	[TextArea]
	public string fullText;
	private string currentText = "";
	public float timeChange;
	public GameObject imageBg;

	public MenuManager pause;

	public GameObject player;
	
	[HideInInspector]
	public bool isEnded;


	// Use this for initialization
	void Start () 
	{
		isEnded = true;
		StartCoroutine(ShowText());
		player.SetActive(false);
	

	}

    private void Update()
    {


	}

    IEnumerator ShowText()
	{
			

		for (int i = 0; i < fullText.Length; i++) 
			{
				currentText = fullText.Substring(0, i);
				this.GetComponent<TextMeshProUGUI>().text = currentText;
				yield return new WaitForSeconds(delay);

			}

		yield return new WaitForSeconds(timeChange);

		player.SetActive(true);

		pause.TalkLog();
		/*imageBg.SetActive(false);
		this.GetComponent<TextMeshProUGUI>().enabled = false;
		isEnded = false;*/
		//Destroy(this.gameObject);
		

	}
}
