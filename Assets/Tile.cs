using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour

{
	[DoNotSerialize]
	public int colorCounter;
	
	[SerializeField]
	private GameObject powerUpPrefab;

	private Color[] tileColors = {
		Color.red,
		Color.yellow,
		Color.green,
		Color.blue
	};

	private void Start()
	{
		Color customColor = tileColors[colorCounter];
		GetComponent<Renderer>().material.color = customColor;
	}

	public void Hit()
	{
		colorCounter++;
		if (colorCounter >= 3)
		{
			Destroy(gameObject);
			GameManager.Instance.score.text = int.Parse(GameManager.Instance.score.text) + 10 + "";
			
			if (--GameManager.tileCount <= 0)
			{

			}

			if (Random.Range(0, 101) > 20)
			{
				Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
			}
			return;
		}


		//Color customColor = tileColors[colorCounter];
		//GetComponent<Renderer>().material.color = customColor;
	}

}
