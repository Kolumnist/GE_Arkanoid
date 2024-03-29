using UnityEngine;

public class Level : MonoBehaviour
{
	public static int currentLevel = 0;
	
	public float zSpaceBetween;
	public float xSpaceBetween;
	public (float, float) topLeftCorner = (8.5f, 6.25f);
	
	[SerializeField]
	private GameObject tilePrefab;

	// 11 horizontal
	// 6 down for each area
	private int[][] levels = new int[3][];
	private int[] levelOne = new int[]
	{
	//  1  2  3  4  5  6  7  8  9  10 11
 		-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1,
		-1, -1, -1, 2, -1, -1, -1, 2, -1, -1, -1,
		1, 1, 1, 1, -1,-1, -1, 1, 1, 1, 1,
		1, 1, 1, 1, -1, -1, -1, 1, 1, 1, 1,
		1, 1, 1, 1, -1, -1, -1, 1, 1, 1, 1,
		-1, -1, -1, 2, -1, -1, -1, 2, -1, -1, -1,
	};
	private int[] levelTwo = new int[]
	{
		1, 3, 0, 3, 0, 3, 0, 3, 0, 3, 1,
		-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
		2, 1, 1, -1, 1, 1, 1, -1, 1, 1, 2,
		0, 0, 0, 0, -1, -1, -1, 3, 3, 3, 3,
		3, -1, 3, 3, 1, 1, 1, 0, 0, -1, 0,
		-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1,
	};
	private int[] levelThree = new int[]
	{
 		0, -1, 1, 2, -1, 0, -1, 1, 2, -1, 0,
		3, 0, -1, 1, 2, -1, 0, -1, 1, 2, -1,
		3, -1, 0, -1, 1, 2, -1, 0, -1, 1, 2,
		3, -1, 3, 0, -1, 1, 2, -1, 0, -1, 1,
		3, -1, 3, -1, 0, -1, 1, 2, -1, 0, -1,
		3, -1, 3, -1, 3, 0, -1, 1, 2, -1, 0,
	};

	private float zPosition;
	private float xPosition;

	void Start()
	{
		levels[0] = levelOne;
		levels[1] = levelTwo;
		levels[2] = levelThree;

		xPosition = topLeftCorner.Item1;
		zPosition = topLeftCorner.Item2;
		PlaceCurrentLevel();
	}

	private void PlaceCurrentLevel()
	{
		for (int i = 0; i < 66; i++)
		{
			if (i % 11 == 0)
			{
				xPosition -= xSpaceBetween;
				zPosition = topLeftCorner.Item2;
			}

			if (levels[currentLevel][i] > -1)
			{
				GameObject tile = Instantiate(tilePrefab, new Vector3(xPosition, 0, zPosition), Quaternion.identity);
				tile.GetComponent<Tile>().colorCounter = levels[currentLevel][i];
				GameManager.tileCount++;
			}

			zPosition -= zSpaceBetween + tilePrefab.transform.localScale.z;
		}
	}

}
