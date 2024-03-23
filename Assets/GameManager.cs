using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private TMP_Text lifePoints;
	[SerializeField]
	private Transform ball;
	[SerializeField]
	private Transform paddle;

	public static GameManager instance;
	public readonly float outOfBoundsX = -9;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("");
			Destroy(this);
		}
		instance = this;
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
	}

	private void ResetBall()
	{
		ball.position = new Vector3(-4, 0.5f, paddle.position.z);
		ball.gameObject.GetComponent<Ball>().Velocity = new Vector3(0, 0, 0);
	}
}
