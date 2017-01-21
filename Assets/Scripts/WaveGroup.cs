using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGroup : MonoBehaviour {

	// Минимальное время спауна
	public float minSpawnTime;
	// Максимальное время спауна
	public float maxSpawnTime;

	private LinkedList<Wave> waves;
	private LinkedList<BigWave> bigWaves;

	private IEnumerator BigWaveSpawnLoop() {
		yield return new WaitForSeconds (Random.Range (minSpawnTime, maxSpawnTime));

		if (!GameManager.instance.isGameOver) {
			SpawnBigWave ();

			StartCoroutine (BigWaveSpawnLoop ());
		}
	}

	//
	void Awake() {
		waves = new LinkedList<Wave> ();
		bigWaves = new LinkedList<BigWave> ();
	}

	// Use this for initialization
	void Start () {
		SpawnWaves ();
		StartCoroutine (BigWaveSpawnLoop ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void SpawnWaves() {
		BoxCollider2D bound = GetComponent<BoxCollider2D>();
		Vector2 offset = bound.offset;
		Vector2 size = bound.size;
		float halfWidth = size.x / 2 * transform.localScale.x;

		float x = offset.x - halfWidth + transform.position.x;
		float y = offset.y + transform.position.y;
		while (x < offset.x + halfWidth) {
			Wave wave = Wave.CreateWave (this);
			wave.transform.SetParent (transform);
			wave.transform.Translate(new Vector3(x, y, 0));
			waves.AddLast (wave);

			BoxCollider2D waveBound = wave.GetComponent<BoxCollider2D>();
			Vector2 waveSize = waveBound.size;
			x += waveSize.x * wave.transform.localScale.x;
		}
	}

	private void SpawnBigWave() {
		BoxCollider2D bound = GetComponent<BoxCollider2D>();
		Vector2 offset = bound.offset;
		Vector2 size = bound.size;
		float halfWidth = size.x / 2 * transform.localScale.x;

		float x = offset.x + halfWidth + transform.position.x;
		float y = offset.y + transform.position.y;

		BigWave wave = BigWave.CreateWave (this);
		wave.transform.SetParent (transform);
		wave.transform.Translate(new Vector3(x, y, 0));
		bigWaves.AddLast (wave);
	}
}
