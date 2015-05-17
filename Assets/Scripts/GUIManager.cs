using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public GUIText score;
	public GameObject gameOver;


	private static GUIManager instance;

	public void Awake()
	{
		instance = this;
		score.text = "0";
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
	}

	void Start () 
	{
		score.text = "0";
		GameEventManager.triggerGameStart();
	}
	
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Return))
		{
			GameEventManager.triggerGameStart();
		}
	}

	void GameStart()
	{
		gameOver.SetActive(false);
		score.text = "0";
		enabled = false;
	}

	void GameOver()
	{
		enabled = true;
		gameOver.SetActive(true);
		gameOver.transform.FindChild("Crates").GetComponent<TextMesh>().text = "Crates: " + score.text;
		if (Application.loadedLevel == 3)
		{
			if (GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().mode == 0)
				gameOver.transform.FindChild("Highscore").GetComponent<TextMesh>().text = "Highscore: " + GameObject.Find("Statistics").GetComponent<Statistics>().level1HighscoreNormal.ToString();
			else if(GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().mode == 1)
				gameOver.transform.FindChild("Highscore").GetComponent<TextMesh>().text = "Highscore: " + GameObject.Find("Statistics").GetComponent<Statistics>().level1HighscoreSMFT.ToString();
		}
		if (Application.loadedLevel == 4)
		{
			if (GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().mode == 0)
				gameOver.transform.FindChild("Highscore").GetComponent<TextMesh>().text = "Highscore: " + GameObject.Find("Statistics").GetComponent<Statistics>().level2HighscoreNormal.ToString();
			else if (GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().mode == 1)
				gameOver.transform.FindChild("Highscore").GetComponent<TextMesh>().text = "Highscore: " + GameObject.Find("Statistics").GetComponent<Statistics>().level2HighscoreSMFT.ToString();
		}
		if (Application.loadedLevel == 5)
		{
			if (GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().mode == 0)
				gameOver.transform.FindChild("Highscore").GetComponent<TextMesh>().text = "Highscore: " + GameObject.Find("Statistics").GetComponent<Statistics>().level3HighscoreNormal.ToString();
			else if (GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().mode == 1)
				gameOver.transform.FindChild("Highscore").GetComponent<TextMesh>().text = "Highscore: " + GameObject.Find("Statistics").GetComponent<Statistics>().level3HighscoreSMFT.ToString();
		}
	}

	public static void incrementScore()
	{
		instance.score.text = (int.Parse(instance.score.text) + 1).ToString();
	}

	public static int getScore()
	{
		return int.Parse(instance.score.text);
	}

	void OnDestroy()
	{
		GameEventManager.GameOver -= GameOver;
		GameEventManager.GameStart -= GameStart;
	}
}
