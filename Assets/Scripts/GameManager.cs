﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	// Очки
	public int score = 0;
	// Пауза
	public bool isPause = false;
	// Конец игры
	public bool isGameOver = false;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addScore(int value) {
		score += value;
	}

	public void StartGame() {
		isGameOver = false;
	}

	public void GameOver() {
		isGameOver = true;
	}
}