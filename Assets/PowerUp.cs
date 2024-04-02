using UnityEngine;

public class PowerUp : MonoBehaviour
{
	public float fallSpeed;

	private enum Power
	{
		BiggerPaddle, // Paddle length increases by 1
		TwinBalls, // A secondary grey twin Ball is summoned
		StrongBall // The original Ball turns bright red and destroys red and yellow tiles instantly
	}

	[SerializeField]
	private GameObject ballCopyPrefab;
	private Vector3 velocity;
	private Power power;

	// Start is called before the first frame update
	void Start()
	{
		velocity = new Vector3(fallSpeed, 0, 0);
		power = (Power)Random.Range(0, 3);

		switch (power)
		{
			case Power.TwinBalls:
				gameObject.GetComponent<Renderer>().material.color = new Color(0.65f, 1, 0.35f, 1);
				break;
			case Power.StrongBall:
				gameObject.GetComponent<Renderer>().material.color = new Color(1, 0.35f, 0.3f, 1);
				break;
		}

		transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
		transform.rotation = Quaternion.Euler(new Vector3(0, 90, 90));
	}

	// Update is called once per frame
	void Update()
	{
		transform.position += velocity * Time.deltaTime;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag != "Paddle")
		{
			return;
		}

		switch (power)
		{
			case Power.BiggerPaddle:
				GameManager.Instance.paddle.localScale += new Vector3(1, 0, 0);
				GameManager.Instance.paddle.gameObject.GetComponent<Paddle>().setMaxAbsolute();
				Invoke(nameof(TimeUpBiggerPaddle), 8);
				break;

			case Power.TwinBalls:
				Instantiate(ballCopyPrefab, new Vector3(other.gameObject.transform.position.x, 0, other.gameObject.transform.position.z), Quaternion.identity);
				Destroy(gameObject, 2);
				break;

			case Power.StrongBall:
				GameManager.Instance.ball.GetComponentInChildren<Light>().color = Color.red;
				GameManager.Instance.ball.GetComponent<Ball>().hitStrength += 2;
				Invoke(nameof(TimeUpStrongBall), 8);
				break;

			default: break;
		}

		gameObject.GetComponent<Renderer>().enabled = false;
		gameObject.GetComponent<Collider>().enabled = false;
	}

	private void TimeUpStrongBall()
	{
		GameManager.Instance.ball.GetComponent<Ball>().hitStrength -= 2;
		if (GameManager.Instance.ball.GetComponent<Ball>().hitStrength == 1)
			GameManager.Instance.ball.GetComponentInChildren<Light>().color = new Color(255, 0, 255, 255);
		Destroy(gameObject);
	}

	private void TimeUpBiggerPaddle()
	{
		GameManager.Instance.paddle.localScale -= new Vector3(1, 0, 0);
		GameManager.Instance.paddle.gameObject.GetComponent<Paddle>().setMaxAbsolute();
		Destroy(gameObject);
	}
}
