using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

	public GameObject bigEnemy;
	public GameObject smallEnemy;
	public GameObject ghost;
	public int mode;

	public float normalMinSpawnDelay;
	public float normalMaxSpawnDelay;


	public float SMFTMinSpawnDelay;
	public float SMFTMaxSpawnDelay;

	float spawnDelay = 1;
	float lastSpawn = 0f;

	public GameObject pause;
	bool paused = false;

	void Awake () 
	{
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		mode = GameObject.Find("GameOptions").GetComponent<GameOptions>().gameMode;
		enabled = false;
	}
	
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.P))
		{
			if(paused)
			{
				pause.SetActive(false);
				Time.timeScale = 1;
				paused = false;
			}
			else
			{
				pause.SetActive(true);
				Time.timeScale = 0;
				paused = true;
			}
		}

		switch (mode)
		{
			case 0:
				normalMode();
				break;
			case 1:
				SMFTMode();
				break;
			case 2:
				ambushMode();
				break;
		}
	}

	private void normalMode()
	{
		if((lastSpawn + spawnDelay) < Time.time)
		{
			spawnDelay = Random.Range(normalMinSpawnDelay, normalMaxSpawnDelay);
			lastSpawn = Time.time;
			switch(Random.Range(0, 4))
			{
				case 0:
					spawnSmall();
					break;
				case 1:
					spawnBig();
					break;
				case 2:
					StartCoroutine(spawnSmallGroup(3));
					break;
				case 3:
					spawnGhost();
					break;
			}
		}
	}

	private void SMFTMode()
	{
		if ((lastSpawn + spawnDelay) < Time.time)
		{
			spawnDelay = Random.Range(SMFTMinSpawnDelay, SMFTMaxSpawnDelay);
			lastSpawn = Time.time;
			switch (Random.Range(0, 4))
			{
				case 0:
					spawnSmall();
					break;
				case 1:
					spawnBig();
					break;
				case 2:
					StartCoroutine(spawnSmallGroup(5));
					break;
				case 3:
					spawnGhost();
					break;
			}
		}
	}

	private void ambushMode()
	{

	}
	void spawnBig()
	{
		GameObject enemy = (GameObject)Instantiate(bigEnemy, transform.position, Quaternion.identity);
		int direction = Random.Range(1, 3);
		if(direction == 2)
		{
			enemy.GetComponent<LandEnemy>().facingRight = false;
			enemy.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
		}
	}

	void spawnSmall()
	{
		GameObject enemy = (GameObject)Instantiate(smallEnemy, transform.position, Quaternion.identity);
		int direction = Random.Range(1, 3);
		if (direction == 2)
		{
			enemy.GetComponent<LandEnemy>().facingRight = false;
			enemy.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
		}
	}

	IEnumerator spawnSmallGroup(int count)
	{
		int direction = Random.Range(1, 3);
		if (direction == 2)
		{
			for (int i = 0; i < count; i++)
			{
				GameObject enemy = (GameObject)Instantiate(smallEnemy, transform.position, Quaternion.identity);
				enemy.GetComponent<LandEnemy>().facingRight = false;
				enemy.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
				yield return new WaitForSeconds(0.25f);
			}
			spawnDelay += 0.25f * count;
		}
		else
		{
			for (int i = 0; i < count; i++)
			{
				Instantiate(smallEnemy, transform.position, Quaternion.identity);
				yield return new WaitForSeconds(1f);
			}
		}
	}

	void spawnGhost()
	{
		GameObject enemy = (GameObject)Instantiate(ghost, transform.position, Quaternion.identity);
		int direction = Random.Range(1, 3);
	}

	bool randBool()
	{
		int ret = Random.Range(1, 3);
		if (ret == 1)
			return true;
		else
			return false;
	}

	void GameStart()
	{
		enabled = true;
	}

	void GameOver()
	{
		enabled = false;
	}
	void OnDestroy()
	{
		GameEventManager.GameStart -= GameStart;
		GameEventManager.GameOver -= GameOver;
	}
}
