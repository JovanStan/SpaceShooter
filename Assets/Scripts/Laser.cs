using UnityEngine;

public class Laser : MonoBehaviour
{
    private Rigidbody2D rb;

	[SerializeField] private float speed = 10f;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
    {
		rb.velocity = Vector2.up * speed;
    }

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
