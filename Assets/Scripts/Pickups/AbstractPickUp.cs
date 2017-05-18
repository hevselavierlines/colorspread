using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractPickUp : MonoBehaviour {

	public int colourAmount = 1;
	public GameObject guiElement;
	public static float guiShownFor = 2.0f;
	protected string colourName = "%NOT SET%";

	private bool collected = false;

	void OnTriggerEnter(Collider other) {
		if (!collected) {
			if (other.gameObject.GetComponent<PlayerController> () != null) {
				// is player colliding with green colour
				collected = true;
				gameObject.GetComponent<Renderer> ().enabled = false;
				gameObject.GetComponent<Collider> ().enabled = false;
				callPlayerControllerAddFunction (other.gameObject.GetComponent<PlayerController>());
				showGUI ();
				Invoke ("hideGUI", guiShownFor);
			}
		}
	}

	public abstract void callPlayerControllerAddFunction (PlayerController pc);

	void showGUI() {
		guiElement.GetComponent<Text> ().text = colourAmount + " " + colourName + " collected...";
		guiElement.SetActive (true);
	}

	void hideGUI() {
		guiElement.SetActive (false);
	}

	public void reset() {
		gameObject.GetComponent<Renderer> ().enabled = true;
		gameObject.GetComponent<Collider> ().enabled = true;
		collected = false;
	}

}
