using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	public void Home()
	{
		Invoke("mainMenu", .5f);
	}
	void mainMenu()
	{
		SceneManager.LoadScene("mainMenu");
		Time.timeScale = 1;
	}
}
