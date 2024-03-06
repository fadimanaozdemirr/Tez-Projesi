using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleController3 : MonoBehaviour
{
    int yerlestirilen_parca = 0;
    int toplam_parca = 9;
    [SerializeField] GameObject correctSound;

    void Start()
    {
        correctSound = GameObject.FindGameObjectWithTag("CorrectSound").gameObject;
    }
    public void sayi_arttir()
    {
        Debug.Log(yerlestirilen_parca+"askýmmm"+toplam_parca);
        yerlestirilen_parca++;
        if (yerlestirilen_parca == toplam_parca)
        {
            correctSound.GetComponent<AudioSource>().Play();
            Invoke("yenilevelgec", 1);
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
