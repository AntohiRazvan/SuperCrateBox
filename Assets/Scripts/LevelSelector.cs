using UnityEngine;
using System.Collections;

public class LevelSelector : MonoBehaviour 
{
	public Transform level;
	public Transform mode;
	public AudioClip selectSound;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.DownArrow) && gameObject.transform.position.y > -3)
		{
			AudioSource.PlayClipAtPoint(selectSound, transform.position);
			gameObject.transform.Translate(0f, -2f, 0f);
		}

		if (Input.GetKeyDown(KeyCode.UpArrow) && gameObject.transform.position.y < 1)
		{
			AudioSource.PlayClipAtPoint(selectSound, transform.position);
			gameObject.transform.Translate(0f, 2f, 0f);
		}

		if (gameObject.transform.position.y == 1)
		{
			if (Input.GetKeyDown(KeyCode.RightArrow) && level.position.x > -30f)
			{
				AudioSource.PlayClipAtPoint(selectSound, transform.position);
				level.Translate(-20, 0f, 0f);
			}

			if (Input.GetKeyDown(KeyCode.LeftArrow) && level.position.x < 0f)
			{
				AudioSource.PlayClipAtPoint(selectSound, transform.position);
				level.Translate(20, 0f, 0f);
			}
		}

		if (gameObject.transform.position.y == -1)
		{
			if (Input.GetKeyDown(KeyCode.RightArrow) && mode.position.x > -20f)
			{
				AudioSource.PlayClipAtPoint(selectSound, transform.position);
				mode.Translate(-20, 0f, 0f);
			}

			if (Input.GetKeyDown(KeyCode.LeftArrow) && mode.position.x < 0f)
			{
				AudioSource.PlayClipAtPoint(selectSound, transform.position);
				mode.Translate(20, 0f, 0f);
			}
		}

		if(Input.GetKeyDown(KeyCode.Return))
		{
			if(gameObject.transform.position.y == -3)
			{
				float mode = GameObject.Find("Game Modes").transform.position.x;
				if (mode == 0)
					GameObject.Find("GameOptions").GetComponent<GameOptions>().gameMode = 0;
				else if(mode == -20)
					GameObject.Find("GameOptions").GetComponent<GameOptions>().gameMode = 1;

				float level = GameObject.Find("Levels").transform.position.x;
				if (level == 0)
					Application.LoadLevel("level1");
				else if (level == -20)
					Application.LoadLevel("level2");
				else if (level == -40)
					Application.LoadLevel("level3");
			}
		}
	}

}
