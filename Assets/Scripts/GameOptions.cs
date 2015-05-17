using UnityEngine;
using System.Collections;

public class GameOptions : MonoBehaviour {

	public int gameMode;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
	

}
