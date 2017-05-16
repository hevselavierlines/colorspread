using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPickUp : MonoBehaviour {

	public int colourAmount = 1;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<PlayerController> () != null) {
			// is player colliding with red colour
			other.gameObject.GetComponent<PlayerController> ().addRedLoad(colourAmount);
			gameObject.SetActive (false);
		}
	}
}
