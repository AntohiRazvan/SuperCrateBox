using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
		
	public List<GameObject> characters = new List<GameObject>();

	GameObject player;

	void Awake () 
	{
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
	}
	
	
	void GameStart()
	{
		player = (GameObject)Instantiate((GameObject)characters[Random.Range(0, characters.Count)], gameObject.transform.position, Quaternion.Euler(0f, 180f, 0f));
	}

	void GameOver()
	{
		Destroy(player);
	}

	void OnDestroy()
	{
		GameEventManager.GameStart -= GameStart;
		GameEventManager.GameOver -= GameOver;
	}
}
