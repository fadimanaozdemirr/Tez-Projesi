using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WordsLevelMenu : MonoBehaviour
{

	public Button[] buttons;


	private void Awake() // level se�imi kilitleme
	{
		int unlockedLevelKelime = PlayerPrefs.GetInt("UnlockedLevelKelime", 1);
		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].interactable = false;
		}
		for (int i = 0; i < unlockedLevelKelime; i++)
		{
			buttons[i].interactable = true;
			buttons[i].transform.GetChild(0).GetComponent<Image>().color = Color.green;
		}
	}

	public void OpenLevel(int levelIndex)
	{
		string levelName = "Kelime_" + levelIndex;
		UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
		//Debug.Log(levelName);
	}


}
