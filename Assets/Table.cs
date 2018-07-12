using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {

	public GameObject firstRow;
	public GameObject secondRow;

	private int numCards;

	void Start () {
		numCards = 0;
	}
	
	void Update () {
		
	}

	void AddCard(GameObject card) {
		if (numCards < 3) {
			card.transform.SetParent(firstRow.transform);
		} else {
			card.transform.SetParent(secondRow.transform);
		}
		numCards++;
	}

	void Clear() {
		numCards = 0;
		foreach (Transform child in firstRow.transform) {
			Destroy(child.gameObject);
		}

		foreach (Transform child in secondRow.transform) {
			Destroy(child.gameObject);
		}
	}
}
