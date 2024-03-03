using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
	public static PlayerController instance;

	private Rigidbody2D rb;
	private Vector2 moveDir;

	[SerializeField] private float speed = 5f;

	[SerializeField] private GameObject laserPrefab;
	[SerializeField] private GameObject trippleShotLaserPrefab;
	[SerializeField] private Transform firePoint;

	private bool canShoot = true;
	private bool isTrippleShotActive = false;

	private int score;
	private int bestScore;

	[SerializeField] private GameObject engineEffect;
	[SerializeField] private ParticleSystem muzzleFlashEffect;

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
		rb = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		UIManager.instance.scoreText.text = "Score: " + score;
		bestScore = PlayerPrefs.GetInt("Best_Score", 0);
		UIManager.instance.bestScoreText.text = "Best Score: " + bestScore;
	}

	private void Update()
	{
		Movement();
		ConstrainMovement();
		Shoot();
	}

	private void Shoot()
	{
		if (CrossPlatformInputManager.GetButtonDown("Shoot") && canShoot)
		{
			if (isTrippleShotActive)
			{
				Instantiate(trippleShotLaserPrefab, firePoint.position + new Vector3(-1.2f, 0), Quaternion.identity);
			}
			else
			{
				Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
			}
			AudioManager.instance.PlayLaserSound();
			muzzleFlashEffect.Play();
			StartCoroutine(ResetShoot());
		}
	}

	private IEnumerator ResetShoot()
	{
		canShoot = false;
		yield return new WaitForSeconds(.2f);
		canShoot = true;
	}

	private void Movement()
	{
		float hor = CrossPlatformInputManager.GetAxisRaw("Horizontal");
		float ver = CrossPlatformInputManager.GetAxisRaw("Vertical");

		moveDir = new Vector2(hor, ver);
		moveDir.Normalize();

		rb.velocity = moveDir * speed;
		PlayEngineEffect();
	}

	private void PlayEngineEffect()
	{
		if (rb.velocity.x != 0 || rb.velocity.y != 0)
		{
			engineEffect.SetActive(true);
		}
		else
		{
			engineEffect.SetActive(false);
		}
	}

	private void ConstrainMovement()
	{
		//appear on another side after player leave screen
		if (transform.position.x > 11f)
		{
			transform.position = new Vector2(-11f, transform.position.y);
		}
		else if (transform.position.x < -11f)
		{
			transform.position = new Vector2(11f, transform.position.y);
		}

		//y constrain
		if (transform.position.y < -4f)
		{
			transform.position = new Vector2(transform.position.x, -4f);
		} else if (transform.position.y > 1f)
		{
			transform.position = new Vector2(transform.position.x, 1f);
		}
	}

	public void AddScore(int amount)
	{
		score += amount;
		UIManager.instance.scoreText.text = "Score: " + score;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "SpeedBoost")
		{
			StartCoroutine(SpeedBoostCO());
			AudioManager.instance.PlayPowerUpSound();
			Destroy(other.gameObject);
		}

		if(other.tag == "TrippleShot")
		{
			StartCoroutine(TrippleShotCO());
			AudioManager.instance.PlayPowerUpSound();
			Destroy(other.gameObject);
		}
	}

	private IEnumerator SpeedBoostCO()
	{
		speed *= 2f;
		yield return new WaitForSeconds(4f);
		speed /= 2f;
	}

	private IEnumerator TrippleShotCO()
	{
		isTrippleShotActive = true;
		yield return new WaitForSeconds(5f);
		isTrippleShotActive = false;
	}

	public void CheckForBestScore()
	{
		if(score > bestScore)
		{
			bestScore = score;
			UIManager.instance.bestScoreText.text = "Best Score: " + bestScore;
			PlayerPrefs.SetInt("Best_Score", bestScore);
		}
	}
}
