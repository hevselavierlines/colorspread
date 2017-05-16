using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPickUp : MonoBehaviour {

	public int colourAmount = 1;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<PlayerController> () != null) {
			// is player colliding with Green colour
			other.gameObject.GetComponent<PlayerController> ().addGreenLoad(colourAmount);
			gameObject.SetActive (false);
		}
	}
}
