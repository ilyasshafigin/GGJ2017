using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {

	void GotoGameScreen() {
		SceneManager.LoadScene ("Scenes/GameScene");
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)) {
			GotoGameScreen ();
		}
	}
}
