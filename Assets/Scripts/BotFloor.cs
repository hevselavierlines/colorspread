using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotFloor : MonoBehaviour {

	Collider recent;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<PlayerController> () != null) {
			// is player
			if (gameObject.GetComponent<Renderer> ().material.name != "tile3active (Instance)") {
				// kill player
				recent = other;
				Invoke ("killPlayer", 0.3f);
			}
		}
	}

	void killPlayer() {
		recent.gameObject.GetComponent<PlayerController>().die();
	}
}
