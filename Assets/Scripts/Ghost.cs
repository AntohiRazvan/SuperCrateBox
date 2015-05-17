using UnityEngine;
using System.Collections;

public class Ghost : MonoBehaviour {

	public float speed;
	private Transform target;
	public Transform collisionCheck;
	float spawnTime;

	void Start () 
	{
		GameEventManager.GameStart += GameStart;
		target = GameObject.FindGameObjectWithTag("Player").transform;
		spawnTime = Time.time;
		gameObject.rigidbody.AddForce(Vector3.down * speed, ForceMode.VelocityChange);
	}
	
	void FixedUpdate () 
	{
		
		try
		{
			if (spawnTime + 1f < Time.time)
			{
				directionCheck();
				gameObject.rigidbody.velocity = Vector3.zero;

				if ((target.position.y > gameObject.transform.position.y))
				{
					Vector3 direction = target.position - transform.position;
					direction = direction.normalized;
					gameObject.rigidbody.AddForce(direction * speed, ForceMode.VelocityChange);
				}

				if (!Physics.Linecast(transform.position, collisionCheck.position, 1 << LayerMask.NameToLayer("Ground")))
				{
					Vector3 direction = target.position - transform.position;
					direction = direction.normalized;
					gameObject.rigidbody.AddForce(direction * speed, ForceMode.VelocityChange);
				}
				else
				{

					if (target.position.x >= gameObject.transform.position.x)
					{
						gameObject.rigidbody.AddForce(Vector3.right * speed, ForceMode.VelocityChange);
					}
					else
					{
						gameObject.rigidbody.AddForce(Vector3.left * speed, ForceMode.VelocityChange);
					}
				}
			}
		}
		catch(System.Exception e)
		{
		
		}
	}

	void directionCheck()
	{
		if (gameObject.rigidbody.velocity.x >= 0f)
			gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		else
			gameObject.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
	}

	void GameStart()
	{
		Destroy(gameObject);
	}

	void OnDestroy()
	{
		GameEventManager.GameStart -= GameStart;
	}
}
