using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour 
{
	public GameObject L1Normal;
	public GameObject L1SMFT;
	public GameObject L2Normal;
	public GameObject L2SMFT;
	public GameObject L3Normal;
	public GameObject L3SMFT;

	public GameObject crates;
	public GameObject deaths;
	public GameObject smallKills;
	public GameObject bigKills;
	public GameObject ghostKills;

	void Start()
	{
		Statistics stats = GameObject.Find("Statistics").GetComponent<Statistics>();
		L1Normal.GetComponent<TextMesh>().text = "Normal: " + stats.level1HighscoreNormal.ToString();
		L1SMFT.GetComponent<TextMesh>().text = "SMFT: " + stats.level1HighscoreSMFT.ToString();

		L2Normal.GetComponent<TextMesh>().text = "Normal: " + stats.level2HighscoreNormal.ToString();
		L2SMFT.GetComponent<TextMesh>().text = "SMFT: " + stats.level2HighscoreSMFT.ToString();

		L3Normal.GetComponent<TextMesh>().text = "Normal: " + stats.level3HighscoreNormal.ToString();
		L3SMFT.GetComponent<TextMesh>().text = "SMFT: " + stats.level3HighscoreSMFT.ToString();

		crates.GetComponent<TextMesh>().text = "Crates: " + stats.cratesCollected.ToString();
		deaths.GetComponent<TextMesh>().text = "Deaths: " + stats.deaths.ToString();

		smallKills.GetComponent<TextMesh>().text = stats.smallKills.ToString();
		bigKills.GetComponent<TextMesh>().text = stats.bigKills.ToString();
		ghostKills.GetComponent<TextMesh>().text = stats.ghostKills.ToString();
	}
}
