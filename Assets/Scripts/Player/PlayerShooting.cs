using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//In addition, we also import TMPro so we can update the ammocount UI.
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    private RaycastHit playerBulletInfo;
    private Ray bulletTrajectory;
    private float timeBetweenFiring = 0;
    [SerializeField] private float shootLagTime = 0.15f; //Edit this from the inspector for diff fire rates.
    private bool triggerReleased = true;
    private int ammoCount = 12;
    private float timeReloading = 0;
    [SerializeField] private float reloadLength = 0.5f; //Edit this for diff reload times.
    [SerializeField] private TextMeshProUGUI ammoCountDisplay; //Set this in inspector.
    [SerializeField] private UIHandling pauseCheck;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //This check has been added to all code: it stops activity if "paused" is true.
        if (pauseCheck.paused)
        {
            return;
        }

        bulletTrajectory = Camera.main.ViewportPointToRay(new Vector3 (0.5f, 0.5f, 0));

        //test ray directions
        //Debug.DrawRay(bulletTrajectory.origin, bulletTrajectory.direction);

        //Can we shoot? Check the following:
        //Has enough time passed for a round to chamber?
        //Are we pulling the trigger?
        //Have we released the trigger after shooting?
        //Do we have more than no bullets in the mag?
        if (timeBetweenFiring > shootLagTime && Input.GetAxis("Fire") > 0 && triggerReleased && ammoCount > 0) {
            Shoot();
        }
        else if (Input.GetAxis("Fire") == 0) {
            triggerReleased = true; 
        }

        //if no bullets (by choice or by mag dump) increment the reload timer.
        if (ammoCount == 0) {
            timeReloading += Time.deltaTime;
        }

        //If long enough has passed, refill the player's ammo.
        if (ammoCount == 0 && timeReloading >= reloadLength) {
            ammoCount = 12;
            timeReloading = 0;
        }

        //TODO: ADD A MANUAL RELOAD HERE

        //increment time between firing to stop bullet spam
        timeBetweenFiring += Time.deltaTime;

        //Update ammocount on GUI.
        ammoCountDisplay.text = "Ammo: " + ammoCount.ToString() + "/12";
    }

    void Shoot() 
    {
        //Decreasing the ammoCount has been moved up here as the function quits if a bullet is fired into nothing.
        ammoCount --;
        Debug.Log("Firing!"); //This has also been moved up here.
        triggerReleased = false;
        timeBetweenFiring = 0; //idk how i missed this earlier, but we need to reset the shot timer.

        //WHY?
        /*
        in the old script, the first thing that was done was summoning the raycast and logging the data.
        The problem with this is that if the raycast fails (hits nothing) it would break out of the function.
        To fix this, any code that must run for ANY bullet (triggerReleased, ammoCount) is put BEHIND the raycast.
        This means that when the raycast runs, any necessary code isn't skipped.
        */

        //Summon the raycast and log the data.
        Physics.Raycast(bulletTrajectory, out playerBulletInfo);
        //triggerReleased = false; //We need to release fire to shoot again.

        if (playerBulletInfo.collider.tag == "Enemy") { //Only try to access enemy components if it is an enemy.
            KillEnemy kill = playerBulletInfo.collider.GetComponent<KillEnemy>();
            //Broken down: creates an object "KillEnemy" (the name of the kill enemy script) named "enemy"
            //Uses RaycastHit to get the collider, and then gets the attached script component named "KillEnemy"
            //then runs KillEnemy.Kill()

            //We have to regenerate the reference to the script here as each script is specific to each enemy controller - 
            //using the same item wouldn't work.

            kill.Kill();
        }

        //Debug.Log("Firing!");
        Debug.Log(playerBulletInfo.collider.tag);

        //ammoCount --;
    }

    void Reload()
    {
        ammoCount = 0; //Sets bullets to 0 - this disables shooting AND triggers the reload conditions in Update().
    }
}
