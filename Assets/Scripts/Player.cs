using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// 
	public enum State {
		PEAСE,
		HIT,
		JUMP
	}
		
	//
	public float yPositionTop = 2.5f;
	//
	public float yPositionMiddle = 0;
	//
	public float yPositionBottom = -2.5f;
	//
	public float xPositon = -9;
	//
	public float jumpDuration = 1;
	//
	public float hitDuration = 1;
	// 
	public float minSwipeDist = 20f;
	//
	public float maxSwipeTime = 0.3f;
	//
	public float hitMaxX = -5;
	//
	public float hitMinX = -7;

	private int offset;

	private Vector3 fingerStartPos = Vector3.zero;
	private float fingerStartTime = 0f;

	private bool isSwipe = false;

	private float jumpYFrom;
	private float jumpYTo;
	private float jumpTime;

	private State state;

	void OnStart () {
		ResetState ();
	}

	void Update() {
		if (GameManager.instance.isGameOver)
			return;

		if (GameManager.instance.isPause)
			return;

		if (Input.GetMouseButtonDown(0)) {
			fingerStartPos = Input.mousePosition;
			fingerStartTime = Time.time;
			isSwipe = true;
		}

		if(Input.GetMouseButtonUp(0))	{
			Vector2 direction = Input.mousePosition - fingerStartPos;
			float gestureTime = Time.time - fingerStartTime;
			float gestureDist = direction.magnitude;

			if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist) {
				Vector2 swipeType = Vector2.zero;

				if (Mathf.Abs (direction.x) > Mathf.Abs (direction.y)) {
					swipeType = Vector2.right * Mathf.Sign (direction.x);
				} else {
					swipeType = Vector2.up * Mathf.Sign (direction.y);
				}

				if (swipeType.x != 0f) {
					if (swipeType.x > 0f) {
						OnSwipeRight ();
					} else {
						OnSwipeLeft ();
					}
				}

				if (swipeType.y != 0f) {
					if (swipeType.y > 0f) {
						OnSwipeUp ();
					} else {
						OnSwipeDown ();
					}
				}
			} else {
				OnTap ();
			}
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			OnSwipeUp ();
		} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			OnSwipeDown ();
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			OnTap ();
		}
			
		if (state == State.JUMP) {
			float progress = jumpTime / jumpDuration;
			float jumpY = jumpYFrom + progress * (jumpYTo - jumpYFrom);

			transform.position = new Vector3 (xPositon, jumpY, 0);

			jumpTime += Time.deltaTime;
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		Debug.Log ("Collide with Player");

		if (collider.tag == "BigWave") {
			BigWave wave = collider.GetComponent<BigWave> ();
			if (wave.IsShown ()) {
				DoDeadOnWave ();
			}
		}
	}

	public void OnStartGame() {
		ResetState ();
	}

	public void OnGameOver() {
		
	}

	private void ResetState() {
		transform.position = new Vector3 (xPositon, yPositionMiddle);
		offset = 1;
		state = State.PEAСE;
	}

	private void OnSwipeUp() {
		Debug.Log ("OnSwipeUp");

		DoJumpUp ();
	}

	private void OnSwipeDown() {
		Debug.Log ("OnSwipeDown");

		DoJumpDown ();
	}

	private void OnSwipeLeft() {
		Debug.Log ("OnSwipeLeft");
	}

	private void OnSwipeRight() {
		Debug.Log ("OnSwipeRight");
	}

	private void OnTap() {
		Debug.Log ("OnTap");

		DoHit ();
	}

	private void DoJumpUp() {
		if (state != State.PEAСE)
			return;
		
		state = State.JUMP;

		float y = transform.position.y;

		jumpTime = 0;
		jumpYFrom = y;

		if (Mathf.Abs (y - yPositionTop) < 0.01) {
			jumpYTo = yPositionTop;
			offset = 0;
		}

		if (Mathf.Abs(y - yPositionMiddle) < 0.01) {
			jumpYTo = yPositionTop;
			offset = 0;
		}

		if (Mathf.Abs(y - yPositionBottom) < 0.01) {
			jumpYTo = yPositionMiddle;
			offset = 1;
		}

		StartCoroutine (DoPeaceAfterTime(jumpDuration));
	}

	private void DoJumpDown() {
		if (state != State.PEAСE)
			return;

		state = State.JUMP;

		float y = transform.position.y;

		jumpTime = 0;
		jumpYFrom = y;

		if (Mathf.Abs (y - yPositionBottom) < 0.01) {
			jumpYTo = yPositionBottom;
			offset = 2;
		}

		if (Mathf.Abs(y - yPositionMiddle) < 0.01) {
			jumpYTo = yPositionBottom;
			offset = 2;
		}

		if (Mathf.Abs(y - yPositionTop) < 0.01) {
			jumpYTo = yPositionMiddle;
			offset = 1;
		}

		StartCoroutine (DoPeaceAfterTime(jumpDuration));
	}

	private IEnumerator DoPeaceAfterTime(float delay) {
		yield return new WaitForSeconds (delay);

		DoPeace ();
	}

	private void DoPeace() {
		if (state == State.JUMP) {
			transform.position = new Vector3 (xPositon, jumpYTo, 0);
			// Update order
			GetComponent<SortingLayer> ().UpdateOrder (offset);
		}
		state = State.PEAСE;
	}

	private void DoHit() {
		if (state != State.PEAСE)
			return;
		
		state = State.HIT;

		GameScreen.instance.OnHit (offset, hitMinX, hitMaxX);

		StartCoroutine (DoPeaceAfterTime(hitDuration));
	}

	private void DoDeadOnWave() {
		GameManager.instance.GameOver ();
	}

	private void DoDeadOnMissedWave() {
		GameManager.instance.GameOver ();
	}

}
