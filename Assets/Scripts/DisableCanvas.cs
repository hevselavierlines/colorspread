using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCanvas : MonoBehaviour {

	public GameObject canvas;

	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt ("CameraMovementActive") == 1) {
			canvas.SetActive (true);
		} else {
			canvas.SetActive (false);
		}
	}
}
