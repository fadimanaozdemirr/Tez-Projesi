using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSecPopup : MonoBehaviour
{

	public Button[] buttons;
	void Start()
    {
        GameObject BtnKapat = GameObject.FindGameObjectWithTag("BtnKapat");
		if (BtnKapat != null)
		{
            BtnKapat.GetComponent<Button>().onClick.AddListener(CarpiButonunaBasildi);
        }
    }

    public void StartGame(int index) //level seçip oyunun baþlamasý. Labirent Class bak
	{

		PlayerPrefs.SetInt("Suankilevel", index);
		if (PlayerPrefs.GetInt("level", 0)<PlayerPrefs.GetInt("Suankilevel", 0))
		{
			PlayerPrefs.SetInt("level", index); // veritabanýnda level tuttuk.

		}
		
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	private void Awake()      // level buton kilitle
	{
		int unlockedLevel = PlayerPrefs.GetInt("level", 0);
		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].interactable = false;
		}
		for (int i = 0; i <= unlockedLevel; i++)
		{
			buttons[i].interactable = true;
			buttons[i].transform.GetChild(0).GetComponent<Image>().color = Color.green;

		}
	}

	public void CarpiButonunaBasildi()
	{
        Debug.Log("Popup ekraný kapatýldý");
        gameObject.SetActive(false);
    }
    void Update()
    {
		Debug.Log(PlayerPrefs.GetInt("level", 0));
	}
}
