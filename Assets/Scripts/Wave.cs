using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

	// Инициализация объекта
	void Initialize(WaveGroup waveGroup) {
		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Создает объект из префаба
	public static Wave CreateWave(WaveGroup waveGroup) {
		GameObject gameObject = (GameObject) Object.Instantiate (Resources.Load ("Prefabs/Wave"));
		Wave wave = gameObject.GetComponent<Wave> ();
		wave.Initialize (waveGroup);
		return wave;
	}
}
