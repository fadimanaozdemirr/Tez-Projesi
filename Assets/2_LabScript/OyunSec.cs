using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OyunSec : MonoBehaviour
{

    public GameObject labirentPopup; 
    private bool popupAcikMi = false; // Popup ekran�n�n a��k olup olmad���n� kontrol etmek i�in

    void Start()
    {
        // Labirent butonuna t�kland���nda LabirentButonunaTiklandi fonksiyonunu �a��r
        Button labirentButon = transform.GetChild(3).GetComponent<Button>();
        if (labirentButon != null)
        {
            labirentButon.onClick.AddListener(LabirentButonunaTiklandi);
        }

        // Ba�lang��ta popup ekran�n� kapat
        if (labirentPopup != null)
        {
            labirentPopup.SetActive(false);
        }
    }

    void LabirentButonunaTiklandi()
    {
        
        Debug.Log("Labirent butonuna t�kland�");

        // Popup ekran�n� a� veya kapat
        if (labirentPopup != null)
        {
            popupAcikMi = !popupAcikMi;
            labirentPopup.SetActive(popupAcikMi);
        }
    }


    void Update()
    {
        
    }
}
