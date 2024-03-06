using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading.Tasks;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] GameObject pauseMenu;
	[SerializeField] RectTransform pausePanelRect, pauseBtnRect; // panelin konumu kontrol etmek için puan tablosunu da ekle //scoreRect
	[SerializeField] float topPosY, middlePosY;
	[SerializeField] float tweenDuration;
	[SerializeField] CanvasGroup canvasGroup; // dark panelin aþamalý kararmasý için
	[SerializeField] GameObject pauseSource;
	[SerializeField] GameObject buttonSource;
	private void Start()
	{
		
	}
	public void Pause()
	{
		pauseMenu.SetActive(true);
		//Time.timeScale = 0;
		PausePanelIntro(); // giris animasyonu
		pauseSource = GameObject.FindGameObjectWithTag("PauseSound").gameObject;
		pauseSource.GetComponent<AudioSource>().Play();
	}
	
	public void Home()
	{
		buttonSource = GameObject.FindGameObjectWithTag("ButtonSound").gameObject;
		buttonSource.GetComponent<AudioSource>().Play();
		Invoke("mainMenu", .5f);

	}
	void mainMenu()
	{
		SceneManager.LoadScene("mainMenu");
		Time.timeScale = 1;
	}
	public async void Resume()
	{
		buttonSource = GameObject.FindGameObjectWithTag("ButtonSound").gameObject;
		buttonSource.GetComponent<AudioSource>().Play();

		pauseSource = GameObject.FindGameObjectWithTag("PauseSound").gameObject;
		pauseSource.GetComponent<AudioSource>().Play();
		await PausePanelOutro();
		pauseMenu.SetActive(false);
		Time.timeScale = 1;
		
	}
	public void Restart()
	{
		buttonSource = GameObject.FindGameObjectWithTag("ButtonSound").gameObject;
		buttonSource.GetComponent<AudioSource>().Play();
		Invoke("Restart2", .5f);
	}
	async void Restart2()
	{
		await PausePanelOutro();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Time.timeScale = 1;
	}

	void PausePanelIntro()
	{
		canvasGroup.DOFade(1, tweenDuration).SetUpdate(true);
		pausePanelRect.DOAnchorPosY(middlePosY, tweenDuration).SetUpdate(true); // set update ile animasyon zamandan baðýmsýz olur ve arkaplanda oyun durur.
		pauseBtnRect.DOAnchorPosX(-66, tweenDuration).SetUpdate(true);
	}
	async Task PausePanelOutro()
	{
		canvasGroup.DOFade(0, tweenDuration).SetUpdate(true);
		await pausePanelRect.DOAnchorPosY(topPosY, tweenDuration).SetUpdate(true).AsyncWaitForCompletion(); // konum ve süre
		pauseBtnRect.DOAnchorPosX(20, tweenDuration).SetUpdate(true);
	}

}
