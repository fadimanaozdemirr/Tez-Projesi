using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private GameObject player; // unity arayüzde görünmesi saðlar ama kodda private
	[SerializeField] float moveSpeed;
	[SerializeField] private GameObject gameOver;
	public Labirent labirent;

	public float donusSpeed;
	public Transform[] checkpointNoktalari;

	[SerializeField] GameObject correctSound;

	private void Start()
	{
		correctSound = GameObject.FindGameObjectWithTag("CorrectSound").gameObject;
	}
	void Update()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, Input.GetAxis("Vertical") * moveSpeed);
		
		Debug.Log("Suanki:"+PlayerPrefs.GetInt("Suankilevel", 0)+ "level:" + PlayerPrefs.GetInt("level", 0));
	}
	private void OnTriggerEnter2D(Collider2D collision) // temas iþlemleri
	{
		if (collision.gameObject.tag == "finishFlag")
		{
			correctSound.GetComponent<AudioSource>().Play();
			Debug.Log("temas etti");

			PlayerPrefs.SetInt("Suankilevel", PlayerPrefs.GetInt("Suankilevel", 0) + 1);
			if (PlayerPrefs.GetInt("level", 0) < PlayerPrefs.GetInt("Suankilevel", 0) )
			{
				PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level", 0) + 1);
			}

			if (labirent.level==6)
			{
				gameOver.gameObject.SetActive(true);
			}
			else
			{
				labirent.LevelGec();
				BaslangicNoktasiniBelirle();
			}
		
				//labirent.LevelGec();
				//BaslangicNoktasiniBelirle();
			//}
		}
	}
	void BaslangicNoktasiniBelirle()
    {
		
        // Baþlangýç noktasýný belirle
        player.transform.position = checkpointNoktalari[labirent.level].position;

        // Burada baþka baþlangýç noktasý için yapýlmasý gereken iþlemleri ekleyebilirsiniz
    }
	void OyunuBitir()
	{
		Debug.Log("OYUN BÝTTÝ");
	}

	

}
