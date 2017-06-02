using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvisibiltyCheck : MonoBehaviour {

	public GameObject overlay;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Floor") {
			if (other.gameObject.GetComponent<Renderer> ().material.name == "tile3active (Instance)") {
				this.overlay.SetActive (true);
			} 
		}
	}

	void OnTriggerExit(Collider other) {
		this.overlay.SetActive (false);
	}
}
