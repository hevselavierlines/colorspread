using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter () {
		PlayerController playerController = GameObject.Find ("Player").GetComponent<PlayerController> ();
		playerController.nextLevel ();
	}
}
