using System;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 velocity;

	public Vector3 Velocity {  
		get => velocity; 
		set =>
			velocity = new Vector3(xMaxSpeed, 0, 0);
	}

    public float xMaxSpeed;
	public float paddleDeviation;

	// Start is called before the first frame update
	void Start()
    {
        velocity = new Vector3(xMaxSpeed, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
		//s = v*t
		transform.position += velocity * Time.deltaTime;
    }

	private void OnTriggerEnter(Collider other)
	{
        GetComponent<AudioSource>().Play();

        switch(other.transform.tag)
        {
            case ("Paddle"):
				float actualDistance = transform.position.z - other.transform.position.z;
				float maxDistance = transform.localScale.z * 0.5f + other.transform.localScale.z * 0.5f;
				float normalizedDistance = actualDistance / maxDistance;

				float angle = paddleDeviation*(Mathf.PI / 4) * normalizedDistance;
				velocity.z = velocity.magnitude * Mathf.Sin(angle);
				velocity.x = velocity.magnitude * Mathf.Cos(angle);

				velocity /= Mathf.Abs(velocity.magnitude / -xMaxSpeed);
				return;

			case ("Wall"):
				velocity.x = -velocity.x * 1.01f;
				return;

			case ("SideWall"):
				velocity.z = -velocity.z * 1.01f;
				return;

			case ("Tile"):
				var collisionPoint = other.ClosestPoint(transform.position);
				
				//normale rechnung verbessern
				var normale = transform.position-collisionPoint;
				if (Mathf.Abs(normale.z) > Mathf.Abs(normale.x))
				{
					normale.z /= Mathf.Abs(normale.z);
					normale.x = 0;
				}
				else
				{
					normale.x /= Mathf.Abs(normale.x);
					normale.z = 0;
				}

				velocity = Vector3.Reflect(velocity, normale.normalized);
				velocity *= 1.01f;
				Debug.Log(velocity.magnitude);

				other.GetComponent<Tile>().Hit();
				return;
			default: 
				break;
		}
	}

}
