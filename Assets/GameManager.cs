using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public int tileCount = 0;

	public TMP_Text score;

	public readonly float outOfBoundsX = -9;

	public Transform ball;

	public Transform paddle;

	[SerializeField]
	private TMP_Text lifePoints;

	[SerializeField]
	private TMP_Text loser;

	[SerializeField]
	private GameObject tilePrefab;

	public void LevelDone()
	{
		if(++Level.currentLevel < 3)
			gameObject.GetComponent<Level>().LoadNewLevel();
	}

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
			if (newLifePoints == 0)
			{
				ball.gameObject.GetComponent<Ball>().startSpeed = 0;
				loser.text = loser.text + "\n" + "Score: " + score.text;
				loser.gameObject.SetActive(true);
				Invoke(nameof(GameLost), 2);
			}

			lifePoints.text = newLifePoints == 1 ? "1 Live left!" : newLifePoints + " Lives left";
			lifePoints.GetComponent<AudioSource>().Play();
			ResetBall();
		}
	}

	private void GameLost()
	{
		Level.currentLevel = 0;
		tileCount = 0;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		loser.gameObject.SetActive(false);
		Invoke(nameof(WaitForPlayerBrainToComprehendWhatHappened), 1);
	}

	private void ResetBall()
	{
		ball.position = new Vector3(-4, 0.5f, paddle.position.z);
		ball.gameObject.GetComponent<Ball>().Velocity = new Vector3(0, 0, 0);
		score.text = int.Parse(score.text) - 50 + "";
		Invoke(nameof(WaitForPlayerBrainToComprehendWhatHappened), 1);
	}

	private void WaitForPlayerBrainToComprehendWhatHappened()
	{
		lifePoints.GetComponent<AudioSource>().Stop();
		ball.gameObject.GetComponent<Ball>().Velocity = new Vector3(ball.gameObject.GetComponent<Ball>().startSpeed, 0, 0);
	}
}
