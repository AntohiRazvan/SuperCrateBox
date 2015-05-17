using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public AudioClip bulletSound;
	public int damage;
	void Start () {
		AudioSource.PlayClipAtPoint(bulletSound, transform.position);
	}
	
	void Update () {
	
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Enemy")
		{
			col.gameObject.GetComponent<Health>().takeDamage(damage);
		}
		Destroy(gameObject);
	}
}
