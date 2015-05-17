using UnityEngine;
using System.Collections;

public class GameEventManager 
{
	public delegate void GameEvent();
	public static event GameEvent GameStart, GameOver;

	public static void triggerGameStart()
	{
		if (GameStart != null)
		{
			GameStart();
		}
	}

	public static void triggerGameOver()
	{
		if (GameOver != null)
		{
			GameOver();
		}
	}
}
