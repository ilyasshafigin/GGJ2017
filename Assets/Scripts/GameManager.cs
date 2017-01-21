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
	public Text scoreText;

	// Очки
	public int score = 0;
	// Пауза
	public bool isPause = false;
	// Конец игры
	public bool isGameOver = false;

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

	public void StartGame() {
		isGameOver = false;
		score = 0;
		gameScreen.OnStartGame ();
	}

	public void RestartGame() {
		CloseGameOverScreen ();
		StartGame ();
	}

	public void GameOver() {
		isGameOver = true;
		gameScreen.OnGameOver ();
		OpenGameOverScreen ();
	}

	private void OpenGameOverScreen() {
		gameOverScreen.Show ();
	}

	private void CloseGameOverScreen() {
		gameOverScreen.Hide ();
	}

}
