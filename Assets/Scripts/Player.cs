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
	public float yPositionTop = 5;
	//
	public float yPositionMiddle = 0;
	//
	public float yPositionBottom = -5;
	//
	public float xPositon = -7;
	//
	public float jumpDuration = 1;
	//
	public float hitDuration = 1;

	private Vector3 fingerStartPos = Vector3.zero;
	private float fingerStartTime = 0f;

	private bool isSwipe = false;
	private float minSwipeDist = 50f;
	private float maxSwipeTime = 0.5f;

	private State state;

	void OnStart () {
		transform.position = new Vector3 (xPositon, yPositionMiddle);
		state = State.PEAСE;
	}

	void FixedUpdate() {
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
			}
		}
	}

	private void OnSwipeUp() {
		Debug.Log ("OnSwipeUp");

		MoveUp ();
	}

	private void OnSwipeDown() {
		Debug.Log ("OnSwipeDown");

		MoveDown ();
	}

	private void OnSwipeLeft() {
		Debug.Log ("OnSwipeLeft");
	}

	private void OnSwipeRight() {
		Debug.Log ("OnSwipeRight");
	}

	void MoveUp() {
		float y = transform.position.y;
		if (Mathf.Abs(y - yPositionTop) < 0.01)
			return;

		if (Mathf.Abs(y - yPositionMiddle) < 0.01) {
			//transform.localPosition.y = yPositionTop;
			transform.position = new Vector3 (xPositon, yPositionTop, 0);
			return;
		}

		if (Mathf.Abs(y - yPositionBottom) < 0.01) {
			//transform.localPosition.y = yPositionMiddle;
			transform.position = new Vector3 (xPositon, yPositionMiddle, 0);
			return;
		}

		transform.position = new Vector3 (xPositon, yPositionBottom, 0);
	}

	void MoveDown() {
		float y = transform.position.y;
		if (Mathf.Abs(y - yPositionBottom) < 0.01)
			return;

		if (Mathf.Abs(y - yPositionMiddle) < 0.01) {
			//transform.localPosition.y = yPositionMiddle;
			transform.position = new Vector3 (xPositon, yPositionBottom, 0);
			return;
		}

		if (Mathf.Abs(y - yPositionTop) < 0.01) {
			//transform.localPosition.y = yPositionTop;
			transform.position = new Vector3 (xPositon, yPositionMiddle, 0);
			return;
		}

		transform.position = new Vector3 (xPositon, yPositionTop, 0);
	}

	void DoJump() {
		state = State.JUMP;

		StartCoroutine (DoPeaceAfterTime(jumpDuration));
	}

	private IEnumerator DoPeaceAfterTime(float delay) {
		yield return new WaitForSeconds (delay);

		DoPeace ();
	}

	void DoPeace() {
		state = State.PEAСE;
	}

	void DoHit() {
		state = State.HIT;

		StartCoroutine (DoPeaceAfterTime(hitDuration));
	}

}
