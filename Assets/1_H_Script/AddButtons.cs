using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddButtons : MonoBehaviour
{
    [SerializeField]
    private Transform memoryField;

	[SerializeField]
	private GameObject btn;

	private void Awake()
	{
        int maxButtonCount = 4; // Varsayýlan deðer

        string sahneAdi = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        switch (sahneAdi)
        {
            case "Hafiza_1":
                maxButtonCount = 4;
                break;

            case "Hafiza_2" :
            case "Hafiza_3" :
                maxButtonCount = 6;
                break;

            case "Hafiza_4":
                maxButtonCount = 8;
                break;

            case "Hafiza_5":
            case "Hafiza_6":
                maxButtonCount = 12;
                break;

            case "Hafiza_7":
                maxButtonCount = 16;
                break;

            case "Hafiza_8":
                maxButtonCount = 20;
                break;

            default:
                Debug.LogError("Bilinmeyen sahne adý: " + sahneAdi);
                break;
        }

        for (int i = 0; i < maxButtonCount; i++)
        {
            GameObject button = Instantiate(btn);
            button.name = "" + i;
            button.transform.SetParent(memoryField, false);
        }



  
	}
}
