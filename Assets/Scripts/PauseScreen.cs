using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour {

	public static PauseScreen instance = null;

	// Use this for initialization
	void Start () {
		instance = this;
		Hide ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Show() {
		gameObject.SetActive (true);
	}

	public void Hide() {
		gameObject.SetActive (false);
	}

}
