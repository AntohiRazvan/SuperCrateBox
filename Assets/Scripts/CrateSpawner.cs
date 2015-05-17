using UnityEngine;
using System.Collections;

public class CrateSpawner : MonoBehaviour {

	public float minX;
	public float maxX;
	public float minY;
	public float maxY;
	public GameObject crate;
	public GameObject playerSpawn;

	private GameObject currentCrate;
	private GameObject newCrate;
	private Vector3 spawnPosition;
	private Vector3 nextPosition;
	bool firstSpawned = false;

	void Awake () 
	{
		GameEventManager.GameStart += GameStart;
	}

	void Update () 
	{
		if((currentCrate == null))
		{
			spawnCrate(nextPosition);
			currentCrate = newCrate;
			setNextPosition();
		}
	}

	void spawnCrate(Vector3 position)
	{
			newCrate = (GameObject)Instantiate(crate, nextPosition, Quaternion.identity);
	}

	void setNextPosition()
	{
		do
		{
			spawnPosition = new Vector3();
			spawnPosition.x = Random.Range(minX, maxX);
			spawnPosition.y = Random.Range(minY, maxY);
			spawnPosition.z = 0f;
		} while (!canPlace(spawnPosition));
		nextPosition = spawnPosition;
	}

	bool canPlace(Vector3 position)
	{
		if((position.x > minX) && (position.x < maxX))
			if((position.y > minY) && (position.y < maxY))
				if(Vector3.Distance(position, currentCrate.transform.position) > 3f)
					if (!Physics.Raycast(position, Vector3.forward, 10f))
						if(!Physics.Raycast(position, Vector3.down, 0.3f))
							if (Physics.Raycast(position, Vector3.down, 0.5f))
							return true;
		return false;
	}

	bool firstSpawn(Vector3 position)
	{
		if((position.x > minX) && (position.x < maxX))
			if((position.y > minY) && (position.y < maxY))
				if (!Physics.Raycast(position, Vector3.forward, 10f))
					if(!Physics.Raycast(position, Vector3.down, 0.3f))
						if (Physics.Raycast(position, Vector3.down, 0.5f))
						{
							newCrate = (GameObject)Instantiate(crate, position, Quaternion.identity);
							playerSpawn.SetActive(false);
							return true;
						}
		return false;
	}

	void GameStart()
	{
		playerSpawn.SetActive(true);
		Destroy(currentCrate);
		while (currentCrate == null)
		{
			spawnPosition = new Vector3();
			spawnPosition.x = Random.Range(minX, maxX);
			spawnPosition.y = Random.Range(minY, maxY);
			spawnPosition.z = 0f;
			if (firstSpawn(spawnPosition))
			{
				currentCrate = newCrate;
			}
		}
	}

	void OnDestroy()
	{
		GameEventManager.GameStart -= GameStart;
	}
}
