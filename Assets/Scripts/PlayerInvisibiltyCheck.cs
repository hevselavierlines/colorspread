using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvisibiltyCheck : MonoBehaviour {

	public GameObject overlay;
	protected float time;
	protected bool measureTime;
	public float NEEDED_TIME = 0.4f;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Floor") {
			if (other.gameObject.GetComponent<Renderer> ().material.name == "tile3active (Instance)") {
				this.overlay.SetActive (true);
				this.measureTime = false;
			} 
		}
	}

	void OnTriggerExit(Collider other) {
		this.measureTime = true;
	}

	void Update() {
		if (measureTime == true) {
			this.time += Time.deltaTime;
			if (this.time >= NEEDED_TIME) {
				this.overlay.SetActive (false);
			}
		} else {
			this.time = 0.0f;
		}
	}
}
