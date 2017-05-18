using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPickUp : AbstractPickUp {

	void Start() {
		colourName = "GREEN";
	}

	public override void callPlayerControllerAddFunction (PlayerController pc) {
		pc.addGreenLoad (colourAmount);
	}

}
