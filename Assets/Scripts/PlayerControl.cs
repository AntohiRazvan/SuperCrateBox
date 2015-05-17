using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
	private int extraJump;
	private bool extraJumpCancel;

	public Animator anim;					// Reference to the player's animator component.
	public float Speed;				// The fastest the player can travel in the x axis.
	public AudioClip jumpClip;			// Array of clips for when the player jumps.
	public AudioClip deathClip;
	public float jumpForce;			// Amount of force added when the player jumps.

	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private Transform moveCheck;
	private bool grounded = false;			// Whether or not the player is grounded.
	private bool canMove = true;
	private float previousHeight = 0;
	void Awake()
	{
		groundCheck = transform.Find("groundCheck");
		moveCheck = transform.Find("moveCheck");
	}

	void Start()
	{
		GameEventManager.GameOver += GameOver;
	}

	void Update()
	{
		if (gameObject != null)
		{
			fallCheck();
			grounded = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
			if (grounded)
			{
				anim.SetBool("isJumping", false);
				extraJump = 0;
				extraJumpCancel = false;
				if (Input.GetButtonDown("Jump"))
				{
					jump = true;
					grounded = false;
				}
			}

			if (Input.GetButtonUp("Jump"))
				extraJumpCancel = true;
		}
	}

	void FixedUpdate()
	{
		float move = Input.GetAxis("Horizontal");
		if (move < 0)
			canMove = !Physics.Linecast(transform.position, moveCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		else
			canMove = !Physics.Linecast(transform.position, moveCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		
		if(canMove)
			rigidbody.velocity = new Vector3(move * Speed, rigidbody.velocity.y, 0f);
	
		anim.SetFloat("speed", Mathf.Abs(move * Speed));

		if (move > 0 && !facingRight)
			Flip();
		else if (move < 0 && facingRight)
			Flip();

		if (jump)
		{
			anim.SetBool("isJumping", true);
			AudioSource.PlayClipAtPoint(jumpClip, transform.position);
			rigidbody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.VelocityChange);
			jump = false;
		}

		if ((Input.GetButton("Jump")) && (extraJump < 20) && (!grounded) && (!extraJumpCancel))
		{
			extraJump++;
			if (extraJump % 2 == 0)
				rigidbody.AddForce(new Vector3(0f, 0.65f, 0f), ForceMode.VelocityChange);
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 newScale = transform.localScale;
		newScale.x *= -1;
		transform.localScale = newScale;
	}

	void fallCheck()
	{
		if (gameObject.transform.position.y < previousHeight)
			anim.SetBool("isFalling", true);
		else if (gameObject.transform.position.y == previousHeight)
			anim.SetBool("isFalling", false);
		previousHeight = transform.position.y;
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "ground")
		{
			extraJumpCancel = true;
			gameObject.rigidbody.velocity = new Vector3(0f, 0f, 0f);
		}

		if(col.gameObject.tag == "Enemy")
		{
			GameEventManager.triggerGameOver();
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.name == "Fire")
			GameEventManager.triggerGameOver();
	}

	void GameOver()
	{
		AudioSource.PlayClipAtPoint(deathClip, transform.position);
		GameEventManager.GameOver -= GameOver;
		GameObject.Find("Statistics").GetComponent<Statistics>().deaths++;
		Destroy(gameObject);
	}

	void OnDestroy()
	{
		GameEventManager.GameOver -= GameOver;
	}
}
