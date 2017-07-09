using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour {

	public GameObject door;
	private int rotation;

	void Start () {
		rotation = -91;
	}

	void Update () {
		if (rotation >= -90 && rotation <= -1) {
			rotation += 1;
			door.transform.eulerAngles = new Vector3 (0, rotation, 0);
		}
	}

	void OnTriggerEnter () {
		rotation = -90;
	}

	public void reset () {
		rotation = -91;
		door.transform.eulerAngles = new Vector3 (0, -90, 0);
	}
}
