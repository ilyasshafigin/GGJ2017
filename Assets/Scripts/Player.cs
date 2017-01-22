using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// 
	public enum State {
		IDLE,
		HIT,
		JUMP,
		DEAD
	}
		
	//
	public float yPositionTop = 2.5f;
	//
	public float yPositionMiddle = 0;
	//
	public float yPositionBottom = -2.5f;
	//
	public float xPositon = -7;
	//
	public float jumpDuration = 0.15f;
	//
	public float hitDuration = 0.15f;
	// 
	public float minSwipeDist = 20f;
	//
	public float maxSwipeTime = 0.3f;
	//
	public float hitMaxX = -3;
	//
	public float hitMinX = -9;

	//
	public AudioClip hitSound;
	//
	public AudioClip deadSound;
	//
	public AudioClip jumpSound;

	private int offset;

	private Vector3 fingerStartPos = Vector3.zero;
	private float fingerStartTime = 0f;

	private bool isSwipe = false;

	private float jumpYFrom;
	private float jumpYTo;
	private float jumpTime;

	private State state;

	private Animator animator;
	private int jumpHash = Animator.StringToHash ("jump");
	private int hitHash = Animator.StringToHash ("attack");

	private bool isWaitGameOver = false;

	void Start () {
		animator = GetComponent<Animator> ();
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
		state = State.IDLE;
		isWaitGameOver = false;
		animator.ResetTrigger (hitHash);
		animator.ResetTrigger (jumpHash);
		animator.SetBool ("dead", false);
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
		if (state != State.IDLE)
			return;
		
		state = State.JUMP;

		PlaySound (jumpSound);
		animator.SetTrigger (jumpHash);

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

		StartCoroutine (DoIdleAfterTime(jumpDuration));
	}

	private void DoJumpDown() {
		if (state != State.IDLE)
			return;

		state = State.JUMP;

		PlaySound (jumpSound);
		animator.SetTrigger (jumpHash);

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

		StartCoroutine (DoIdleAfterTime(jumpDuration));
	}

	private IEnumerator DoIdleAfterTime(float delay) {
		yield return new WaitForSeconds (delay);

		DoIdle ();
	}

	private IEnumerator DoGameOverAfterTime(float delay) {
		yield return new WaitForSeconds (delay);

		isWaitGameOver = false;
		GameManager.instance.GameOver ();
	}

	private void DoIdle() {
		if (state == State.JUMP) {
			transform.position = new Vector3 (xPositon, jumpYTo, 0);
			// Update order
			GetComponent<SortingLayer> ().UpdateOrder (offset);
		}
			
		state = State.IDLE;

		animator.ResetTrigger (hitHash);
		animator.ResetTrigger (jumpHash);
		animator.SetBool ("dead", false);
	}

	private void DoHit() {
		if (state != State.IDLE)
			return;
		
		state = State.HIT;

		if (GameScreen.instance.OnHit (offset, hitMinX, hitMaxX)) {
			PlaySound (hitSound);
		}

		animator.SetTrigger (hitHash);

		StartCoroutine (DoIdleAfterTime(hitDuration));
	}

	private void DoDeadOnWave() {
		PlaySound (deadSound);
		animator.SetBool ("dead", true);

		isWaitGameOver = true;
		StartCoroutine (DoGameOverAfterTime (2));
	}

	public void OnWaveMissed() {
		DoDeadOnMissedWave ();
	}

	private void DoDeadOnMissedWave() {
		if (isWaitGameOver)
			return;
		
		PlaySound (deadSound);
		animator.SetBool ("dead", true);

		isWaitGameOver = true;
		StartCoroutine (DoGameOverAfterTime (2));
	}

	private void PlaySound(AudioClip sound) {
		AudioSource audioSource = GetComponent<AudioSource> ();
		audioSource.Stop ();
		audioSource.loop = false;
		audioSource.clip = sound;
		audioSource.Play ();
	}

}
