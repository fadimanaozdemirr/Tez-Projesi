using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController instance;
	public AudioSource ButtonSound;
	private void Awake()
	{
        instance = this;
	}
	
}
