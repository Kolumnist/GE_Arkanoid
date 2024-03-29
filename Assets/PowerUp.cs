using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float fallSpeed;

	private Transform paddle;
	private Vector3 velocity;

	// Start is called before the first frame update
	void Start()
    {
		velocity = new Vector3(fallSpeed, 0, 0);

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
		paddle = other.transform;
		if (other.gameObject.tag == "Paddle")
		{
			other.transform.localScale += new Vector3(1, 0, 0);
			other.gameObject.GetComponent<Paddle>().setMaxAbsolute();
			Invoke(nameof(TimeUpForBiggerPowerUp), 8);
			gameObject.GetComponent<Renderer>().enabled = false;
			gameObject.GetComponent<Collider>().enabled = false;
		}
	}

	private void TimeUpForBiggerPowerUp()
	{
		paddle.localScale -= new Vector3(1, 0, 0);
		paddle.gameObject.GetComponent<Paddle>().setMaxAbsolute();
		Destroy(gameObject);
	}
}
