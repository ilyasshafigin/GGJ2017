using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour {

	// Минимальное время спауна
	public float minSpawnTime = 2;
	// Максимальное время спауна
	public float maxSpawnTime = 5;
	//
	public float timeSpeed = 0.001f;
	//
	public float spawnX = 10;
	// Скорость перемещения волны
	public float waveSpeed = 0.1f;
	//
	public float waveAcceleration = 0.00001f;
	//
	public float deadX = -10;
	//
	public float[] yOffsets = new float[] {2.5f, 0, -2.5f};

	private IEnumerator currentCoroutine = null;
	public float timeMultiplier = 1;
	public float currentSpeed;
		
	// Use this for initialization
	void Start () {
		timeMultiplier = 1;
		currentSpeed = waveSpeed;
	}

	void Update () {
		if (!GameManager.instance.isGameOver && !GameManager.instance.isPause) {
			timeMultiplier += timeSpeed;
			currentSpeed += waveAcceleration;
		}
	}

	public void OnStartGame() {
		timeMultiplier = 1;
		currentSpeed = waveSpeed;
		currentCoroutine = BigWaveSpawnLoop ();
		StartCoroutine (currentCoroutine);
	}

	public void OnGameOver() {
		if (currentCoroutine != null) {
			StopCoroutine (currentCoroutine);
		}
	}

	private IEnumerator BigWaveSpawnLoop() {
		yield return new WaitForSeconds (Random.Range (minSpawnTime / timeMultiplier, maxSpawnTime / timeMultiplier));

		if (!GameManager.instance.isGameOver) {
			if (!GameManager.instance.isPause)
				SpawnBigWave ();

			currentCoroutine = BigWaveSpawnLoop ();
			StartCoroutine (currentCoroutine);
		}
	}

	private void SpawnBigWave() {
		float x = spawnX + transform.position.x;
		int offset = Random.Range (0, 3);
		float y = transform.position.y + yOffsets[offset];

		BigWave wave = BigWave.CreateWave (offset, currentSpeed, deadX);
		wave.transform.SetParent (transform);
		wave.transform.Translate(new Vector3(x, y, 0));
	}

	// Событие удара
	public void OnHit(int hitOffset, float hitMinX, float hitMaxX) {
		BigWave[] waves = GetComponentsInChildren<BigWave> ();
		for (int i = 0; i < waves.Length; i++) {
			BigWave wave = waves [i];
			bool isHide = wave.IsOnHit (hitOffset, hitMinX, hitMaxX);
			if (isHide) {
				wave.Hide ();

				GameManager.instance.AddScore (1);
			}
		}
	}
}
