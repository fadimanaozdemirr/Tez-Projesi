using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WordsNextLevel : MonoBehaviour
{
	public static WordsNextLevel instance;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void NextLevelLoad()
	{
		Debug.Log("Diðer levele geçti");
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}
	public void LoadScene(string levelName)
	{
		Debug.Log("yeni level: " + levelName);
		SceneManager.LoadSceneAsync(levelName);
	}
	public void UnlockNewLevel()
	{
		if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndexKelime"))
		{
			PlayerPrefs.SetInt("ReachedIndexKelime", SceneManager.GetActiveScene().buildIndex + 1);
			PlayerPrefs.SetInt("UnlockedLevelKelime", PlayerPrefs.GetInt("UnlockedLevelKelime", 1) + 1);
			PlayerPrefs.Save();
		}
	}
}
