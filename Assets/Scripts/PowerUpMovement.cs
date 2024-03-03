using UnityEngine;

public class PowerUpMovement : MonoBehaviour
{
    private Rigidbody2D rb;
	[SerializeField] private float speed = 3f;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
    {
		rb.velocity = Vector3.down * speed;
    }

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
