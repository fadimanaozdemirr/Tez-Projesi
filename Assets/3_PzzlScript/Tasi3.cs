using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasi3 : MonoBehaviour
{
    Camera camera;
    Vector2 baslangic_poz; // puzzle par�alar�n�n ilk konumunu tutucam
    GameObject[] kutu_dizisi;

    PuzzleController3 controlEt3;
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
        controlEt3 = GameObject.Find("PuzzleController3").GetComponent<PuzzleController3>();
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
                        print("Emre");
                        transform.position = kutu.transform.position; // mesafe 1den az ise puzzle par�as�n�n konumu siyah yerdeki konum oluyor
						controlEt3.sayi_arttir();
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
