using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    public GameObject template; //Set in inspector.
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Kill()
    {
        //Create the new gameobject as a clone of the current one.
        float spawnX = Random.Range(-3.0f, 3.0f);
        float spawnY = Random.Range(-3.0f, 3.0f);
        Vector3 spawnLoc = new Vector3(spawnX, 2, spawnY); //generate a location in Vector3 to spawn the new enemy.
        //Extreme points are (-3, 2, -3), (3, 2, 3), (-3, 2, 3) and (3, 2, -3).

        Instantiate(template, spawnLoc, Quaternion.identity);

        //Delete the existing object.
        Destroy(this.gameObject);
    }
}
