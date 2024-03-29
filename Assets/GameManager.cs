using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public static int tileCount = 0;

	public TMP_Text score;

	public readonly float outOfBoundsX = -9;

	[SerializeField]
	private TMP_Text lifePoints;
	
	[SerializeField]
	private Transform ball;
	
	[SerializeField]
	private Transform paddle;

	[SerializeField]
	private GameObject tilePrefab;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
		}
	}

	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		if (ball.position.x <= outOfBoundsX)
		{
			int newLifePoints = int.Parse(lifePoints.text[..1]) - 1;
			if (newLifePoints <= 0) GameLost();
			lifePoints.text = newLifePoints == 1 ? "1 Live left!" : newLifePoints + " Lives left";
			lifePoints.GetComponent<AudioSource>().Play();
			ResetBall();
		}
	}

	private void GameLost()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		GetComponent<AudioSource>().Play();
		//Some Timer till the next round so that the sound can play maybe even a start screen with blurred background and start button
		//Invoke or Coroutine
	}

	private void ResetBall()
	{
		ball.position = new Vector3(-4, 0.5f, paddle.position.z);
		ball.gameObject.GetComponent<Ball>().Velocity = new Vector3(0, 0, 0);
	}
}
