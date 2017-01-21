using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour {

	//
	public int[] orderLayers = new int[] {2, 6, 10};

	//
	public void UpdateOrder(int offset) {
		GetComponent<SpriteRenderer>().sortingOrder = orderLayers[offset];
	}
}
