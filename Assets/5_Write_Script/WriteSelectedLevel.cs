using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteSelectedLevel : MonoBehaviour
{
    public void SelectWriteLevel(int index)
	{
		PlayerPrefs.SetInt("quizDatas", index);
		UnityEngine.SceneManagement.SceneManager.LoadScene("Write");
	}
}
