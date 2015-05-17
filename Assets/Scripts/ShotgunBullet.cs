using UnityEngine;
using System.Collections;

public class ShotgunBullet : MonoBehaviour {
	public AudioClip shot;

	void Start()
	{
		AudioSource.PlayClipAtPoint(shot, transform.position);
	}
	void OnCollisionEnter(Collision col)	
	{
		int damage = 1;
		if (col.gameObject.tag == "Enemy")
		{
			col.gameObject.GetComponent<Health>().takeDamage(damage);
			Destroy(gameObject);
		}
	}
}
