using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    Button btn;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ButtonS);
    }

    void Update()
    {

    }
    public void ButtonS()
    {
		if (MainMenuController.instance)
        MainMenuController.instance.ButtonSound.Play();

	    else if (QuizController.instance)
		{
            QuizController.instance.clickSound.Play();

        }

    }
}
