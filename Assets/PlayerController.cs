using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	CharacterController controller;
	public float gravity = -3.711f;
	public float movementSpeed = 5.0f;
	public float mouseSensivity = 100.0f;
	public float jumpSpeed = 10.0f;
	public float slowJumpSpeed;
	private float verticalAngle = 0.0f;
	private float maxVerticalAngle = 90.0f;
	private bool grounded = true;
	private float fallSpeed = 0.0f;
	private float gravityRotationStart = 0.0f;
	public Camera mainCamera;
	public Material original;
	public Material selection1;
	public Material colorSet1;
	public Material selection2;
	public Material colorSet2;
	public float raycastLength;
	private Collider oldSelection;
	private short startJumping = 0;
	private GameObject resetPos;
	private int deadCounter = 0;
	private string currentWorld = "Level1";
	public int currentLevel = 1;

	private short jumpers = 0;
	private short shrinkers = 0;
	private short invisibility = 0;

	private int selectionMode = 2;
	public Text jumpText;
	public Image jumpIcon;
	public Text shrinkText;
	public Image shrinkIcon;
	private bool crouch = false;
	public GameObject underWaterGui;
	public GameObject pickedRedGui;
	public GameObject pickedGreenGui;
	public GameObject pickedBlueGui;
	public GameObject pickedColourGui;
	private bool firstTimeSelection = true;

	void changeSelection() {
		/*
		// checks if colour change make sense
		int amountDifferentColours = 0;
		if (jumpers > 0)
			amountDifferentColours++;
		if (shrinkers > 0)
			amountDifferentColours++;
		if (invisibility > 0)
			amountDifferentColours++;
		if (amountDifferentColours < 2)
			return;
		*/
		// surpresses the showed picked colour when you start the game
		if (!firstTimeSelection) {
			showColourPick (selectionMode);
			Invoke ("hideColourPick", 1);
		} else {
			firstTimeSelection = false;
		}

		if (selectionMode == 1) {
			selectionMode = 2;

			Color color = jumpIcon.color;
			color.a = 0.5f;
			jumpIcon.color = color;

			color = shrinkIcon.color;
			color.a = 1.0f;
			shrinkIcon.color = color;
		} else if (selectionMode == 2) {
			selectionMode = 1;

			Color color = jumpIcon.color;
			color.a = 1.0f;
			jumpIcon.color = color;

			color = shrinkIcon.color;
			color.a = 0.5f;
			shrinkIcon.color = color;
		}

		if (oldSelection != null) {
			Renderer oldMaterial = oldSelection.GetComponent<Renderer>();
			if (!oldMaterial.material.name.Contains ("active")) {
				oldMaterial.material = original;
			}
		}
	}

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		resetPos = GameObject.Find(currentWorld + "/StartPoint");
		jumpText.text = jumpers + "";
		shrinkText.text = shrinkers + "";


		changeSelection ();
		resetPlayer ();
	}

	void resetPlayer() {
		transform.position = resetPos.transform.position;
		jumpers = 0;
		shrinkers = 0;
		invisibility = 0;
		jumpText.text = "0";
		shrinkText.text = "0";
	}

	void resetTiles() {
		GameObject level = GameObject.Find (currentWorld);
		Renderer[] renderer = level.GetComponentsInChildren<Renderer> ();
		foreach (Renderer rend in renderer) {
			if(rend.material.name.Contains("tile")) {
				rend.material = original;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate (0, Input.GetAxis ("Mouse X") * mouseSensivity, 0);
		verticalAngle -= Input.GetAxis ("Mouse Y") * mouseSensivity;
		verticalAngle = Mathf.Clamp (verticalAngle, gravityRotationStart - maxVerticalAngle, gravityRotationStart + maxVerticalAngle);
		Quaternion localRotation = Quaternion.Euler (verticalAngle, 0, 0);
		mainCamera.transform.localRotation = localRotation;

		float horizontalMovement = Input.GetAxis ("Horizontal") * movementSpeed;
		float verticalMovement = Input.GetAxis ("Vertical") * movementSpeed;
		float reset = Input.GetAxis ("Reset");

		if (grounded) {
			fallSpeed = 0;
			if (startJumping == 1) {
				fallSpeed = jumpSpeed;
				startJumping = 0;
			} else if (startJumping == 2) {
				fallSpeed = slowJumpSpeed;
				startJumping = 0;
			}
		} else {
			fallSpeed += gravity * Time.deltaTime;
		}

		if (transform.position.y < 0) {
			underWaterGui.SetActive(true);
			deadCounter++;
			if (deadCounter > 120) {
				deadCounter = 0;
				die ();
			}
		}

		// Resets the player when he press the key "r"
		if (reset == 1f) {
			die ();
		}
			
		CollisionFlags flags;
		Vector3 motion = new Vector3 (horizontalMovement, fallSpeed, verticalMovement);

		motion = Quaternion.Euler (0, 0, 0) * motion;
		motion = transform.TransformDirection (motion);

		flags = controller.Move (motion * Time.deltaTime);

		Vector3 fwd = transform.TransformDirection(localRotation * Vector3.forward);
		RaycastHit hit;
		if (Physics.Raycast (mainCamera.transform.position, fwd, out hit, raycastLength)) {
			if (selectionMode == 1 && jumpers > 0) {
				if (hit.collider.tag == "Floor") { 
					if (oldSelection != null) {
						if (hit.collider.name != oldSelection.name) {
							Renderer oldMaterial = oldSelection.GetComponent<Renderer> ();
							Renderer newMaterial = hit.collider.GetComponent<Renderer> ();
							bool oldContains = oldMaterial.material.name.Contains ("active");
							bool newContains = newMaterial.material.name.Contains ("active");
							if (!oldContains && !newContains) {
								oldMaterial.material = original;
								newMaterial.material = selection1;
							} else {
								if (newContains && !oldContains) {
									oldMaterial.material = original;
								}
								if (oldContains && !newContains) {
									newMaterial.material = selection1;
								}
							}
						}
					}
					oldSelection = hit.collider;
				}
			} else if (selectionMode == 2 && shrinkers > 0) {
				if (hit.collider.tag == "Floor") { 
					if (oldSelection != null) {
						if (hit.collider.name != oldSelection.name) {
							Renderer oldMaterial = oldSelection.GetComponent<Renderer> ();
							Renderer newMaterial = hit.collider.GetComponent<Renderer> ();
							bool oldContains = oldMaterial.material.name.Contains ("active");
							bool newContains = newMaterial.material.name.Contains ("active");
							if (!oldContains && !newContains) {
								oldMaterial.material = original;
								newMaterial.material = selection2;
							} else {
								if (newContains && !oldContains) {
									oldMaterial.material = original;
								}
								if (oldContains && !newContains) {
									newMaterial.material = selection2;
								}
							}
						}
					}
					oldSelection = hit.collider;
				}
			}
		} else {
			if (oldSelection != null) {
				Renderer oldMaterial = oldSelection.GetComponent<Renderer>();
				if (!oldMaterial.material.name.Contains ("active")) {
					oldMaterial.material = original;
				}
			}
		}

		if (Input.GetMouseButtonDown (0)) {
			Renderer oldMaterial = oldSelection.GetComponent<Renderer> ();

			if (selectionMode == 1) {
				if (jumpers > 0) {
					oldMaterial.material = colorSet1;
					jumpers--;
					jumpText.text = jumpers + "";
				}
			} else if (selectionMode == 2) {
				if (shrinkers > 0) {
					oldMaterial.material = colorSet2;
					shrinkers--;
					shrinkText.text = shrinkers + "";
				}
			}
		}

		if (Input.GetKeyUp ("c")) {
			changeSelection ();
		}

		grounded = ((flags & CollisionFlags.Below) != 0);
		if (Input.GetKeyDown ("space")) {
			startJumping = 2;
		}
		if (Input.GetKeyUp ("backspace")) {
			resetPlayer ();
		}
		if (crouch) {
			transform.localScale = new Vector3(0,2.0f,0);
		} else {
			transform.localScale = new Vector3(0,4.0f,0);
		}

	}

	public void nextLevel() {
		currentLevel += 1;
		currentWorld = "Level" + currentLevel;
		resetPos = GameObject.Find(currentWorld + "/StartPoint");
		die ();
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.collider.tag == "Floor") {
			Material material = hit.collider.GetComponent<Renderer> ().material;
			if (material.name.Equals ("tile1active (Instance)")) {
				grounded = true;
				startJumping = 1;
			}
			if (material.name.Equals ("tile2active (Instance)")) {
				grounded = true;
				crouch = true;
			} else {
				crouch = false;
			}
		}
	}

	/// <summary>
	/// Shows the colour pick.
	/// </summary>
	/// <param name="colour">Red 1 Green 2 Blue 3 None 0</param>
	void showColourPick(int colour) {
		pickedRedGui.SetActive(false);
		pickedGreenGui.SetActive(false);
		pickedBlueGui.SetActive(false);

		switch (colour) {
		case 1:
			pickedGreenGui.SetActive(true);
			pickedColourGui.GetComponent<Image> ().color = new Color (0, 255, 0, 255);
			break;
		case 2:
			pickedRedGui.SetActive(true);
			pickedColourGui.GetComponent<Image> ().color = new Color (255, 0, 0, 255);
			break;
		case 3:
			pickedBlueGui.SetActive(true);
			pickedColourGui.GetComponent<Image> ().color = new Color (0, 0, 255, 255);
			break;
		default:
			break;
		}
	}

	/// <summary>
	/// Hides the colour pick.
	/// </summary>
	void hideColourPick() {
		showColourPick (0);
	}

	public void addRedLoad(int amount) {
		jumpers += (short)amount;
		jumpText.text = jumpers + "";
	}

	public void addGreenLoad(int amount) {
		shrinkers += (short)amount;
		shrinkText.text = shrinkers + "";
	}

	void die() {
		resetPlayer();
		resetTiles();
		underWaterGui.SetActive(false);
		foreach(GameObject go in GameObject.FindGameObjectsWithTag("Collectable")) {
			Debug.Log ("123");
			go.SetActive (true);
		}
	}
}
