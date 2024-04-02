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
		GetComponent<Renderer>().material.color = tileColors[colorCounter];
	}

	public void Hit(int hitStrength)
	{
		colorCounter += hitStrength;
		if (colorCounter >= 3)
		{
			Destroy(gameObject);
			GameManager.Instance.score.text = int.Parse(GameManager.Instance.score.text) + 10 + "";
			
			if (--GameManager.Instance.tileCount <= 0)
			{
				GameManager.Instance.LevelDone();
				return;
			}

			if (Random.Range(0, 101) < 50)
			{
				Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
			}
			return;
		}
	}
}
