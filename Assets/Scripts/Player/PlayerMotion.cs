using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    private CharacterController playerControl;
    private float yVelocity = 0;

    //We need to gather necessary variables from settings.
    private PlayerSettings settings;
    private float gravConstant, initialJumpVelo, playerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerControl = GetComponent<CharacterController>();
        settings = GetComponent<PlayerSettings>();

        gravConstant = settings.gravConstant;
        initialJumpVelo = settings.initialJumpVelo;
        playerSpeed = settings.playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate gravity.
        if (!playerControl.isGrounded)
        {
            yVelocity -= gravConstant * Time.deltaTime; //Increase velocity downwards by gravitational constant.
        }
        else
        {
            yVelocity = 0;
        }

        //Do jumping.
        if (playerControl.isGrounded && Input.GetAxis("Jump") > 0)
        {
            yVelocity = initialJumpVelo; //Set velocity to the jump velocity.
        }

        //Combine input axis and calculated vertical velocity. Multiply player inputs by the running speed.
        Vector3 move = new Vector3(Input.GetAxis("Horizontal") * playerSpeed, yVelocity, Input.GetAxis("Vertical") * playerSpeed);

        //Apply this motion to the CharacterController, multiplied by frametime. Multiply by transform.rotation to account for turning the camera.
        playerControl.Move(transform.rotation * move * Time.deltaTime);

        //Check if we have fallen off the map, and reset to (0, 2, 0)
        if (transform.position.y < -5) {
            Vector3 resetPos = new Vector3(0, 2, 0);
            transform.position = resetPos;
        }
    }
}
