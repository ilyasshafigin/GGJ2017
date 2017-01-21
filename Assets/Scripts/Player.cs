using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int DISTANCE = 30;
	public float deltaY = 10f;
	//private System.DateTime ONE_SECOND = new System.DateTime (0, 0, 0, 0, 0, 1);
	private Touch startTouch;
	private Touch endTouch;
	private System.DateTime startTime;
	private bool isMoveDone = false;

	void FixedUpdate(){
		/*try {
			Touch fTouch = Input.GetTouch (0);
		

		if (fTouch.phase == TouchPhase.Began) {
			startTouch = fTouch;
			endTouch = fTouch;
			startTime = System.DateTime.Now;
		} else if (fTouch.phase == TouchPhase.Moved) {
			endTouch = fTouch;
			if (Mathf.Abs (endTouch.position.y - startTouch.position.y) > deltaY) {
				if (endTouch.position.y - startTouch.position.y > 0) {
					MoveUp ();
					isMoveDone = true;
				} else {
					MoveDown ();
					isMoveDone = true;
				}
			}
		} else if (fTouch.phase == TouchPhase.Stationary) {
			endTouch = fTouch;
			if (Mathf.Abs (endTouch.position.y - startTouch.position.y) < 2 &&
			    Mathf.Abs (endTouch.position.x - startTouch.position.x) < 2) {
				if (ONE_SECOND.CompareTo (System.DateTime.Now.Subtract (startTime)) <= 0) {
					DoSuperHit ();
					isMoveDone = true;
				} else {
					DoHit ();
					isMoveDone = true;
				}
			}
		} else if (fTouch.phase == TouchPhase.Ended) {
			endTouch = fTouch;
			if (Mathf.Abs (endTouch.position.y - startTouch.position.y) > deltaY) {
				if (endTouch.position.y - startTouch.position.y > 0) {
					MoveUp ();
					isMoveDone = true;
				} else {
					MoveDown ();
					isMoveDone = true;
				}
			} else if (Mathf.Abs (endTouch.position.y - startTouch.position.y) < 2 &&
				Mathf.Abs (endTouch.position.x - startTouch.position.x) < 2) {
				if (ONE_SECOND.CompareTo (System.DateTime.Now.Subtract (startTime)) <= 0) {
					DoSuperHit ();
					isMoveDone = true;
				} else {
					DoHit ();
					isMoveDone = true;
				}
			} 
		}
	} catch(System.Exception e) {}*/
		int speed = 1;
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
			// Get movement of the finger since last frame
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

			// Move object across XY plane
			transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
		}
	}

	void MoveUp(){
		transform.Translate (0, DISTANCE, 0);
	}

	void MoveDown(){
		transform.Translate (0, -DISTANCE, 0);
	}

	void DoSuperHit(){

	}

	void DoHit(){

	}

}
