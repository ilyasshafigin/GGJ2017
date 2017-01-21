using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

	public float speed = 0.08f;
	private Vector3 initialPositon;
	public float amplitudeX = 0.4f;
	public float amplitudeY = 0.05f;
	private float phase;

	// Use this for initialization
	void Start () {
		initialPositon = new Vector3 (transform.position.x, transform.position.y, 0);
		phase = Random.value * Mathf.PI;
	}
	
	// Update is called once per frame
	void Update () {
		phase += speed;
		float xoff = amplitudeX * Mathf.Cos (phase);
		float yoff = amplitudeY * Mathf.Sin (phase);
		transform.position = initialPositon + new Vector3 (xoff, yoff, 0);
	}

	// Создает объект из префаба
	public static Wave CreateWave() {
		GameObject gameObject = (GameObject) Object.Instantiate (Resources.Load ("Prefabs/Wave"));
		Wave wave = gameObject.GetComponent<Wave> ();
		return wave;
	}
}
