using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryLevelMenu : MonoBehaviour
{
	public Button[] buttons;


	private void Awake() // level seçimi kilitleme
	{
		int unlockedLevelMemory = PlayerPrefs.GetInt("UnlockedLevelMemory", 1);
		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].interactable = false;
		}
		for (int i = 0; i < unlockedLevelMemory; i++)
		{
			if (buttons[i])
			{

			buttons[i].interactable = true;
			buttons[i].transform.GetChild(0).GetComponent<Image>().color = Color.green;
		}
		}
	}

	public void OpenLevel(int levelIndex)
	{
		string levelName = "Hafiza_" + levelIndex;
		UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
		//Debug.Log(levelName);
	}
}
