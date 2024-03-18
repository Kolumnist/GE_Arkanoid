using System;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 velocity;

	public Vector3 Velocity {  
		get => velocity; 
		set =>
			velocity = new Vector3(xSpeed, 0, 0);
	}

    public float xSpeed;
    public float zSpeed;

	// Start is called before the first frame update
	void Start()
    {
        velocity = new Vector3(xSpeed, 0, 0);
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
				velocity.z = normalizedDistance * zSpeed;
				velocity.x = -velocity.x;
                return;

			case ("Wall"):
				velocity.x = -velocity.x;
				return;

			case ("SideWall"):
				velocity.z = -velocity.z;
				return;

			default: 
				break;
		}
	}

}
