﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RigidbodyCharacterMotor))]
public class ConfigurablePlayerControl : MonoBehaviour {

    [Tooltip("What button to listen to in order to make the camera swing behind the player. Unity default is Right Click.")]
    public string snapCameraButton = "Fire2";
    [Tooltip("The camera that the player is using. This script bases input on the rotation of this camera.")]
    public OrbitFollowCamera playerCamera;

    private RigidbodyCharacterMotor character;
    public RigidbodyCharacterMotor Character { get { return character; } }

    [Tooltip("What axis to listen to for horizontal move input. Unity default is A and D.")]
    public string moveHorizontalAxis = "Horizontal";
    [Tooltip("What axis to lisen to for vertical move input. Unity default is W and S.")]
    public string moveVerticalAxis = "Vertical";

    [Tooltip("What button to listen to for jump button input. Unity default is Space.")]
    public string jumpButton = "Jump";
    private bool wasJumpHeld = false;

    [Tooltip("What button to listen to for fire button input. Unity default is Left Click.")]
    public string fireButton = "Fire1";
    public PrototypeWeapon weapon;

	// Use this for initialization
	void Start () {
        character = GetComponent<RigidbodyCharacterMotor>();
	}

    void Update() {
        character.moveInput.x = Input.GetAxis(moveHorizontalAxis);
        character.moveInput.z = Input.GetAxis(moveVerticalAxis);

        character.moveInput *= character.moveInput.magnitude;
        character.moveInput *= 3;

        if (character.moveInput.magnitude > 1f) {
            character.moveInput.Normalize();
        }

        //Lastly, rotate the input to be rotated along with the object.
        Quaternion rotation = Quaternion.Euler(0, playerCamera.transform.rotation.eulerAngles.y, 0);
        character.moveInput = rotation * character.moveInput;


        //Detect the rising edge case ourselves, going above 60 FPS is causing errors in reading the jump input for some reason.
        if (Input.GetButton(jumpButton) && !wasJumpHeld) {
            character.jumpInput = true;
        }
        wasJumpHeld = Input.GetButton(jumpButton);

        if (Input.GetButton(fireButton) && weapon) {
            weapon.Fire();
        }

        playerCamera.followTargetRotation = Input.GetButton(snapCameraButton);
        
        // Currently has a bit of strange behavouir when uncommented.
        // character.enableAutoRotation = !Input.GetButton(snapCameraButton);
    }
}
