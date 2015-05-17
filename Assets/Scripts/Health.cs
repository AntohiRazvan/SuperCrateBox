using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int health;
	public Color damaged = Color.red;
	public SpriteRenderer gfx;
 
	public void takeDamage(int damage)
	{
		health -= damage;
		StartCoroutine(registerHit());
		if (health <= 0)
		{
			if (gameObject.name.Contains("Big"))
				GameObject.Find("Statistics").GetComponent<Statistics>().bigKills++;

			if (gameObject.name.Contains("Small"))
				GameObject.Find("Statistics").GetComponent<Statistics>().smallKills++;

			if (gameObject.name.Contains("Ghost"))
				GameObject.Find("Statistics").GetComponent<Statistics>().ghostKills++;
			die();
		}
			
	}

	private void die()
	{


/*		gameObject.GetComponent<BoxCollider>().enabled = false;
		gameObject.rigidbody.freezeRotation = false;
		gameObject.rigidbody.AddForce(Vector3.up * 5f);
		gameObject.rigidbody.AddTorque(Vector3.forward * 10f);
		*/

		Destroy(gameObject);
	}

	public void setHealth(int newHealth)
	{
		health = newHealth;
	}

	IEnumerator registerHit()
	{
		gfx.color = Color.red;
		yield return new WaitForSeconds(0.1f);
		gfx.color = Color.white;
	}
}
