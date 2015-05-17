using UnityEngine;
using System.Collections;
using System.IO;

public class Statistics : MonoBehaviour
{

	public int level1HighscoreNormal = 0;
	public int level2HighscoreNormal = 0;
	public int level3HighscoreNormal = 0;

	public int level1HighscoreSMFT = 0;
	public int level2HighscoreSMFT = 0;
	public int level3HighscoreSMFT = 0;

	public int level1HighscoreAmbush = 0;
	public int level2HighscoreAmbush = 0;
	public int level3HighscoreAmbush = 0;

	public int cratesCollected = 0;
	public int smallKills = 0;
	public int bigKills = 0;
	public int ghostKills = 0;

	public int deaths;

	void Awake()
	{
		if (GameObject.FindGameObjectsWithTag("stats").Length > 1)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
		GameEventManager.GameOver += GameOver;
	}

	void Start()
	{
		try
		{
			using (StreamReader sr = new StreamReader(Application.persistentDataPath + "/SuperCrateBox124531.dat"))
			{
				level1HighscoreNormal = int.Parse(sr.ReadLine());
				level2HighscoreNormal = int.Parse(sr.ReadLine());
				level3HighscoreNormal = int.Parse(sr.ReadLine());

				level1HighscoreSMFT = int.Parse(sr.ReadLine());
				level2HighscoreSMFT = int.Parse(sr.ReadLine());
				level3HighscoreSMFT = int.Parse(sr.ReadLine());

				level1HighscoreAmbush = int.Parse(sr.ReadLine());
				level2HighscoreAmbush = int.Parse(sr.ReadLine());
				level3HighscoreAmbush = int.Parse(sr.ReadLine());

				cratesCollected = int.Parse(sr.ReadLine());
				smallKills = int.Parse(sr.ReadLine());
				bigKills = int.Parse(sr.ReadLine());
				ghostKills = int.Parse(sr.ReadLine());

				deaths = int.Parse(sr.ReadLine());
			}
		}
		catch (System.Exception e)
		{

		}
	}

	void GameOver()
	{
		try
		{
			int mode = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().mode;
			int level = Application.loadedLevel;
			int score = GUIManager.getScore();
			switch (level)
			{
				case 3:
					if (mode == 0)
					{
						if(level1HighscoreNormal < score)
								level1HighscoreNormal = score;
					}
					else if (mode == 1)
					{		
						if(level1HighscoreSMFT < score)
							level1HighscoreSMFT = score;
					}
					break;
				case 4:
					if (mode == 0)
					{
						if(level2HighscoreNormal < score)
							level2HighscoreNormal = score;
					}
					else if (mode == 1)
					{
 						if(level2HighscoreSMFT < score)
							level2HighscoreSMFT = score;
					}
					break;
				case 5:
					if (mode == 0)
					{
						if(level3HighscoreNormal < score)
							level3HighscoreNormal = score;
					}
					else if (mode == 1)
					{
						if(level3HighscoreSMFT < score)
						level3HighscoreSMFT = score;
					}
					break;
			}
		}
		catch (System.Exception e)
		{ }

		try
		{
			File.Delete(Application.persistentDataPath + "/highscore.dat");
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath + "/SuperCrateBox124531.dat", false))
			{
				file.WriteLine(level1HighscoreNormal.ToString());
				file.WriteLine(level2HighscoreNormal.ToString());
				file.WriteLine(level3HighscoreNormal.ToString());


				file.WriteLine(level1HighscoreSMFT.ToString());
				file.WriteLine(level2HighscoreSMFT.ToString());
				file.WriteLine(level3HighscoreSMFT.ToString());

				file.WriteLine(level1HighscoreAmbush.ToString());
				file.WriteLine(level2HighscoreAmbush.ToString());
				file.WriteLine(level3HighscoreAmbush.ToString());

				file.WriteLine(cratesCollected.ToString());
				file.WriteLine(smallKills.ToString());
				file.WriteLine(bigKills.ToString());
				file.WriteLine(ghostKills.ToString());

				file.WriteLine(deaths.ToString());
			}
		}
		catch (System.Exception e)
		{

		}
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel("menu");
		}
	}
}
