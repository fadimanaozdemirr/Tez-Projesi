using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MemoryController : MonoBehaviour
{
	[SerializeField] private Sprite bgImage;

	public Sprite[] puzzels;

	public List<Sprite> gameMemorys = new List<Sprite>();

    public List<Button> btns = new List<Button>();

	private bool firstGuess, secondGuess;

	private int countGuesses;
	private int countCorrectGuesses;
	private int gameGuesses;

	private int firstGuessIndex, secondGuessIndex;

	private string firstGuessMemory, secondGuessMemory;

	[SerializeField]GameObject correctSound;
	[SerializeField] GameObject kartSound;
	[SerializeField] private GameObject gameOver;

	private void Awake()
	{
		//puzzels = Resources.LoadAll<Sprite>("hafiza/animal"); // e�le�ecek resimleri ald�k. s�r�klemek yerine kodla yapt�k
		
		string sahneAdi = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

		switch (sahneAdi)
		{
			case "Hafiza_1":
				puzzels = Resources.LoadAll<Sprite>("hafiza/animal");
				break;

			case "Hafiza_2":
				puzzels = Resources.LoadAll<Sprite>("hafiza/uzay");
				break;

			case "Hafiza_3":
				puzzels = Resources.LoadAll<Sprite>("hafiza/kiyafet");
				break;

			case "Hafiza_4":
				puzzels = Resources.LoadAll<Sprite>("hafiza/balik");
				break;

			case "Hafiza_5":
				puzzels = Resources.LoadAll<Sprite>("hafiza/korsan");
				break;

			case "Hafiza_6":
				puzzels = Resources.LoadAll<Sprite>("hafiza/okul");
				break;

			case "Hafiza_7":
				puzzels = Resources.LoadAll<Sprite>("hafiza/food");
				break;

			case "Hafiza_8":
				puzzels = Resources.LoadAll<Sprite>("hafiza/garden");
				break;

		}
		
	}

	void Start()
	{
		correctSound = GameObject.FindGameObjectWithTag("CorrectSound").gameObject;
		kartSound = GameObject.FindGameObjectWithTag("KartSound").gameObject;
		GetButtons();
		AddListeners();
		AddGamePuzzles();
		Shuffle(gameMemorys);
		gameGuesses = gameMemorys.Count / 2; // gameGuess olmas� gereken toplam do�ru tahmin say�s�
	}

	void GetButtons()
	{
		GameObject[] objects = GameObject.FindGameObjectsWithTag("MemoryButton");

		for (int i = 0; i < objects.Length; i++)
		{
			btns.Add(objects[i].GetComponent<Button>());
			btns[i].image.sprite = bgImage; // buton bg lerini buradan de�i�tir.
		}
	}

	void AddGamePuzzles()
	{
		int looper = btns.Count; // e�le�ecek resimlerin hepsini hepsini ald�k ve buton say�s� kadar�n� se�tik
		int index = 0;

		for (int i = 0; i < looper; i++)
		{
			if (index == looper / 2) // her resimden 2 tane ekledik ki e�le�tirebilelim
			{
				index = 0;
			}
			gameMemorys.Add(puzzels[index]);
			index++;
		}
	}

	void AddListeners()
	{
		foreach (Button btn in btns) // butonlar�n t�klanma �zelli�i verildi
		{
			btn.onClick.AddListener(() => PickAMemory());
		}
	}
	//void KartSound()
	//{
	//	kartSound.GetComponent<AudioSource>().Play();
	//}

	public void PickAMemory() // kart se�me kontrolleri
	{
		kartSound.GetComponent<AudioSource>().Play();

		// tahminlerin indexi al�n�yoruz, g�rsellere isim veriyoruz ve bu g�r�nt�leri butonlara at�yoruz

		if (!firstGuess) // ilk tahmin yanl�� ise
		{
			firstGuess = true;

			firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name); // ilk t�klan�lan butonun indexifirstGuessIndexe atan�yor 

			firstGuessMemory = gameMemorys[firstGuessIndex].name;

			btns[firstGuessIndex].image.sprite = gameMemorys[firstGuessIndex]; // bu butonun resmi gameMemorys den al�n�p ilgili butona atan�yor

		}
		else if (!secondGuess)
		{
			secondGuess = true;

			secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

			secondGuessMemory = gameMemorys[secondGuessIndex].name;

			btns[secondGuessIndex].image.sprite = gameMemorys[secondGuessIndex];



			if (firstGuessMemory==secondGuessMemory)
			{
				//audioManager.PlaySFX(audioManager.correct);
				
				correctSound.GetComponent<AudioSource>().Play();
				Debug.Log("e�le�ti");
			}
			else
			{
				Debug.Log("E�LE�MED�");
			}

			countGuesses++;
			StartCoroutine(CheckIfThePuzzlesMatch());
		}
	}
	

	IEnumerator CheckIfThePuzzlesMatch()
	{
		yield return new WaitForSeconds(.5f);
		if (firstGuessMemory == secondGuessMemory) // DO�RU TAHM�N EDERSEK
		{
			yield return new WaitForSeconds(.1f);

			btns[firstGuessIndex].interactable = false; // do�ru tahmin edilenler t�klanamaz oldu
			btns[secondGuessIndex].interactable = false;

			btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
			btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

			CheckIfTheGameIsFinished();
			//point();
		}
		else
		{
			yield return new WaitForSeconds(.1f);

			btns[firstGuessIndex].image.sprite = bgImage;
			btns[secondGuessIndex].image.sprite = bgImage;
		}
		yield return new WaitForSeconds(.1f);

		firstGuess = secondGuess = false;
	}

	void CheckIfTheGameIsFinished()
	{
		countCorrectGuesses++;
		if (countCorrectGuesses == gameGuesses)
		{
			kartSound.GetComponent<AudioSource>().enabled=false;
			Debug.Log("final");
			if (SceneManager.GetActiveScene().name == "Hafiza_8")
			{
				if (gameOver)
				{
					gameOver.gameObject.SetActive(true);

				}

			}
			else
			{
				Invoke("NxtLevel", .5f);
				Debug.Log("" + countGuesses + " par�a e�le�tirildi");

			}

		}
	}
	
	void NxtLevel()
	{
		
			MemoryNextLevel.instance.UnlockNewLevel();
			MemoryNextLevel.instance.NextLevelLoad();
		
	}

	void Shuffle(List<Sprite> list) // par�alar�n random da��l�m�
	{

		for (int i = 0; i < list.Count; i++)
		{
			Sprite temp = list[i];
			int randomIndex = UnityEngine.Random.Range(i, list.Count);
			list[i] = list[randomIndex];
			list[randomIndex] = temp;
		}
	}
	




}
