using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    int yerlestirilen_parca = 0;
	int toplam_puzzle = 16;
    [SerializeField] GameObject correctSound;
    void Start()
    {
        correctSound = GameObject.FindGameObjectWithTag("CorrectSound").gameObject;
    }

    public void sayi_arttir()
	{
        yerlestirilen_parca++;
		if (yerlestirilen_parca==toplam_puzzle)
		{
            correctSound.GetComponent<AudioSource>().Play();
            Invoke("yenilevelgec",1);
            Debug.Log("SONRAKÝ LEVEL");
        }
	}
    void yenilevelgec()
    {
        NextLevel.instance.UnlockNewLevel();
        NextLevel.instance.NextLevelLoad();
    }

    void Update()
    {
        
    }
}
