using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWave : MonoBehaviour {

	// Скорость перемещения волны
	public float speed = 1;

	// Инициализация объекта
	void Initialize(WaveGroup waveGroup) {

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (-speed, 0, 0));
	}



	// Создает объект из префаба
	public static BigWave CreateWave(WaveGroup waveGroup) {
		GameObject gameObject = (GameObject) Object.Instantiate (Resources.Load ("Prefabs/BigWave"));
		BigWave wave = gameObject.GetComponent<BigWave> ();
		wave.Initialize (waveGroup);
		return wave;
	}

}
