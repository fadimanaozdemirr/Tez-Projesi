using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WordsController : MonoBehaviour
{
    public string[] sozluk;
    public TMPro.TextMeshProUGUI puan_txt;

    List<GameObject> isaretli_butonlar; // yeþil butonlarý tutuyoruz

    string kelime = null;
    public bool tiklandi = false;

    public GameObject bittiPanel;

    int puan = 0;
    int bulunan_kelime_sayisi = 0;
    [SerializeField] GameObject correctSound;
    [SerializeField] private GameObject gameOver;


    void Start()
    {
        correctSound = GameObject.FindGameObjectWithTag("CorrectSound").gameObject;
        isaretli_butonlar = new List<GameObject>();
    }


    public void isaretli_buton_olustur(GameObject Buton)
	{
        isaretli_butonlar.Add(Buton);

        kelime = null;

		foreach (GameObject butonlar in isaretli_butonlar)
		{
            kelime = kelime + butonlar.name;
            puan_txt.text = kelime;
		}
	}
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tiklandi = true;
            Debug.Log("týklandý");

        }

        if (Input.GetMouseButtonUp(0))
		{
            tiklandi = false;

            yazi_olustur();

            puan_txt.text = puan.ToString();
		}
    }

    void yazi_olustur()
	{
		foreach (string kelimeler in sozluk)
		{
			if (kelimeler == kelime)
			{
                correctSound.GetComponent<AudioSource>().Play();
                Debug.Log("doðru");
                puan += 100;
                bulunan_kelime_sayisi++;

				foreach (GameObject Buton in isaretli_butonlar)
				{
                    Buton.GetComponent<Buton>().yok_ol = true;
				}
			}
		}

        isaretli_butonlar.Clear();
        kelime = null;
		if (bulunan_kelime_sayisi==sozluk.Length)
		{
            Debug.Log("OYUN BÝTTÝ");

            if (SceneManager.GetActiveScene().name == "Kelime_8")
            {
                if (gameOver)
                {
                    gameOver.gameObject.SetActive(true);

                }

            }
            else
            {
                Invoke("NxtLevel", .5f);

            }


            //WordsNextLevel.instance.UnlockNewLevel();
            //WordsNextLevel.instance.NextLevelLoad();
        }
	}
    void NxtLevel()
    {
        WordsNextLevel.instance.UnlockNewLevel();
        WordsNextLevel.instance.NextLevelLoad();
    }
}
