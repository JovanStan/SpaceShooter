using UnityEngine;

public class HeartPickup : MonoBehaviour
{


	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			PlayerHealth health = other.GetComponent<PlayerHealth>();
			health.currentHealth++;
			if(health.currentHealth > health.maxHealth)
			{
				health.currentHealth = health.maxHealth;
			}
			Destroy(gameObject);
			AudioManager.instance.PlayHeartPickupSound();
		}
	}
}
