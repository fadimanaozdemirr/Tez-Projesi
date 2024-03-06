using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuzzleLevelMenu : MonoBehaviour
{
	public Button[] buttons;
	private void Awake() // level seçimi kilitleme
	{
		int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].interactable = false;
		}
		for (int i = 0; i < unlockedLevel; i++)
		{
			buttons[i].interactable = true;
			buttons[i].transform.GetChild(0).GetComponent<Image>().color = Color.green;
		}
	}

	public void OpenLevel(int levelIndex)
	{
		string levelName = "PuzzleLV_" + levelIndex;
		UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
		//Debug.Log(levelName);
	}
}
