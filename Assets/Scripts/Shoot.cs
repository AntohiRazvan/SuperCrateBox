using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shoot : MonoBehaviour {

	//Weapons
	public GameObject pistol;
	public GameObject dualPistol;
	public GameObject machinegun;
	public GameObject bazooka;
	public GameObject magnum;
	public GameObject discgun;
	public GameObject flamethrower;
	public GameObject knife;
	public GameObject lasergun;
	public GameObject mine;
	public GameObject minigun;
	public GameObject shotgun;
	//Ammo
	public GameObject bullet;
	public GameObject magnumBullet;
	public GameObject shotgunBullet;
	public GameObject disc;

	//Weapon options
	public float pistolROF;
	public float machinegunROF;
	public float minigunROF;
	public float shotgunROF;
	public float discgunROF;
	public float bulletSpeed = 10;
	public float machinegunPushback;
	public float minigunPushback;
	public float shotgunPushback;

	public AudioClip pickupSound;

	private float direction = -1f;
	private float lastShot = 0f;
	private GameObject currentWeapon;

	private List<GameObject> availableWeapons = new List<GameObject>();

	void Start () 
	{
		availableWeapons.Add(dualPistol);
		availableWeapons.Add(shotgun);
		availableWeapons.Add(machinegun);
		availableWeapons.Add(magnum);
		availableWeapons.Add(minigun);
		availableWeapons.Add(discgun);
		currentWeapon = pistol;
		currentWeapon.SetActive(true);
	}
	
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.X))
		{
			switch(currentWeapon.name)
			{
				case "Pistol":
					shootPistol();
					break;
				case "DualPistol":
					shootDualPistol();
					break;
				case "Shotgun":
					shootShotgun();
					break;
				case "Magnum":
					shootMagnum();
					break;
				case "Discgun":
					shootDiscgun();
					break;
				default:
					break;
			}
		}
		if (Input.GetKey(KeyCode.X))
		{
			switch (currentWeapon.name)
			{
				case "Minigun":
					shootMinigun();
					break;
				case "Machinegun":
					shootMachinegun();
					break;
			}
		}
	}


	void shootPistol()
	{
		if ((lastShot + pistolROF) <= Time.time)
		{
			lastShot = Time.time;
			Vector3 spawn = pistol.transform.Find("bullet_spawn").position;
			GameObject shot = (GameObject)Instantiate(bullet, spawn, Quaternion.identity);
			shot.rigidbody.velocity = new Vector3(gameObject.rigidbody.velocity.x, 0f, 0f);
			Destroy(shot, 15f);
			if (gameObject.GetComponent<PlayerControl>().facingRight)
				shot.rigidbody.velocity += new Vector3(bulletSpeed, 0f, 0f);
			else
				shot.rigidbody.velocity += new Vector3(-bulletSpeed, 0f, 0f);
		}
	}

	void shootDualPistol()
	{
		if ((lastShot + pistolROF) <= Time.time)
		{
			lastShot = Time.time;
			Vector3 spawn = dualPistol.transform.Find("bullet_spawn1").position;
			GameObject shot1 = (GameObject)Instantiate(bullet, spawn, Quaternion.identity);
			spawn = dualPistol.transform.Find("bullet_spawn2").position;
			GameObject shot2 = (GameObject)Instantiate(bullet, spawn, Quaternion.identity);
			shot1.rigidbody.velocity = new Vector3(gameObject.rigidbody.velocity.x, 0f, 0f);
			shot2.rigidbody.velocity = new Vector3(-gameObject.rigidbody.velocity.x, 0f, 0f);
			Destroy(shot1, 15f);
			Destroy(shot2, 15f);
			if (gameObject.GetComponent<PlayerControl>().facingRight)
			{
				shot1.rigidbody.velocity += new Vector3(bulletSpeed, 0f, 0f);
				shot2.rigidbody.velocity += new Vector3(-bulletSpeed, 0f, 0f);
			}
			else
			{
				shot1.rigidbody.velocity += new Vector3(-bulletSpeed, 0f, 0f);
				shot2.rigidbody.velocity += new Vector3(bulletSpeed, 0f, 0f);
			}
		}
	}

	void shootMachinegun()
	{
		if ((lastShot + machinegunROF) <= Time.time)
		{
			lastShot = Time.time;
			Vector3 spawn = machinegun.transform.Find("bullet_spawn").position;
			direction = Random.Range(-0.5f, 0.5f);
			GameObject shot = (GameObject)Instantiate(bullet, spawn, Quaternion.Euler(new Vector3(0f, 0f, direction)));
			shot.rigidbody.velocity = new Vector3(gameObject.rigidbody.velocity.x, 0f, 0f);
			Destroy(shot, 15f);
			if (gameObject.GetComponent<PlayerControl>().facingRight)
			{
				shot.rigidbody.velocity += new Vector3(bulletSpeed, direction, 0f);
				gameObject.transform.Translate(new Vector3(machinegunPushback, 0f, 0f));
			}
			else
			{
				shot.rigidbody.velocity += new Vector3(-bulletSpeed, direction, 0f);
				gameObject.transform.Translate(new Vector3(-machinegunPushback, 0f, 0f));
			}
		}
	}

	void shootMagnum()
	{
		if ((lastShot + pistolROF) <= Time.time)
		{
			lastShot = Time.time;
			Vector3 spawn = magnum.transform.Find("bullet_spawn").position;
			GameObject shot = (GameObject)Instantiate(magnumBullet, spawn, Quaternion.identity);
			shot.rigidbody.velocity = new Vector3(gameObject.rigidbody.velocity.x, 0f, 0f);
			Destroy(shot, 15f);
			if (gameObject.GetComponent<PlayerControl>().facingRight)
				shot.rigidbody.velocity += new Vector3(bulletSpeed, 0f, 0f);
			else
				shot.rigidbody.velocity += new Vector3(-bulletSpeed, 0f, 0f);
		}
	}

	void shootMinigun()
	{
		if ((lastShot + minigunROF) <= Time.time)
		{
			lastShot = Time.time;
			Vector3 spawn = minigun.transform.Find("bullet_spawn").position;
			direction = Random.Range(-5f, 5f);
			GameObject shot = (GameObject)Instantiate(bullet, spawn, Quaternion.Euler(new Vector3(0f, 0f, direction)));
			shot.rigidbody.velocity = new Vector3(gameObject.rigidbody.velocity.x, 0f, 0f);
			Destroy(shot, 15f);
			if (gameObject.GetComponent<PlayerControl>().facingRight)
			{
				shot.rigidbody.velocity += new Vector3(bulletSpeed, direction, 0f);
				gameObject.transform.Translate(new Vector3(minigunPushback, 0f, 0f));
			}
			else
			{
				shot.rigidbody.velocity += new Vector3(-bulletSpeed, direction, 0f);
				gameObject.transform.Translate(new Vector3(-minigunPushback, 0f, 0f));
			}
		}
	}

	void shootShotgun()
	{
		if ((lastShot + shotgunROF) <= Time.time)
		{
			float maxAngle = 3f;
			float minAngle = -1.5f;
			float ttl = 0.3f;
			float minSpeed = 6;
			float maxSpeed = 10;
			float speed;
			lastShot = Time.time;
			Vector3 spawn = shotgun.transform.Find("bullet_spawn").position;
			GameObject shot1 = (GameObject)Instantiate(shotgunBullet, spawn, Quaternion.identity);
			GameObject shot2 = (GameObject)Instantiate(shotgunBullet, spawn, Quaternion.identity);
			GameObject shot3 = (GameObject)Instantiate(shotgunBullet, spawn, Quaternion.identity);
			GameObject shot4 = (GameObject)Instantiate(shotgunBullet, spawn, Quaternion.identity);
			GameObject shot5 = (GameObject)Instantiate(shotgunBullet, spawn, Quaternion.identity);
			GameObject shot6 = (GameObject)Instantiate(shotgunBullet, spawn, Quaternion.identity);
			GameObject shot7 = (GameObject)Instantiate(shotgunBullet, spawn, Quaternion.identity);
			shot1.rigidbody.velocity = new Vector3(gameObject.rigidbody.velocity.x, 0f, 0f);
			shot2.rigidbody.velocity = new Vector3(gameObject.rigidbody.velocity.x, 0f, 0f);
			shot3.rigidbody.velocity = new Vector3(gameObject.rigidbody.velocity.x, 0f, 0f);
			shot4.rigidbody.velocity = new Vector3(gameObject.rigidbody.velocity.x, 0f, 0f);
			shot5.rigidbody.velocity = new Vector3(gameObject.rigidbody.velocity.x, 0f, 0f);
			shot6.rigidbody.velocity = new Vector3(gameObject.rigidbody.velocity.x, 0f, 0f);
			shot7.rigidbody.velocity = new Vector3(gameObject.rigidbody.velocity.x, 0f, 0f);
			Destroy(shot1, ttl);
			Destroy(shot2, ttl);
			Destroy(shot3, ttl);
			Destroy(shot4, ttl);
			Destroy(shot5, ttl);
			Destroy(shot6, ttl);
			Destroy(shot7, ttl);
			if (gameObject.GetComponent<PlayerControl>().facingRight)
			{
				gameObject.transform.Translate(new Vector3(shotgunPushback, 0f, 0f));
				direction = Random.Range(minAngle, maxAngle);
				speed = Random.Range(minSpeed, maxSpeed);
				shot1.rigidbody.velocity += new Vector3(speed, direction, 0f);
				direction = Random.Range(minAngle, maxAngle);
				speed = Random.Range(minSpeed, maxSpeed);
				shot2.rigidbody.velocity += new Vector3(speed, direction, 0f);
				direction = Random.Range(minAngle, maxAngle);
				speed = Random.Range(minSpeed, maxSpeed);
				shot3.rigidbody.velocity += new Vector3(speed, direction, 0f);
				direction = Random.Range(minAngle, maxAngle);
				speed = Random.Range(minSpeed, maxSpeed);
				shot4.rigidbody.velocity += new Vector3(speed, direction, 0f);
				direction = Random.Range(minAngle, maxAngle);
				speed = Random.Range(minSpeed, maxSpeed);
				shot5.rigidbody.velocity += new Vector3(speed, direction, 0f);
				direction = Random.Range(minAngle, maxAngle);
				speed = Random.Range(minSpeed, maxSpeed); 
				shot6.rigidbody.velocity += new Vector3(speed, direction, 0f);
				direction = Random.Range(minAngle, maxAngle);
				speed = Random.Range(minSpeed, maxSpeed);
				shot7.rigidbody.velocity += new Vector3(speed, direction, 0f);
			}
			else
			{
				gameObject.transform.Translate(new Vector3(-shotgunPushback, 0f, 0f));
				direction = Random.Range(minAngle, maxAngle);
				speed = Random.Range(minSpeed, maxSpeed);
				shot1.rigidbody.velocity += new Vector3(-speed, direction, 0f);
				direction = Random.Range(minAngle, maxAngle);
				speed = Random.Range(minSpeed, maxSpeed);
				shot2.rigidbody.velocity += new Vector3(-speed, direction, 0f);
				direction = Random.Range(minAngle, maxAngle);
				speed = Random.Range(minSpeed, maxSpeed);
				shot3.rigidbody.velocity += new Vector3(-speed, direction, 0f);
				direction = Random.Range(minAngle, maxAngle);
				speed = Random.Range(minSpeed, maxSpeed);
				shot4.rigidbody.velocity += new Vector3(-speed, direction, 0f);
				direction = Random.Range(minAngle, maxAngle);
				speed = Random.Range(minSpeed, maxSpeed);
				shot5.rigidbody.velocity += new Vector3(-speed, direction, 0f);
				direction = Random.Range(minAngle, maxAngle);
				speed = Random.Range(minSpeed, maxSpeed);
				shot6.rigidbody.velocity += new Vector3(-speed, direction, 0f);
				direction = Random.Range(minAngle, maxAngle);
				speed = Random.Range(minSpeed, maxSpeed);
				shot7.rigidbody.velocity += new Vector3(-speed, direction, 0f);
			}
		}
	}

	void shootDiscgun()
	{
		if ((lastShot + discgunROF) <= Time.time)
		{
			lastShot = Time.time;
			Vector3 spawn = discgun.transform.Find("bullet_spawn").position;
			GameObject shot = (GameObject)Instantiate(disc, spawn, Quaternion.identity);
			shot.rigidbody.velocity = new Vector3(gameObject.rigidbody.velocity.x, 0f, 0f);
			Destroy(shot, 20f);
			if (gameObject.GetComponent<PlayerControl>().facingRight)
				shot.rigidbody.velocity += new Vector3(bulletSpeed, 0f, 0f);
			else
				shot.rigidbody.velocity += new Vector3(-bulletSpeed, 0f, 0f);
		}
	}

	void switchWeapon()
	{
		int index;
		GameObject newWeapon = new GameObject();
		do
		{
			index = Random.Range(0, availableWeapons.Count);
			newWeapon = availableWeapons[index];
		} while (newWeapon == currentWeapon);
		
		currentWeapon.SetActive(false);
		currentWeapon = newWeapon;
		currentWeapon.SetActive(true);
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "Crate(Clone)")
		{
			AudioSource.PlayClipAtPoint(pickupSound, transform.position);
			GUIManager.incrementScore();
			switchWeapon();
			GameObject.Find("Statistics").GetComponent<Statistics>().cratesCollected++;
			Destroy(col.gameObject);
		}
	}
}
