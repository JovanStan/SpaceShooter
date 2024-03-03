using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
	private Rigidbody2D rb;

	[SerializeField] private float speed = 10f;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		rb.velocity = Vector2.down * speed;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Destroy(gameObject);
			PlayerHealth.instance.DamagePlayer();
		}
	}

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
