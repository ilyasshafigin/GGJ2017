using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour {

	// Минимальное время спауна
	public float minSpawnTime;
	// Максимальное время спауна
	public float maxSpawnTime;
	//
	public float spawnX = 10;
	//
	public int[] orderLayers = new int[] {2, 6, 10};
	//
	public float[] xOffsets = new float[] {2.5f, 0, -2.5f};
		
	// Use this for initialization
	void Start () {
	}

	public void OnStartGame() {
		StartCoroutine (BigWaveSpawnLoop ());
	}

	public void OnGameOver() {
		
	}

	private IEnumerator BigWaveSpawnLoop() {
		yield return new WaitForSeconds (Random.Range (minSpawnTime, maxSpawnTime));

		if (!GameManager.instance.isGameOver) {
			SpawnBigWave ();

			StartCoroutine (BigWaveSpawnLoop ());
		}
	}

	private void SpawnBigWave() {
		float x = spawnX + transform.position.x;
		int offset = Random.Range (0, 2);
		float y = transform.position.y + xOffsets[offset];

		BigWave wave = BigWave.CreateWave (offset);
		wave.transform.SetParent (transform);
		wave.transform.Translate(new Vector3(x, y, 0));
		wave.GetComponent<SpriteRenderer> ().sortingOrder = orderLayers [offset];
	}

	// Событие удара
	public void OnHit(int hitOffset, float hitMinX, float hitMaxX) {
		BigWave[] waves = GetComponentsInChildren<BigWave> ();
		for (int i = 0; i < waves.Length; i++) {
			BigWave wave = waves [i];
			bool isHide = wave.IsOnHit (hitOffset, hitMinX, hitMaxX);
			if (isHide) {
				wave.Hide ();
			}
		}
	}
}
