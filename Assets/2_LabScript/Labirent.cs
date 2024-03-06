using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labirent : MonoBehaviour
{

	public List<GameObject> Labirentler;
	public int level = 0;
	public int SuankiLevel = 0;

	void Start()
	{
		
		level = PlayerPrefs.GetInt("level",0);
		SuankiLevel = PlayerPrefs.GetInt("Suankilevel", 0); ;

		LevelGec();
	}
	public void LevelGec() // levellerin görünürlüðü
	{
		level = PlayerPrefs.GetInt("Suankilevel", 0);
		foreach (var item in Labirentler)
			item.gameObject.SetActive(false); 
		Labirentler[level].gameObject.SetActive(true);


	}

	
    
    void Update()
    {
		
	}
}
