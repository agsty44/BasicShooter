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
    [SerializeField] private UIHandling pauseCheck;
    private AudioSource footsteps; 

    // Start is called before the first frame update
    void Start()
    {
        playerControl = GetComponent<CharacterController>();
        settings = GetComponent<PlayerSettings>();
        footsteps = GetComponent<AudioSource>();

        gravConstant = settings.gravConstant;
        initialJumpVelo = settings.initialJumpVelo;
        playerSpeed = settings.playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //This check has been added to all code: it stops activity if "paused" is true.
        if (pauseCheck.paused)
        {
            if (footsteps.isPlaying) // stop sounds if paused
            {
                footsteps.Stop();
            }

            return;
        }

        //check for motion
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

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
        Vector3 move = new Vector3(xInput * playerSpeed, yVelocity, yInput * playerSpeed);

        //Apply this motion to the CharacterController, multiplied by frametime. Multiply by transform.rotation to account for turning the camera.
        playerControl.Move(transform.rotation * move * Time.deltaTime);

        bool isMoving = xInput != 0 || yInput != 0; //check for moving
        float vertMagnitude = Mathf.Abs(yVelocity);
        bool movingOnGround = isMoving && vertMagnitude < 0.1f; //check if we are moving and not flying through the sky

        if (movingOnGround && !footsteps.isPlaying) //if we meet conditions for steps but arent playing, play
        {
            footsteps.Play();
            Debug.Log("PLAY");
        }
        else if (!movingOnGround && footsteps.isPlaying) //playing but dont meet conditions, stop
        {
            footsteps.Stop();
            Debug.Log("STOP");
        }

        //Check if we have fallen off the map, and reset to (0, 2, 0)
        if (transform.position.y < -5) {
            Vector3 resetPos = new Vector3(0, 2, 0);
            transform.position = resetPos;
        }
    }
}
