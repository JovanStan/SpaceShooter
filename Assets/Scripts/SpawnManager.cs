using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
	[SerializeField] private GameObject speedBoostPrefab;
	[SerializeField] private GameObject trippleShotPrefab;
	[SerializeField] private GameObject hearthPickup;

	private float enemySpawnInterval = 3f;
	private float elapsedTime = 0f;

	private void Start()
	{
        StartCoroutine(SpawnEnemy());
		StartCoroutine(SpawnSpeedBoost());
		StartCoroutine(SpawnTrippleShot());
		StartCoroutine(SpawnHeartPickup());
	}

	IEnumerator SpawnEnemy()
	{
		yield return new WaitForSeconds(2f);
		while (!PlayerHealth.instance.IsPlayerDead())
		{
			Vector2 spawnPosition;
			if (Random.Range(0, 2) == 0)
			{
				spawnPosition = new Vector2(-10f, Random.Range(1.5f, 3.5f));
			}
			else
			{
				spawnPosition = new Vector2(10f, Random.Range(1.5f, 3.5f));
			}
			Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
			yield return new WaitForSeconds(enemySpawnInterval); 

			elapsedTime += enemySpawnInterval;
			if (elapsedTime >= 45f)
			{
				enemySpawnInterval = Mathf.Max(0.5f, enemySpawnInterval - 0.1f); 
				elapsedTime = 0f;
			}
		}
	}

	IEnumerator SpawnSpeedBoost()
	{
		yield return new WaitForSeconds(5f);
		while (!PlayerHealth.instance.IsPlayerDead())
		{
			Vector2 spawnPosition = new Vector2(Random.Range(-9f, 9f), 6f);
			Instantiate(speedBoostPrefab, spawnPosition, Quaternion.identity);
			yield return new WaitForSeconds(Random.Range(7f, 12f));
		}
	}

	IEnumerator SpawnTrippleShot()
	{
		yield return new WaitForSeconds(10f);
		while (!PlayerHealth.instance.IsPlayerDead())
		{
			Vector2 spawnPosition = new Vector2(Random.Range(-9f, 9f), 6f);
			Instantiate(trippleShotPrefab, spawnPosition, Quaternion.identity);
			yield return new WaitForSeconds(Random.Range(10f, 15f));
		}
	}

	IEnumerator SpawnHeartPickup()
	{
		yield return new WaitForSeconds(15f);
		while (!PlayerHealth.instance.IsPlayerDead())
		{
			Vector2 spawnPosition = new Vector2(Random.Range(-9f, 9f), 6f);
			Instantiate(hearthPickup, spawnPosition, Quaternion.identity);
			yield return new WaitForSeconds(Random.Range(20f, 30f));
		}
	}
}
