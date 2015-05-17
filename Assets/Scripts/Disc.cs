using UnityEngine;
using System.Collections;

public class Disc : MonoBehaviour 
{
	int damage = 5;
	int health = 2;
	Vector3 velocity;
	float hitTime = 0f;

	void Awake()
	{
		GameEventManager.GameOver += GameOver;
	}

	void Update()	
	{
		velocity = rigidbody.velocity;
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Enemy")
		{

			if ((hitTime + 0.099f < Time.time))
			{
				hitTime = Time.time;
				col.gameObject.GetComponent<Health>().takeDamage(damage);
				StartCoroutine(deactivateCollider());
			}
			rigidbody.velocity = velocity;
		}

		if(col.gameObject.tag == "frame" ||  col.gameObject.tag == "ground")
		{
			health--;
			if(health == 0)
			{
				Destroy(gameObject);
			}
		}

		if (col.gameObject.tag == "Player" && health == 1)
			GameEventManager.triggerGameOver();
	}

	void GameOver()
	{
		Destroy(gameObject);
	}

	void OnDestroy()
	{
		GameEventManager.GameOver -= GameOver;
	}

	IEnumerator deactivateCollider()
	{
		collider.enabled = false;
		yield return new WaitForSeconds(0.05f);
		collider.enabled = true;
	}
}
