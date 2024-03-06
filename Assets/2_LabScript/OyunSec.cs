using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OyunSec : MonoBehaviour
{

    public GameObject labirentPopup; 
    private bool popupAcikMi = false; // Popup ekranýnýn açýk olup olmadýðýný kontrol etmek için

    void Start()
    {
        // Labirent butonuna týklandýðýnda LabirentButonunaTiklandi fonksiyonunu çaðýr
        Button labirentButon = transform.GetChild(3).GetComponent<Button>();
        if (labirentButon != null)
        {
            labirentButon.onClick.AddListener(LabirentButonunaTiklandi);
        }

        // Baþlangýçta popup ekranýný kapat
        if (labirentPopup != null)
        {
            labirentPopup.SetActive(false);
        }
    }

    void LabirentButonunaTiklandi()
    {
        
        Debug.Log("Labirent butonuna týklandý");

        // Popup ekranýný aç veya kapat
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
