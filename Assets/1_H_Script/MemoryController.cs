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
		//puzzels = Resources.LoadAll<Sprite>("hafiza/animal"); // eþleþecek resimleri aldýk. sürüklemek yerine kodla yaptýk
		
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
		gameGuesses = gameMemorys.Count / 2; // gameGuess olmasý gereken toplam doðru tahmin sayýsý
	}

	void GetButtons()
	{
		GameObject[] objects = GameObject.FindGameObjectsWithTag("MemoryButton");

		for (int i = 0; i < objects.Length; i++)
		{
			btns.Add(objects[i].GetComponent<Button>());
			btns[i].image.sprite = bgImage; // buton bg lerini buradan deðiþtir.
		}
	}

	void AddGamePuzzles()
	{
		int looper = btns.Count; // eþleþecek resimlerin hepsini hepsini aldýk ve buton sayýsý kadarýný seçtik
		int index = 0;

		for (int i = 0; i < looper; i++)
		{
			if (index == looper / 2) // her resimden 2 tane ekledik ki eþleþtirebilelim
			{
				index = 0;
			}
			gameMemorys.Add(puzzels[index]);
			index++;
		}
	}

	void AddListeners()
	{
		foreach (Button btn in btns) // butonlarýn týklanma özelliði verildi
		{
			btn.onClick.AddListener(() => PickAMemory());
		}
	}
	//void KartSound()
	//{
	//	kartSound.GetComponent<AudioSource>().Play();
	//}

	public void PickAMemory() // kart seçme kontrolleri
	{
		kartSound.GetComponent<AudioSource>().Play();

		// tahminlerin indexi alýnýyoruz, görsellere isim veriyoruz ve bu görüntüleri butonlara atýyoruz

		if (!firstGuess) // ilk tahmin yanlýþ ise
		{
			firstGuess = true;

			firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name); // ilk týklanýlan butonun indexifirstGuessIndexe atanýyor 

			firstGuessMemory = gameMemorys[firstGuessIndex].name;

			btns[firstGuessIndex].image.sprite = gameMemorys[firstGuessIndex]; // bu butonun resmi gameMemorys den alýnýp ilgili butona atanýyor

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
				Debug.Log("eþleþti");
			}
			else
			{
				Debug.Log("EÞLEÞMEDÝ");
			}

			countGuesses++;
			StartCoroutine(CheckIfThePuzzlesMatch());
		}
	}
	

	IEnumerator CheckIfThePuzzlesMatch()
	{
		yield return new WaitForSeconds(.5f);
		if (firstGuessMemory == secondGuessMemory) // DOÐRU TAHMÝN EDERSEK
		{
			yield return new WaitForSeconds(.1f);

			btns[firstGuessIndex].interactable = false; // doðru tahmin edilenler týklanamaz oldu
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
				Debug.Log("" + countGuesses + " parça eþleþtirildi");

			}

		}
	}
	
	void NxtLevel()
	{
		
			MemoryNextLevel.instance.UnlockNewLevel();
			MemoryNextLevel.instance.NextLevelLoad();
		
	}

	void Shuffle(List<Sprite> list) // parçalarýn random daðýlýmý
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
