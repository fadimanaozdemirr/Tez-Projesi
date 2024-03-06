using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordData : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI charText;

    [HideInInspector]
    public char charValue; // charValue

    private Button buttonObj;
	//internal char charValue;

	private void Awake()
    {
        buttonObj = GetComponent<Button>();
        if (buttonObj)
        {
            buttonObj.onClick.AddListener(() => CharSelected());
        }
    }

    public void SetChar(char value)
    {
        charText.text = value + "";
        charValue = value;
    }

    private void CharSelected()
    {
        QuizController.instance.SelectedOption(this);
    }

}
