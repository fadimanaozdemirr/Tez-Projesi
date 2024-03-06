using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tasi4 : MonoBehaviour
{

    Camera camera;
    Vector2 baslangic_poz; // puzzle par�alar�n�n ilk konumunu tutucam

    GameObject[] kutu_dizisi;

	PuzzleController4 control4;
    private void OnMouseDrag() // yapboz par�alar�n�n mouse ile ta��ma i�lemleri i�in fonksiyon
    {
        Vector3 pozisyon = camera.ScreenToWorldPoint(Input.mousePosition);
        pozisyon.z = 0; // oyunda kutular�n z ekseni de�eri hep 0, de�i�memesi laz�m.
        transform.position = pozisyon; // yapboz par�alar� ta��nabilir hale geldi

    }


    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        baslangic_poz = transform.position;

        kutu_dizisi = GameObject.FindGameObjectsWithTag("kutu");
		control4 = GameObject.Find("PuzzleController4").GetComponent<PuzzleController4>();
    }

    
    void Update()
    {
		// kutu isimleri ayn� ve mesafe 1 den az ise yerine yerle�sin, de�ilse ilk konumuna d�ns�n
		if (Input.GetMouseButtonUp(0))
		{
			foreach (GameObject kutu in kutu_dizisi)
			{
				if (kutu.name == gameObject.name) // siyah kutular�n ve puzzle par�alar�n�n ismini e�le�tiriyor
				{
					float mesafe = Vector3.Distance(kutu.transform.position, transform.position);
					if (mesafe <= 1)
					{
						transform.position = kutu.transform.position; // mesafe 1den az ise puzzle par�as�n�n konumu siyah yerdeki konum oluyor
						control4.sayi_arttir();
						this.enabled = false;

					}
					else
					{
						transform.position = baslangic_poz;
					}
				}
			}
		}
	}
}
