using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	//
	public GameScreen gameScreen;
	//
	public GameOverScreen gameOverScreen;
	//
	public PauseScreen pauseScreen;
	//
	public Text scoreText;

	// Очки
	public int score = 0;
	// Пауза
	public bool isPause = false;
	// Конец игры
	public bool isGameOver = false;

	private bool isResumWait = false;

	// Use this for initialization
	void Start () {
		instance = this;
		StartGame ();
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = score.ToString ();
	}

	public void AddScore(int value) {
		score += value;
	}

	public int GetScore() {
		return score;
	}

	public void StartGame() {
		isGameOver = false;
		score = 0;
		gameScreen.OnStartGame ();
	}

	public void RestartGame() {
		gameOverScreen.Hide ();
		StartGame ();
	}

	public void GameOver() {
		if (isGameOver)
			return;
		
		isGameOver = true;
		gameScreen.OnGameOver ();
		gameOverScreen.Show ();
	}

	public void PauseGame() {
		if (isPause || isResumWait)
			return;
		
		isPause = true;
		pauseScreen.Show ();
		//Time.timeScale = 0;
	}

	public void ResumeGame() {
		if (!isPause || isResumWait)
			return;
		
		pauseScreen.Hide ();
		isResumWait = true;
		StartCoroutine (DoResumeAfterTime(2));
	}

	public void ExitGame() {
		Application.Quit ();
	}

	private IEnumerator DoResumeAfterTime(float delay) {
		yield return new WaitForSeconds (delay);

		isPause = false;
		isResumWait = false;
		//Time.timeScale = 1;
	}

}
