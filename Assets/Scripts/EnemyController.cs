using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;

	[SerializeField] private float randomSpeed;

	[SerializeField] private GameObject laserPrefab;
	[SerializeField] private Transform firePoint;
	[SerializeField] private int health;

	[SerializeField] GameObject enemyDeathEffect;

	private int scoreToAdd = 10; 

	private bool canFire;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (PlayerHealth.instance.IsPlayerDead()) return;
		Movement();
		Shoot();
	}

	private void Movement()
	{
		randomSpeed = Random.Range(2, 5);
		if (transform.position.x == -10f)
		{
			rb.velocity = Vector3.right * randomSpeed;
		}
		else if (transform.position.x == 10f)
		{
			rb.velocity = Vector3.left * randomSpeed;
		}
	}

	private void Shoot()
	{
		if (canFire)
		{
			GameObject enemyLaser = Instantiate(laserPrefab, firePoint.position, Quaternion.identity);
			StartCoroutine(ResetCanShoot());
		}
	}

	private IEnumerator ResetCanShoot()
	{
		canFire = false;
		yield return new WaitForSeconds(Random.Range(2f, 4f));
		canFire = true;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			Destroy(gameObject);
			PlayerController.instance.AddScore(scoreToAdd);
			AudioManager.instance.PlayExplosionSound();
			PlayerHealth.instance.DamagePlayer();
		}

		if (other.tag == "Laser")
		{
			health--;
			AudioManager.instance.HurtSound();
			if (health <= 0)
			{
				KillEnemy();
			}
			Destroy(other.gameObject);
		}
	}

	private void KillEnemy()
	{
		PlayerController.instance.AddScore(scoreToAdd);
		AudioManager.instance.PlayExplosionSound();
		GameObject effect = Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 2f);
		Destroy(gameObject);
	}

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}

	private void OnBecameVisible()
	{
		canFire = true;
	}
}
