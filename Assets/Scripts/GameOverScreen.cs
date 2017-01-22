using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour {

	public static GameOverScreen instance = null;

	//
	public Text playerText;
	//
	public Text highText;

	// Use this for initialization
	void Start () {
		instance = this;
		Hide ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0) || Input.GetKeyDown (KeyCode.Space)) {
			GameManager.instance.RestartGame ();
		}
	}

	public void Show() {
		gameObject.SetActive (true);
	}

	public void Hide() {
		gameObject.SetActive (false);
	}

}
