using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePickUp : AbstractPickUp {

	void Start() {
		colourName = "BLUE";
	}

	public override void callPlayerControllerAddFunction (PlayerController pc) {
		pc.addBlueLoad (colourAmount);
	}

}
