﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWave : MonoBehaviour {

	// Скорость перемещения волны
	private float speed = 0.3f;
	//
	private float xDead = -10;
	//
	private int offset;

	// Инициализация объекта
	void Initialize(int waveOffset, float waveSpeed, float waveXDead) {
		offset = waveOffset;
		speed = waveSpeed;
		xDead = waveXDead;

		// Update order
		GetComponent<SortingLayer> ().UpdateOrder (offset);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.isPause)
			return;
		
		transform.Translate (new Vector3 (-speed, 0, 0));

		if (GameManager.instance.isGameOver) {
			Destroy (gameObject);
			return;
		}

		if (transform.position.x <= xDead) {
			OnCoast ();
		}
	}

	private void OnCoast() {
		// Если персонаж не убил волну
		if (IsShown()) {
			GameScreen.instance.OnWaveMissed ();
		}
		Destroy (gameObject);
	}

	//
	public bool IsShown() {
		return GetComponent<SpriteRenderer> ().enabled;
	}

	// На расстоянии удара?
	public bool IsOnHit(int hitOffset, float hitMinX, float hitMaxX) {
		return GetComponent<SpriteRenderer>().enabled &&
			offset == hitOffset &&
			transform.position.x < hitMaxX &&
			transform.position.x > hitMinX; 
	}

	//
	public void Hide() {
		GetComponent<SpriteRenderer> ().enabled = false;
	}

	// Создает объект из префаба
	public static BigWave CreateWave(int offset, float speed, float xDead) {
		GameObject gameObject = (GameObject) Object.Instantiate (Resources.Load ("Prefabs/BigWave"));
		BigWave wave = gameObject.GetComponent<BigWave> ();
		wave.Initialize (offset, speed, xDead);
		return wave;
	}

}
