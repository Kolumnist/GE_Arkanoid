using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private Transform playArea;

    private float zMaxAbsolute;

	public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        zMaxAbsolute = (playArea.localScale.z * 10) * 0.5f - transform.localScale.x * 0.5f - 0.25f;
	}

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxis("Horizontal");

        float zNew = transform.position.z + speed * -direction * Time.deltaTime;
        zNew = Mathf.Clamp(zNew, -zMaxAbsolute, +zMaxAbsolute);

		transform.position = new Vector3(transform.position.x, transform.position.y, zNew);
    }
}
