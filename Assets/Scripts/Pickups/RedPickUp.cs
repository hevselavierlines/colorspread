using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPickUp : AbstractPickUp {

	void Start() {
		colourName = "RED";
	}

	public override void callPlayerControllerAddFunction (PlayerController pc) {
		pc.addRedLoad (colourAmount);
	}

}
