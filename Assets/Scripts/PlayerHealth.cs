using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public static PlayerHealth instance;

	public int currentHealth;
	public int maxHealth = 5;
	private bool isPlayedDead = false;

	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		currentHealth = maxHealth;
	}

	private void Update()
	{
		UIManager.instance.healthText.text = currentHealth + "/" + maxHealth;
		UIManager.instance.healthSlider.maxValue = maxHealth;
		UIManager.instance.healthSlider.value = currentHealth;
	}

	public bool IsPlayerDead()
	{
		return isPlayedDead;
	}

	public void DamagePlayer()
	{
		currentHealth--;
		AudioManager.instance.HurtSound();

		if (currentHealth <= 0)
		{
			currentHealth = 0;
			isPlayedDead = true;
			UnityAds.instance.ShowInterstitial();
			UIManager.instance.gameOverPanel.SetActive(true);
			AudioManager.instance.PlayGameOverSound();
			PlayerController.instance.CheckForBestScore();
		}
	}
}
