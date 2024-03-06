using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleController25 : MonoBehaviour
{
    int yerlestirilen_parca = 0;
    int top_parca = 25;
    [SerializeField] GameObject correctSound;
    [SerializeField] private GameObject gameOver;

    void Start()
    {
        correctSound = GameObject.FindGameObjectWithTag("CorrectSound").gameObject;
    }

    public void sayi_arttir()
	{
        yerlestirilen_parca++;
        if (yerlestirilen_parca == top_parca)
        {
            correctSound.GetComponent<AudioSource>().Play();
            Debug.Log("SONRAKÝ LEVEL");
			if (SceneManager.GetActiveScene().name == "PuzzleLV_8")
			{
				if (gameOver)
				{
					gameOver.gameObject.SetActive(true);
				}
			}
		}
	}
    
    void Update()
    {
        
    }
}
