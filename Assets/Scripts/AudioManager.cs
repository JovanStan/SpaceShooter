using UnityEngine;

public class AudioManager : MonoBehaviour
{
   public static AudioManager instance;

	private AudioSource audioSource;
	public AudioClip buttonClick;
	public AudioClip laserSFX;
	public AudioClip powerupSFX;
	public AudioClip explosionSFX;
	public AudioClip heartPickupSFX;
	public AudioClip gameOverSound;
	public AudioClip hurtSFX;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		audioSource = GetComponent<AudioSource>();
	}

	public void PlayButtonSound()
	{
		audioSource.PlayOneShot(buttonClick);
	}

	public void PlayLaserSound()
	{
		audioSource.PlayOneShot(laserSFX);
	}

	public void PlayPowerUpSound()
	{
		audioSource.PlayOneShot(powerupSFX);
	}

	public void PlayExplosionSound()
	{
		audioSource.PlayOneShot(explosionSFX);
	}

	public void PlayHeartPickupSound()
	{
		audioSource.PlayOneShot(heartPickupSFX);
	}

	public void PlayGameOverSound()
	{
		audioSource.PlayOneShot(gameOverSound);
	}

	public void HurtSound()
	{
		audioSource.PlayOneShot(hurtSFX);
	}
}
