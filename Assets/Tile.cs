using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	private Color[] tileColors = {
		new Color(100, 100, 100),
		new Color(255, 100, 0),
		new Color(100, 0, 255),
		new Color(0, 255, 100)
	};

	private int colorCounter = 0;

	private void Start()
	{
		colorCounter = Random.Range(0, 3);
	}

	public void Hit()
	{
		colorCounter++;
		//Debug.Log(colorCounter);
		if (colorCounter > 3) Destroy(gameObject);

		Color customColor = tileColors[colorCounter];
		GetComponent<Renderer>().material.color = customColor;
	}

}
