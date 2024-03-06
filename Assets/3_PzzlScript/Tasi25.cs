using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasi25 : MonoBehaviour
{
    Camera camera;
    Vector2 baslangic_poz; // puzzle parçalarýnýn ilk konumunu tutucam

    GameObject[] kutu_dizisi;

    PuzzleController25 control25;
    private void OnMouseDrag() // yapboz parçalarýnýn mouse ile taþýma iþlemleri için fonksiyon
    {
        Vector3 pozisyon = camera.ScreenToWorldPoint(Input.mousePosition);
        pozisyon.z = 0; // oyunda kutularýn z ekseni deðeri hep 0, deðiþmemesi lazým.
        transform.position = pozisyon; // yapboz parçalarý taþýnabilir hale geldi

    }
    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        baslangic_poz = transform.position;

        kutu_dizisi = GameObject.FindGameObjectsWithTag("kutu");
        control25 = GameObject.Find("PuzzleController25").GetComponent<PuzzleController25>();
    }

    
    void Update()
    {
        // kutu isimleri ayný ve mesafe 1 den az ise yerine yerleþsin, deðilse ilk konumuna dönsün
        if (Input.GetMouseButtonUp(0))
        {
            foreach (GameObject kutu in kutu_dizisi)
            {
                if (kutu.name == gameObject.name) // siyah kutularýn ve puzzle parçalarýnýn ismini eþleþtiriyor
                {
                    float mesafe = Vector3.Distance(kutu.transform.position, transform.position);
                    if (mesafe <= 1)
                    {
                        transform.position = kutu.transform.position; // mesafe 1den az ise puzzle parçasýnýn konumu siyah yerdeki konum oluyor
                        control25.sayi_arttir();
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
