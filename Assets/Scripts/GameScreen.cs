using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreen : MonoBehaviour {

	public static GameScreen instance = null;

	//
	public Waves waves;
	//
	public Player player;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//
	public void OnStartGame() {
		player.OnStartGame ();
		waves.OnStartGame ();
	}

	//
	public void OnGameOver() {
		player.OnGameOver ();
		waves.OnGameOver ();
	}

	//
	public bool OnHit(int hitOffset, float hitMinX, float hitMaxX) {
		return waves.OnHit (hitOffset, hitMinX, hitMaxX);
	}

	public void OnWaveMissed() {
		player.OnWaveMissed ();
	}

}
