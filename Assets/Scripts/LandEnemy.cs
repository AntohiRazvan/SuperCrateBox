using UnityEngine;
using System.Collections;

public class LandEnemy : MonoBehaviour {

	public float speed;
	public Animator anim;
	public bool facingRight;
	public GameObject enragedEnemy;

	private Transform enemySpawner;
	private Transform groundCheck;
	private bool grounded;
	
	void Awake ()
	{
		facingRight = true;
		groundCheck = transform.Find("groundCheck");
		enemySpawner = GameObject.Find("EnemySpawner").transform;
	}

	void Start()
	{
		GameEventManager.GameStart += GameStart;
	}

	void FixedUpdate()
	{
		gameObject.rigidbody.isKinematic = false;
		fallCheck();
		if (Mathf.Abs(gameObject.rigidbody.velocity.x) < speed && grounded)
		{
			gameObject.rigidbody.velocity = Vector3.zero;
			if(facingRight)
				gameObject.rigidbody.AddForce(Vector3.right * speed, ForceMode.VelocityChange);
			else
				gameObject.rigidbody.AddForce(Vector3.right * (-speed), ForceMode.VelocityChange);
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "frame")
			flip();
		if (col.gameObject.tag == "bullet")
			gameObject.rigidbody.isKinematic = true;
	}

	void OnTriggerEnter(Collider col)
	{
		enrage();
	}

	void flip()
	{
		Vector3 newScale = transform.localScale;
		newScale.x *= -1;
		transform.localScale = newScale;
		gameObject.rigidbody.velocity = new Vector3(0f, gameObject.rigidbody.velocity.y, 0f);
		facingRight = !facingRight;
	}

	void fallCheck()
	{
		grounded = Physics.Linecast(gameObject.transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		if (grounded)
			anim.SetBool("isFalling", false);
		else
			anim.SetBool("isFalling", true);
	}

	void GameStart()
	{
		Destroy(gameObject);
	}

	void enrage()
	{
		GameObject enemy = (GameObject)Instantiate(enragedEnemy, enemySpawner.position, Quaternion.identity);
		int direction = Random.Range(1, 3);
		if (direction == 2)
		{
			enemy.GetComponent<LandEnemy>().facingRight = false;
			enemy.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
		}
		enemy.GetComponent<Health>().setHealth(gameObject.GetComponent<Health>().health);
		Destroy(gameObject);
	}

	void OnDestroy()
	{
		GameEventManager.GameStart -= GameStart;
	}
}
