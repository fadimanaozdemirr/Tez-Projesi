using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    public static QuizController instance;  //LilitaOne-Regular Outline 120 SDF

    [SerializeField] private GameObject gameOver;
    [SerializeField] private QuizDataScriptable questionData;
    [SerializeField] private Image questionImage;
    [SerializeField] private WordData[] answerWordList;     //do�ru cevaplar�n bulundu�u list
    [SerializeField] private WordData[] optionsWordList;    // harf se�eneklerinin oldu�u list
    [SerializeField] GameObject correctSound;
    public AudioSource clickSound;

    private char[] charArray = new char[12]; // 12 prefab yani 12 kelimemiz var oldu�u i�in
    private int currentAnswerIndex = 0; // verilen cevap
    private bool correctAnswer = true;
    private List<int> selectedWordIndex;
    private int currentQuestionIndex = 0;
    private GameStatus gameStatus = GameStatus.Playing;
	private string answerWord;
    private int score = 0;
    public List<QuizDataScriptable> quizDatas;

	private void Awake() // ilk �rnekten sonra di�erlerini engeller
	{
		if (instance == null)
		{
            instance = this;
        }
		else
		{
            Destroy(this.gameObject);
        }

        selectedWordIndex = new List<int>();
	}

	private void Start()
	{
        correctSound = GameObject.FindGameObjectWithTag("CorrectSound").gameObject;
        questionData = quizDatas[PlayerPrefs.GetInt("quizDatas", 0)];
        SetQuestion();
    }
	private void Update()
	{
        print(currentAnswerIndex);
    }


	private void SetQuestion() // soruyu rastgele harfleri ayarlad���m�z kod blo�u
	{
        currentAnswerIndex = 0;
        selectedWordIndex.Clear();
		//ResetQuestion();
		questionImage.sprite = questionData.questions[currentQuestionIndex].questionImage;
        answerWord = questionData.questions[currentQuestionIndex].answer;//1

        ResetQuestion();

        for (int i = 0; i < answerWord.Length; i++)//2
        {
            charArray[i] = char.ToUpper(answerWord[i]); // b�y�k harf olarak ald�k  //3
        }

		for (int i = answerWord.Length; i < optionsWordList.Length; i++) //4
		{
            charArray[i] = (char)UnityEngine.Random.Range(65, 91); // harflerin say� kodu 65ten ba�lay�p 90da bitiyor
        }
        charArray = ShuffleList.ShuffleListItems<char>(charArray.ToList()).ToArray();

        for (int i = 0; i < optionsWordList.Length; i++)
        {
            optionsWordList[i].SetChar(charArray[i]);
        }
        currentQuestionIndex++;
        gameStatus = GameStatus.Playing;

    }


    public void SelectedOption(WordData wordData) // harf se�me durumlar� kontrol edilir
	{
		if (gameStatus == GameStatus.Next || currentAnswerIndex >= answerWord.Length)return;//5

        selectedWordIndex.Add(wordData.transform.GetSiblingIndex());
        answerWordList[currentAnswerIndex].SetChar(wordData.charValue);
        wordData.gameObject.SetActive(false); // kullan�lan harf g�r�nmez
		currentAnswerIndex++;

		if (currentAnswerIndex >= answerWord.Length) // cevab�n do�rulu�u kontrol ediliyor//6
		{
			correctAnswer = true;

			for (int i = 0; i < answerWord.Length; i++)//7
			{
				if (char.ToUpper(answerWord[i]) != char.ToUpper(answerWordList[i].charValue))//8
				{
                    correctAnswer = false;
                    break;
				}
			}
			if (correctAnswer==true)
			{
                gameStatus = GameStatus.Next;
                score += 50;
                correctSound.GetComponent<AudioSource>().Play();

                Debug.Log("do�ru cevap" + score);

                if (currentQuestionIndex < questionData.questions.Count)
				{
                    Invoke("SetQuestion", 0.5f);
                }
				else
				{
                    gameOver.SetActive(true);
				}
                
			}
			else if (correctAnswer==false)
			{
                Debug.Log("yanl�� cevap");
                Invoke("ResetQuestion",.3f);
               

            }
		}

	}

	public void ResetQuestion() // kelime kadar cevap kutusu g�sterir
	{
        currentAnswerIndex = 0;

        for (int i = 0; i < answerWordList.Length; i++)
		{
            answerWordList[i].gameObject.SetActive(true);
            answerWordList[i].SetChar('_');
		}

        for (int i = answerWord.Length; i < answerWordList.Length; i++)//9
        {
            answerWordList[i].gameObject.SetActive(false);
            
        }
		for (int i = 0; i < optionsWordList.Length; i++) // kelime de�itik�e harf se�enekleri yeniden diziliyor
		{
			optionsWordList[i].gameObject.SetActive(true);
		}

		//currentAnswerIndex = 0;
	}

    public void ResetLastWord() // kullan�lan harf geri alma button
	{

		if (selectedWordIndex.Count > 0)
		{
            int index = selectedWordIndex[selectedWordIndex.Count - 1];
            optionsWordList[index].gameObject.SetActive(true);
            selectedWordIndex.RemoveAt(selectedWordIndex.Count - 1);

			if (currentAnswerIndex>=1)
			{
                currentAnswerIndex--;

            }

            if (currentAnswerIndex>=0)
            answerWordList[currentAnswerIndex].SetChar('_');
        }
     
	}
}

[System.Serializable]
public class QuestionData
{
    public Sprite questionImage;
    public string answer;
}
public enum GameStatus
{
    Playing,
    Next
}