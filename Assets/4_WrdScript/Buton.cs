using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Buton : MonoBehaviour
{
    WordsController control;
    Image renk;
    RectTransform buyukluk;

    string harf;

    bool harf_verildi = false;

    public bool yok_ol = false;

    float kuculme_miktari = 0.08f;


    void Start()
    {
        control = GameObject.Find("WordsController").GetComponent<WordsController>();

        renk = GetComponent<Image>();

        buyukluk = GetComponent<RectTransform>();

        harf = gameObject.name;
    }

    
    private void Update()
    {
		if (control.tiklandi==false)
		{
            harf_verildi = false;
			renk.color = Color.white;
		}

		if (yok_ol==true)
		{
            buyukluk.localScale -= new Vector3(kuculme_miktari, kuculme_miktari, kuculme_miktari);

			if (buyukluk.localScale.x <= 0)
			{
                Destroy(gameObject);
			}
		}
    }

    public void yesilOl()
	{
        if (control.tiklandi == true)
        {
            renk.color = Color.green;

			if (harf_verildi == false)
			{
                control.isaretli_buton_olustur(gameObject);
                harf_verildi = true;
			}
        }
    }
}
